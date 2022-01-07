using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using mcm4csharp.v1.Data.Alerts;
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
		}

		/// <summary>
		/// Builds a URI from the base uri and given endpoint with path parameters.
		/// </summary>
		/// <param name="endpoint">The endpoint for the request.</param>
		/// <param name="pathParams">The parameters to use in the URI.</param>
		/// <returns>Built URI.</returns>
		private Uri buildUri (string endpoint, Dictionary<string, string>? pathParams = null)
		{
			UriBuilder uriBuilder = new (BaseUri) {
				Path = endpoint
			};

			var query = HttpUtility.ParseQueryString ("");

			if (pathParams != null)
				foreach (var param in pathParams) {
					query [param.Key] = param.Value;
				}

			uriBuilder.Query = query.ToString ();

			return uriBuilder.Uri;
		}

		/// <summary>
		/// Builds a request from the given method, URI, and body.
		/// </summary>
		/// <typeparam name="T">The type of body to send.</typeparam>
		/// <param name="method">The method to use to send request.</param>
		/// <param name="uri">The URI to make request to.</param>
		/// <param name="hasBody">Whether JSON body should be specified.</param>
		/// <param name="body">Post body to use.</param>
		/// <returns>Built request.</returns>
		private HttpRequestMessage prepareRequest (HttpMethod method, Uri uri, object? body = null)
		{
			HttpRequestMessage request = new (method, uri) {
				Content = JsonContent.Create(body)
			};
			request.Content.Headers.ContentType = new MediaTypeHeaderValue ("application/json");

			return request;
		}

		/// <summary>
		/// Sends a request and compiles the given response.
		/// </summary>
		/// <typeparam name="T">The type of response to expect.</typeparam>
		/// <param name="request">The request to send.</param>
		/// <returns>Compiled response.</returns>
		private async Task<Response<T>> buildResponse<T> (HttpRequestMessage request)
		{
			var sent = await authClient.SendAsync (request);

			var response = await sent.Content.ReadFromJsonAsync<Response<T>> ();

			uint retryAfterMs = uint.Parse (sent.Headers.GetValues ("Retry-After").First ());
			
			response.RetryAfterMilliseconds = retryAfterMs;

			return response;
		}

		public async Task<Response<string>> GetHealthAsync ()
		{
			var healthUri = this.buildUri (Endpoints.HEALTH);
			var healthReq = this.prepareRequest (HttpMethod.Get, healthUri, null);

			return await this.buildResponse<string> (healthReq);
		}

		public async Task<Response<Alert[]>> GetUnreadAlertsAsync (Sortable? sortingOptions = null)
		{
			var alertsUri = this.buildUri (Endpoints.ALERTS);
			var alertsReq = this.prepareRequest (HttpMethod.Get, alertsUri, sortingOptions);

			return await this.buildResponse<Alert []> (alertsReq);
		}
	}
}

