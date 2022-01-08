using mcm4csharp.v1.Client;
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
			Console.WriteLine ("We failed to connect to the API.");
			Console.WriteLine ($"Error code: {health.Error.Code}");
			Console.WriteLine ($"Reason: {health.Error.Message}");

			return;
		}

		while (!await fetchConversations ()) ;
	}

	private static void setupClient ()
	{
		var msg = "Please enter your *private* key: ";
		Console.Write (msg);
		var key = Console.ReadLine ();
		chatClient = new ApiClient (TokenType.Private, key);

		Console.Clear ();
	}

	private static async Task<bool> fetchConversations ()
	{
		Console.WriteLine ("We're fetching your open conversations now, please wait.");
		var conversations = await chatClient.GetUnreadConversationsAsync ();

		if (conversations.Data.Length == 0) {
			Console.WriteLine (@" Warning, you have no open conversations.
You can fix this by navigating to any previous conversation and clicking ""Mark Unread"".
Otherwise, you can opt to create a new conversation here.
");
			Console.Write ("Would you like to create a conversation now (y/n)?");
			var key = Console.ReadKey (false);

			if (key.Key == ConsoleKey.Y) {

			} else {
				Console.WriteLine ("Please open a conversation, then press any key when done.");
				Console.ReadKey (true);
				return false;
			}
		}

		return true;
	}

	private static async Task<uint> createConversation()
	{

	}
}