using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using mcm4csharp.v1.Data.Alerts;
using mcm4csharp.v1.Data.Content;
using mcm4csharp.v1.Data.Conversations;
using mcm4csharp.v1.Data.Members;

namespace mcm4csharp.v1.Client {
	public class ApiClient {
		public readonly Uri BaseUri = new UriBuilder ("https://api.mc-market.org").Uri;

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
		private Uri buildUri (string endpoint, Dictionary<string, string>? pathParams = null, Dictionary<string, string>? replacements = null)
		{
			// todo: escape later, this is dangerous
			if (replacements != null)
				foreach (var replacement in replacements) {
					endpoint = endpoint.Replace (replacement.Key, replacement.Value);
				}

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
				Content = JsonContent.Create (body)
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
		private async Task<Response<T>> buildResponseAsync<T> (HttpRequestMessage request)
		{
			var sent = await authClient.SendAsync (request);

			var response = await sent.Content.ReadFromJsonAsync<Response<T>> ();

			uint retryAfterMs = uint.Parse (sent.Headers.GetValues ("Retry-After").First ());

			response.RetryAfterMilliseconds = retryAfterMs;

			return response;
		}

		/*
		 * API Health
		 */

		public async Task<Response<string>> GetHealthAsync ()
		{
			var healthUri = this.buildUri (Endpoints.HEALTH);
			var healthReq = this.prepareRequest (HttpMethod.Get, healthUri);

			return await this.buildResponseAsync<string> (healthReq);
		}

		/*
		 * Alerts
		 */

		public async Task<Response<Alert []>> GetUnreadAlertsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var alertsUri = this.buildUri (Endpoints.ALERTS, pathParams: opt);
			var alertsReq = this.prepareRequest (HttpMethod.Get, alertsUri);

			return await this.buildResponseAsync<Alert []> (alertsReq);
		}

		public async Task<Response<string>> MarkUnreadAsync ()
		{
			var alertsUri = this.buildUri (Endpoints.ALERTS);
			var alertsReq = this.prepareRequest (HttpMethod.Patch, alertsUri);

			return await this.buildResponseAsync<string> (alertsReq);
		}

		/*
		 * Conversations
		 */

		public async Task<Response<Conversation []>> GetUnreadConversationsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var convoUri = this.buildUri (Endpoints.CONVERSATIONS, pathParams: opt);
			var convoReq = this.prepareRequest (HttpMethod.Get, convoUri);

			return await this.buildResponseAsync<Conversation []> (convoReq);
		}

		public async Task<Response<uint>> StartNewConversationAsync (ConversationContent content)
		{
			var convoUri = this.buildUri (Endpoints.CONVERSATIONS);
			var convoReq = this.prepareRequest (HttpMethod.Post, convoUri, content);

			return await this.buildResponseAsync<uint> (convoReq);
		}

		public async Task<Response<Reply []>> GetUnreadRepliesAsync (uint id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;
			var convoUri = this.buildUri (Endpoints.CONVERSATIONS_ID, pathParams: opt, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var convoReq = this.prepareRequest (HttpMethod.Get, convoUri);

			return await this.buildResponseAsync<Reply []> (convoReq);
		}

		public async Task<Response<uint>> ReplyUnreadConversationAsync (uint id, MessageContent content)
		{
			var convoUri = this.buildUri (Endpoints.CONVERSATIONS_ID, replacements: new () {
					{ "{id}", id.ToString () }
				});
			var convoReq = this.prepareRequest (HttpMethod.Post, convoUri, content);

			return await this.buildResponseAsync<uint> (convoReq);
		}

		/*
		 * Members
		 */

		public async Task<Response<Member>> GetSelfAsync()
		{
			var selfUri = this.buildUri (Endpoints.MEMBERS_SELF);
			var selfReq = this.prepareRequest (HttpMethod.Get, selfUri);

			return await this.buildResponseAsync<Member> (selfReq);
		}
	}
}

