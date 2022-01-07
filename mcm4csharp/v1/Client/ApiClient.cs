using System;
using System.Net.Http.Headers;

namespace mcm4csharp.v1.Client {
	public class ApiClient {
		public const string BASE_URI = "https://api.mc-market.org/v1/";

		private readonly HttpClient authClient;

		public ApiClient (TokenType type, string token)
		{
			this.authClient = new HttpClient ();
			authClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue (type.ToString (), token);
			// set content-type header on request creation
		}
	}
}

