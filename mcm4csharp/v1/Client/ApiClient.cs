using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using mcm4csharp.v1.Data.Content;

namespace mcm4csharp.v1.Client {
	public class ApiClient {
		public readonly Uri BaseUri = new UriBuilder ("https://api.mc-market.org/v1/").Uri;

		private readonly HttpClient authClient;

		public ApiClient (TokenType type, string token)
		{
			this.authClient = new HttpClient ();
			authClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue (type.ToString (), token);
			// set content-type header on request creation
		}

		private HttpRequestMessage prepareRequest<T> (HttpMethod method, Uri uri, bool hasBody, T body)
		{
			var request = new HttpRequestMessage (method, uri);

			// optional headers - we use json
			request.Content = hasBody ? JsonContent.Create (body) : new StringContent ("");
			request.Content.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

			return request;
		}

		private Uri buildUri (string endpoint, Dictionary<string, string> pathParams = null)
		{
			var uriBuilder = new UriBuilder (BaseUri);
			uriBuilder.Path = endpoint;

			var query = HttpUtility.ParseQueryString ("");

			if (pathParams != null)
				foreach (var param in pathParams) {
					query [param.Key] = param.Value;
				}

			uriBuilder.Query = query.ToString ();

			return uriBuilder.Uri;
		}

		private async Task<Response<T>> buildResponse<T> (HttpRequestMessage request)
		{
			var sent = await authClient.SendAsync (request);

			Console.WriteLine (await sent.Content.ReadAsStringAsync ());
			var response = await sent.Content.ReadFromJsonAsync<Response<T>> ();

			uint retryAfterMs = uint.Parse (sent.Headers.GetValues ("Retry-After").First ());

			response.RetryAfterMilliseconds = retryAfterMs;

			return response;
		}

		public async Task<Response<string>> GetHealthAsync ()
		{
			var healthUri = buildUri (Endpoints.HEALTH);
			var healthReq = prepareRequest<string> (HttpMethod.Get, healthUri, false, "");


			return await buildResponse<string> (healthReq);
		}
	}
}

