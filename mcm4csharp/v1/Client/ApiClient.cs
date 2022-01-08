using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using mcm4csharp.v1.Data.Alerts;
using mcm4csharp.v1.Data.Content;
using mcm4csharp.v1.Data.Conversations;
using mcm4csharp.v1.Data.Members;
using mcm4csharp.v1.Data.Resources;
using Version = mcm4csharp.v1.Data.Resources.Version;

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
				Path = endpoint.Trim ('/')
			};

			var query = HttpUtility.ParseQueryString ("");

			if (pathParams != null)
				foreach (var param in pathParams) {
					query [param.Key] = param.Value;
				}

			uriBuilder.Query = query.ToString ();

			Console.WriteLine (uriBuilder.Uri.ToString ());

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
			Console.WriteLine (await sent.Content.ReadAsStringAsync ());
			var response = await sent.Content.ReadFromJsonAsync<Response<T>> ();

			if (sent.Headers.Contains ("Retry-After")) {
				uint retryAfterMs = uint.Parse (sent.Headers.GetValues ("Retry-After").First ());

				response.RetryAfterMilliseconds = retryAfterMs;
			}

			return response;
		}

		// Structure
		// Path replacements -> parameters
		// Query parameters -> DTOs
		// Request body -> DTOs

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

		public async Task<Response<Member>> GetSelfAsync ()
		{
			var selfUri = this.buildUri (Endpoints.MEMBERS, replacements: new () {
				{ "{id}", "self" }
			});
			var selfReq = this.prepareRequest (HttpMethod.Get, selfUri);

			return await this.buildResponseAsync<Member> (selfReq);
		}

		public async Task<Response<string>> ModifySelfAsync (SelfUpdateContent content)
		{
			var selfUri = this.buildUri (Endpoints.MEMBERS, replacements: new () {
				{ "{id}", "self" }
			});
			var selfReq = this.prepareRequest (HttpMethod.Patch, selfUri, content);

			return await this.buildResponseAsync<string> (selfReq);
		}

		public async Task<Response<Member>> GetUserAsync (uint id)
		{
			var selfUri = this.buildUri (Endpoints.MEMBERS, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var selfReq = this.prepareRequest (HttpMethod.Get, selfUri);

			return await this.buildResponseAsync<Member> (selfReq);
		}

		public async Task<Response<Member>> GetUserAsync (string username)
		{
			var selfUri = this.buildUri (Endpoints.MEMBERS, replacements: new () {
				{ "{id}", $"username/{username}" }
			});
			var selfReq = this.prepareRequest (HttpMethod.Get, selfUri);

			return await this.buildResponseAsync<Member> (selfReq);
		}

		public async Task<Response<Ban []>> GetBansAsync ()
		{
			var bansUri = this.buildUri (Endpoints.MEMBERS, replacements: new () {
				{ "{id}", "bans" }
			});
			var bansReq = this.prepareRequest (HttpMethod.Get, bansUri);

			return await this.buildResponseAsync<Ban []> (bansReq);
		}

		/*
		 * Profile posts
		 */

		public async Task<Response<ProfilePost []>> GetProfilePostsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var postsUri = this.buildUri (Endpoints.PROFILE_POSTS, pathParams: opt, replacements: new () {
				{ "{id}", "" }
			});
			var postsReq = this.prepareRequest (HttpMethod.Get, postsUri);

			return await this.buildResponseAsync<ProfilePost []> (postsReq);
		}

		public async Task<Response<ProfilePost>> GetProfilePostAsync (uint id)
		{
			var postUri = this.buildUri (Endpoints.PROFILE_POSTS, replacements: new () {
				{ "{id}", id.ToString () },
			});
			var postReq = this.prepareRequest (HttpMethod.Get, postUri);

			return await this.buildResponseAsync<ProfilePost> (postReq);
		}

		public async Task<Response<string>> ModifyProfilePostAsync (uint id, MessageContent content)
		{
			var postUri = this.buildUri (Endpoints.PROFILE_POSTS, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var postReq = this.prepareRequest (HttpMethod.Patch, postUri, content);

			return await this.buildResponseAsync<string> (postReq);
		}

		public async Task<Response<string>> DeleteProfilePostAsync (uint id)
		{
			var postUri = this.buildUri (Endpoints.PROFILE_POSTS, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var postReq = this.prepareRequest (HttpMethod.Delete, postUri);

			return await this.buildResponseAsync<string> (postReq);
		}

		/*
		 * Resources
		 */

		public async Task<Response<BasicResource []>> GetPublicResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var resUri = this.buildUri (Endpoints.RESOURCES, pathParams: opt, replacements: new () {
				{ "{id}", "" }
			});
			var resReq = this.prepareRequest (HttpMethod.Get, resUri, sortingOptions);

			return await this.buildResponseAsync<BasicResource []> (resReq);
		}

		public async Task<Response<BasicResource []>> GetOwnedResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var resUri = this.buildUri (Endpoints.RESOURCES, pathParams: opt, replacements: new () {
				{ "{id}", "owned" }
			});
			var resReq = this.prepareRequest (HttpMethod.Get, resUri, sortingOptions);

			return await this.buildResponseAsync<BasicResource []> (resReq);
		}

		public async Task<Response<BasicResource []>> GetCollaboratedResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var resUri = this.buildUri (Endpoints.RESOURCES, pathParams: opt, replacements: new () {
				{ "{id}", "collaborated" }
			});
			var resReq = this.prepareRequest (HttpMethod.Get, resUri, sortingOptions);

			return await this.buildResponseAsync<BasicResource []> (resReq);
		}

		public async Task<Response<BasicResource>> GetResourceAsync (uint id)
		{
			var resUri = this.buildUri (Endpoints.RESOURCES, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var resReq = this.prepareRequest (HttpMethod.Get, resUri);

			return await this.buildResponseAsync<BasicResource> (resReq);
		}

		public async Task<Response<string>> UpdateResourceAsync (uint id, ResourceContent content)
		{
			var resUri = this.buildUri (Endpoints.RESOURCES, replacements: new () {
				{ "{id}", id.ToString () }
			});
			var resReq = this.prepareRequest (HttpMethod.Get, resUri, content);

			return await this.buildResponseAsync<string> (resReq);
		}

		/*
		 * Versions
		 */

		public async Task<Response<Version []>> GetResourceVersionsAsync (uint id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var verUri = this.buildUri (Endpoints.VERSIONS, pathParams: opt, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{v_id}", "" }
			});
			var verReq = this.prepareRequest (HttpMethod.Get, verUri, sortingOptions);

			return await this.buildResponseAsync<Version []> (verReq);
		}

		public async Task<Response<Version>> GetLatestVersionAsync (uint id)
		{
			var verUri = this.buildUri (Endpoints.VERSIONS, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{v_id}", "latest" }
			});
			var verReq = this.prepareRequest (HttpMethod.Get, verUri);

			return await this.buildResponseAsync<Version> (verReq);
		}

		public async Task<Response<Version>> GetVersionAsync (uint resId, uint versionId)
		{
			var verUri = this.buildUri (Endpoints.VERSIONS, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{v_id}", versionId.ToString () }
			});
			var verReq = this.prepareRequest (HttpMethod.Get, verUri);

			return await this.buildResponseAsync<Version> (verReq);
		}

		public async Task<Response<Version>> DeleteVersionAsync (uint resId, uint versionId)
		{
			var verUri = this.buildUri (Endpoints.VERSIONS, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{v_id}", versionId.ToString () }
			});
			var verReq = this.prepareRequest (HttpMethod.Delete, verUri);

			return await this.buildResponseAsync<Version> (verReq);
		}

		/*
		 * Updates
		 */

		public async Task<Response<Update []>> GetResourceUpdatesAsync (uint id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			var updateUri = this.buildUri (Endpoints.UPDATES, pathParams: opt, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{u_id}", "" }
			});
			var updateReq = this.prepareRequest (HttpMethod.Get, updateUri, sortingOptions);

			return await this.buildResponseAsync<Update []> (updateReq);
		}

		public async Task<Response<Update>> GetLatestUpdateAsync (uint id)
		{
			var updateUri = this.buildUri (Endpoints.UPDATES, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{u_id}", "latest" }
			});
			var updateReq = this.prepareRequest (HttpMethod.Get, updateUri);

			return await this.buildResponseAsync<Update> (updateReq);
		}

		public async Task<Response<Update>> GetUpdateAsync (uint resId, uint updateId)
		{
			var updateUri = this.buildUri (Endpoints.UPDATES, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{u_id}", updateId.ToString () }
			});
			var updateReq = this.prepareRequest (HttpMethod.Get, updateUri);

			return await this.buildResponseAsync<Update> (updateReq);
		}

		public async Task<Response<Update>> DeleteUpdateAsync (uint resId, uint updateId)
		{
			var updateUri = this.buildUri (Endpoints.UPDATES, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{u_id}", updateId.ToString () }
			});
			var updateReq = this.prepareRequest (HttpMethod.Delete, updateUri);

			return await this.buildResponseAsync<Update> (updateReq);
		}

	}
}

