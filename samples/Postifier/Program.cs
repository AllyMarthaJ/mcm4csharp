using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using mcm4csharp.v1.Client;
using mcm4csharp.v1.Data.Content;

namespace mcm4csharp;

public class Program {
	/// <summary>
	/// Modifies a set of profile posts given with random messages.
	/// </summary>
	/// <param name="token">Your private Ultimate MCM API token.</param>
	/// <param name="post">IDs of each post to update.</param>
	/// <param name="jsonFile">Name of file containing JSON array of messages.</param>
	/// <param name="delaySeconds">Time in seconds to wait after each update.</param>
	/// <returns></returns>
	public static async Task Main (string token, ulong [] post, string jsonFile, int delaySeconds = 30)
	{
		// read file to string array
		string fileContent = File.ReadAllText (jsonFile);
		string [] messages = JsonSerializer.Deserialize<string []> (fileContent)
				.Select (msg => Regex.Unescape (msg))
				.ToArray ();

		// init tools
		Random rnd = new Random ();
		var apiClient = new ApiClient (TokenType.Private, token);

		// check authentication
		if ((await apiClient.GetHealthAsync ()).Result != "success") {
			Console.WriteLine ("Fatal: Couldn't connect to the API.");
			return;
		}

		// main body loop for updating posts
		while (true) {
			// update each post
			foreach (ulong postId in post) {
				// compost the message to update
				string postMessage = messages [rnd.Next (messages.Length)];
				setPlaceholders (ref postMessage);

				MessageContent content = new () { Message = postMessage };

				// send the update request
				var safeModify = async () => await apiClient.ModifyProfilePostAsync (postId, content);
				var modifyResult = await apiClient.SafeRequestAsync (safeModify);

				// spit our result out
				string suffix = modifyResult.Result == "success" ?
								Regex.Escape (postMessage) :
								"Error: " + modifyResult.Error.Message;

				Console.WriteLine ($"[{DateTime.Now.ToShortTimeString ()}] [{postId}] {suffix}");

				await Task.Delay (delaySeconds * 1000);
			}
		}
	}

	private static void setPlaceholders (ref string str)
	{
		str = new StringBuilder (str)
			.Replace ("{time}", DateTime.Now.ToString ())
			.ToString ();
	}
}