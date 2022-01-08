using System.Text;
using mcm4csharp.v1.Client;
using mcm4csharp.v1.Data.Content;
using mcm4csharp.v1.Data.Conversations;

public class Program {
	private static uint conversationId;
	private static Reply lastMessage;
	private static ApiClient chatClient;

	public static async Task Main (string [] args)
	{
		setupClient ();

		var health = await chatClient.GetHealthAsync ();

		if (health.Result != "success") {
			showError ("We failed to connect to the API.", health.Error);

			return;
		}

		while (!await fetchConversation ()) ;
	}

	private static void setupClient ()
	{
		var msg = "Please enter your *private* key: ";
		Console.Write (msg);
		var key = Console.ReadLine ();
		chatClient = new ApiClient (TokenType.Private, key);

		Console.Clear ();
	}

	private static async Task<bool> fetchConversation ()
	{
		Console.WriteLine ("We're fetching your open conversations now, please wait.");
		var conversations = await chatClient.GetUnreadConversationsAsync ();

		if (conversations.Data.Length == 0) {
			Console.WriteLine (@" Warning, you have no open conversations.
You can fix this by navigating to any previous conversation and clicking ""Mark Unread"".
Otherwise, you can opt to create a new conversation here.
");
			Console.Write ("Would you like to create a conversation now (y/n)? ");
			var key = Console.ReadKey (true);
			Console.WriteLine ();

			if (key.Key == ConsoleKey.Y) {
				var convoId = await createConversation ();
				if (convoId == 0) return false;
				conversationId = convoId;
				return true;
			} else {
				Console.WriteLine ("Please open a conversation, then press any key when done.");
				Console.ReadKey (true);
				Console.WriteLine ();
				return false;
			}
		}

		Console.WriteLine ("Your conversations: ");
		foreach (var conversation in conversations.Data) {
			Console.WriteLine ($"[{conversation.ConversationId}] {conversation.Title}, {conversation.ReplyCount} messages.");
		}
		Console.Write ("Enter the ID of the conversation: ");
		try {
			conversationId = UInt32.Parse (Console.ReadLine ());
			return true;
		} catch {
			return false;
		}
	}

	private static async Task<uint> createConversation ()
	{
		Console.WriteLine ("Enter the IDs of users you wish to open a conversation with, separated by ',':");
		var users = Console.ReadLine ();

		uint [] userIds;
		string title;
		string message;

		try {
			userIds = users
				.Split (",")
				.Select (u => UInt32.Parse (u))
				.ToArray ();
		} catch {
			return 0;
		}

		Console.Write ("Conversation title: ");
		title = Console.ReadLine ();

		Console.WriteLine ("Enter message. Add // to end of line to add a new line.");
		message = multilineRead ();

		var convo = await chatClient.StartNewConversationAsync (new ConversationContent () {
			RecipientIds = userIds,
			Title = title,
			Message = message
		});

		if (convo.Result != "success") {
			showError ("We couldn't create your conversation.", convo.Error);

			return 0;
		}

		return convo.Data;
	}

	private static string multilineRead ()
	{
		StringBuilder message = new ();
		string line = "//";
		while (line.EndsWith ("//")) {
			line = Console.ReadLine ();
			message.AppendLine (line.Trim ('/'));
		}
		return message.ToString ();
	}

	private static void showError(string prepend, Error err)
	{
		Console.WriteLine (new String ('-', 20));
		Console.WriteLine (prepend);
		Console.WriteLine (new String ('-', 20));
		Console.WriteLine ("Response: " + err.Code);
		Console.WriteLine ("Reason: " + err.Message);
		Console.WriteLine (new String ('-', 20));
	}
}