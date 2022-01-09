using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Web;
using mcm4csharp.v1.Data.Alerts;
using mcm4csharp.v1.Data.Content;
using mcm4csharp.v1.Data.Conversations;
using mcm4csharp.v1.Data.Members;
using mcm4csharp.v1.Data.Resources;
using mcm4csharp.v1.Data.Threads;

using Thread = mcm4csharp.v1.Data.Threads.Thread;
using Version = mcm4csharp.v1.Data.Resources.Version;

namespace mcm4csharp.v1.Client {
	/// <summary>
	/// The client wrapper used for accessing the ultimate API.
	/// </summary>
	public class ApiClient {
		public readonly Uri BaseUri = new UriBuilder ("https://api.mc-market.org").Uri;

		public bool WaitForTimeout { get; set; } = true;
		public ulong TimeoutBuffer = 10;

		private readonly HttpClient authClient;

		private ulong lastRequest = 0;
		private ulong lastReplyAfter = 0;

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
			if (replacements != null)
				foreach (var replacement in replacements) {
					endpoint = endpoint.Replace (replacement.Key, HttpUtility.UrlEncode (replacement.Value));
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

			return uriBuilder.Uri;
		}

		/// <summary>
		/// Builds a request from the given method, URI, and body.
		/// </summary>
		/// <typeparam name="T">The type of body to send.</typeparam>
		/// <param name="method">The method to use to send request.</param>
		/// <param name="uri">The URI to make request to.</param>
		/// <param name="body">Post body to use.</param>
		/// <returns>Built request.</returns>
		private HttpRequestMessage prepareRequest (HttpMethod method, Uri uri, object? body = null)
		{
			HttpRequestMessage request = new (method, uri) {
				Content = JsonContent.Create (body, options: new JsonSerializerOptions () {
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				})
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
			if (this.WaitForTimeout) {
				var currentTime = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds ();

				if (currentTime <= lastRequest + lastReplyAfter + TimeoutBuffer) {
					await Task.Delay ((int)(currentTime - lastRequest + lastReplyAfter + TimeoutBuffer));
				}

				lastRequest = currentTime;
			}

			try {
				var sent = await authClient.SendAsync (request);

				var response = await sent.Content.ReadFromJsonAsync<Response<T>> (new JsonSerializerOptions () {
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				});

				if (sent.Headers.Contains ("Retry-After")) {
					ulong retryAfterMs = ulong.Parse (sent.Headers.GetValues ("Retry-After").First ());

					response.RetryAfterMilliseconds = retryAfterMs;
					lastReplyAfter = retryAfterMs;
				} else {
					lastReplyAfter = 0;
				}

				return response;
			} catch (Exception ex) {
				return new Response<T> () {
					Error = new Error () {
						Code = ex.GetType ().Name,
						Message = ex.Message
					}
				};
			}

		}

		/// <summary>
		/// Access a given endpoint.
		/// </summary>
		/// <typeparam name="T">Type of response to expect from the API.</typeparam>
		/// <param name="endpoint">The parameterised endpoint URI (excl. domain)</param>
		/// <param name="method">HTTP Method to send the request with.</param>
		/// <param name="pathParams">Parameters in the path to append.</param>
		/// <param name="replacements">Parameters in the path to replace.</param>
		/// <param name="body">Post body when modifying/adding data.</param>
		/// <returns>Deserialised object of type T.</returns>
		public async Task<Response<T>> SendEndpointAsync<T> (string endpoint,
									HttpMethod method,
									Dictionary<string, string>? pathParams = null,
									Dictionary<string, string>? replacements = null,
									object? body = null)
		{
			var requestUri = this.buildUri (endpoint, pathParams, replacements);
			var requestReq = this.prepareRequest (method, requestUri, body);

			return await this.buildResponseAsync<T> (requestReq);
		}

		/// <summary>
		/// Safely sends a request and retries until not ratelimited.
		/// </summary>
		/// <typeparam name="T">Type of response to expect.</typeparam>
		/// <param name="request">Async function to use to send the request.</param>
		/// <returns>Request after rate limit.</returns>
		public async Task<Response<T>> SafeRequestAsync<T>(Func<Task<Response<T>>> request)
		{
			Response<T> response;
			do {
				response = await request ();
			} while (response.Error.Code == "RateLimitExceededError");

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
			return await this.SendEndpointAsync<string> (Endpoints.HEALTH, HttpMethod.Get);
		}

		/*
		 * Alerts
		 */

		public async Task<Response<Alert []>> GetUnreadAlertsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Alert []> (Endpoints.ALERTS, HttpMethod.Get, pathParams: opt);
		}

		public async Task<Response<string>> MarkUnreadAsync ()
		{
			return await this.SendEndpointAsync<string> (Endpoints.ALERTS, HttpMethod.Patch);
		}

		/*
		 * Conversations
		 */

		public async Task<Response<Conversation []>> GetUnreadConversationsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Conversation []> (Endpoints.CONVERSATIONS, HttpMethod.Get, pathParams: opt);
		}

		public async Task<Response<ulong>> StartNewConversationAsync (ConversationContent content)
		{
			return await this.SendEndpointAsync<ulong> (Endpoints.CONVERSATIONS, HttpMethod.Post, body: content);
		}

		public async Task<Response<Data.Conversations.Reply []>> GetUnreadRepliesAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Data.Conversations.Reply []> (Endpoints.CONVERSATIONS_ID,
											  HttpMethod.Get,
											  pathParams: opt,
											  replacements: new () {
												  { "{id}", id.ToString () }
											  });
		}

		public async Task<Response<ulong>> ReplyUnreadConversationAsync (ulong id, MessageContent content)
		{
			return await this.SendEndpointAsync<ulong> (Endpoints.CONVERSATIONS_ID,
								    HttpMethod.Post,
								    replacements: new () {
									    { "{id}", id.ToString () }
								    }, body: content);
		}

		/*
		 * Members
		 */

		public async Task<Response<Member>> GetSelfAsync ()
		{
			return await this.SendEndpointAsync<Member> (Endpoints.MEMBERS, HttpMethod.Get, replacements: new () {
				{ "{id}", "self" }
			});
		}

		public async Task<Response<string>> ModifySelfAsync (SelfUpdateContent content)
		{
			return await this.SendEndpointAsync<string> (Endpoints.MEMBERS, HttpMethod.Patch, replacements: new () {
				{ "{id}", "self" }
			}, body: content);
		}

		public async Task<Response<Member>> GetUserAsync (ulong id)
		{
			return await this.SendEndpointAsync<Member> (Endpoints.MEMBERS, HttpMethod.Get, replacements: new () {
				{ "{id}", id.ToString () }
			});
		}

		public async Task<Response<Member>> GetUserAsync (string username)
		{
			return await this.SendEndpointAsync<Member> (Endpoints.MEMBERS, HttpMethod.Get, replacements: new () {
				{ "{id}", $"usernames/{username}" }
			});
		}

		public async Task<Response<Ban []>> GetBansAsync ()
		{
			return await this.SendEndpointAsync<Ban []> (Endpoints.MEMBERS, HttpMethod.Get, replacements: new () {
				{ "{id}", "bans" }
			});
		}

		/*
		 * Profile posts
		 */

		public async Task<Response<ProfilePost []>> GetProfilePostsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<ProfilePost []> (Endpoints.PROFILE_POSTS,
									     HttpMethod.Get,
									     pathParams: opt,
									     replacements: new () {
										     { "{id}", "" }
									     });
		}

		public async Task<Response<ProfilePost>> GetProfilePostAsync (ulong id)
		{
			return await this.SendEndpointAsync<ProfilePost> (Endpoints.PROFILE_POSTS, HttpMethod.Get, replacements: new () {
				{ "{id}", id.ToString () },
			});
		}

		public async Task<Response<string>> ModifyProfilePostAsync (ulong id, MessageContent content)
		{
			return await this.SendEndpointAsync<string> (Endpoints.PROFILE_POSTS, HttpMethod.Patch, replacements: new () {
				{ "{id}", id.ToString () }
			}, body: content);
		}

		public async Task<Response<string>> DeleteProfilePostAsync (ulong id)
		{
			return await this.SendEndpointAsync<string> (Endpoints.PROFILE_POSTS, HttpMethod.Delete, replacements: new () {
				{ "{id}", id.ToString () }
			});
		}

		/*
		 * Resources
		 */

		public async Task<Response<BasicResource []>> GetPublicResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<BasicResource []> (Endpoints.REPLIES,
									       HttpMethod.Get,
									       pathParams: opt,
									       replacements: new () {
										       { "{id}", "" }
									       });
		}

		public async Task<Response<BasicResource []>> GetOwnedResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<BasicResource []> (Endpoints.RESOURCES,
									       HttpMethod.Get,
									       pathParams: opt,
									       replacements: new () {
										       { "{id}", "owned" }

									       });
		}

		public async Task<Response<BasicResource []>> GetCollaboratedResourcesAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<BasicResource []> (Endpoints.RESOURCES,
									      HttpMethod.Get,
									      pathParams: opt,
									      replacements: new () {
										      { "{id}", "collaborated" }
									      });
		}

		public async Task<Response<BasicResource>> GetResourceAsync (ulong id)
		{
			return await this.SendEndpointAsync<BasicResource> (Endpoints.RESOURCES, HttpMethod.Get, replacements: new () {
				{ "{id}", id.ToString () }
			});
		}

		public async Task<Response<string>> UpdateResourceAsync (ulong id, ResourceContent content)
		{
			return await this.SendEndpointAsync<string> (Endpoints.RESOURCES, HttpMethod.Patch, replacements: new () {
				{ "{id}", id.ToString () }
			});
		}

		/*
		 * Versions
		 */

		public async Task<Response<Version []>> GetResourceVersionsAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Version []> (Endpoints.VERSIONS,
									 HttpMethod.Get,
									 pathParams: opt,
									 replacements: new () {
										 { "{r_id}", id.ToString () },
										 { "{v_id}", "" }
									 });
		}

		public async Task<Response<Version>> GetLatestVersionAsync (ulong id)
		{
			return await this.SendEndpointAsync<Version> (Endpoints.VERSIONS, HttpMethod.Get, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{v_id}", "latest" }
			});
		}

		public async Task<Response<Version>> GetVersionAsync (ulong resId, ulong versionId)
		{
			return await this.SendEndpointAsync<Version> (Endpoints.VERSIONS, HttpMethod.Get, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{v_id}", versionId.ToString () }
			});
		}

		public async Task<Response<string>> DeleteVersionAsync (ulong resId, ulong versionId)
		{
			return await this.SendEndpointAsync<string> (Endpoints.VERSIONS, HttpMethod.Delete, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{v_id}", versionId.ToString () }
			});
		}

		/*
		 * Updates
		 */

		public async Task<Response<Update []>> GetResourceUpdatesAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Update []> (Endpoints.UPDATES,
									HttpMethod.Get,
									pathParams: opt,
									replacements: new () {
										{ "{r_id}", id.ToString () },
										{ "{u_id}", "" }
									});
		}

		public async Task<Response<Update>> GetLatestUpdateAsync (ulong id)
		{
			return await this.SendEndpointAsync<Update> (Endpoints.UPDATES, HttpMethod.Get, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{u_id}", "latest" }
			});
		}

		public async Task<Response<Update>> GetUpdateAsync (ulong resId, ulong updateId)
		{
			return await this.SendEndpointAsync<Update> (Endpoints.UPDATES, HttpMethod.Get, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{u_id}", updateId.ToString () }
			});
		}

		public async Task<Response<Update>> DeleteUpdateAsync (ulong resId, ulong updateId)
		{
			return await this.SendEndpointAsync<Update> (Endpoints.UPDATES, HttpMethod.Delete, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{u_id}", updateId.ToString () }
			});
		}

		/*
		 * Reviews
		 */

		public async Task<Response<Review []>> GetResourceReviewsAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Review []> (Endpoints.REVIEWS,
									HttpMethod.Get,
									pathParams: opt,
									replacements: new () {
										{ "{res_id}", id.ToString () },
										{ "{rev_id}", "" }
									});
		}

		public async Task<Response<Review>> GetResourceReviewAsync (ulong resId, ulong memberId)
		{
			return await this.SendEndpointAsync<Review> (Endpoints.REVIEWS, HttpMethod.Get, replacements: new () {
				{ "{res_id}", resId.ToString () },
				{ "{rev_id}", "members/" + memberId.ToString () }
			});
		}

		public async Task<Response<string>> RespondReviewAsync (ulong resId, ulong revId, ResponseContent content)
		{
			return await this.SendEndpointAsync<string> (Endpoints.REVIEWS, HttpMethod.Patch, replacements: new () {
				{ "{res_id}", resId.ToString () },
				{ "{rev_id}", revId.ToString () }
			}, body: content);
		}

		/*
		 * Purchases
		 */

		public async Task<Response<Purchase []>> GetPurchasesAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Purchase []> (Endpoints.PURCHASES,
									  HttpMethod.Get,
									  pathParams: opt,
									  replacements: new () {
										  { "{r_id}", id.ToString () },
										  { "{p_id}", "" }
									  });
		}

		public async Task<Response<Purchase>> GetPurchaseAsync (ulong resId, ulong purchId)
		{
			return await this.SendEndpointAsync<Purchase> (Endpoints.PURCHASES, HttpMethod.Get, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{p_id}", purchId.ToString () }
			});
		}

		/*
		 * Licenses
		 */

		public async Task<Response<License []>> GetResourceLicensesAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<License []> (Endpoints.LICENSES,
								       HttpMethod.Get,
								       pathParams: opt,
								       replacements: new () {
									       { "{r_id}", id.ToString () },
									       { "{l_id}", "" }
								       });
		}

		public async Task<Response<ulong>> IssueLicenseAsync (ulong id, LicenseContent content)
		{
			return await this.SendEndpointAsync<ulong> (Endpoints.LICENSES, HttpMethod.Post, replacements: new () {
				{ "{r_id}", id.ToString () },
				{ "{l_id}", "" }
			});
		}

		public async Task<Response<License>> GetResourceLicenseAsync (ulong resId, ulong licId)
		{
			return await this.SendEndpointAsync<License> (Endpoints.LICENSES, HttpMethod.Get, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{l_id}", licId.ToString () }
			});
		}

		public async Task<Response<string>> ModifyLicenseAsync (ulong resId, ulong licId, LicenseContent content)
		{
			return await this.SendEndpointAsync<string> (Endpoints.LICENSES, HttpMethod.Patch, replacements: new () {
				{ "{r_id}", resId.ToString () },
				{ "{l_id}", licId.ToString () }
			}, body: content);
		}

		public async Task<Response<License>> GetResourceLicenseMemberAsync (ulong resId, ulong memId, ulong? nonce, ulong? timestamp)
		{
			var opt = nonce.HasValue && timestamp.HasValue ? new Dictionary<string, string> () {
				{ "nonce", nonce.Value.ToString() },
				{ "timestamp", timestamp.Value.ToString() }
			} : null;
			return await this.SendEndpointAsync<License> (Endpoints.LICENSES,
								      HttpMethod.Get,
								      pathParams: opt,
								      replacements: new () {
									      { "{r_id}", resId.ToString () },
									      { "{l_id}", "members/" + memId.ToString () }
								      });
		}

		/*
		 * Downloads
		 */

		public async Task<Response<Download []>> GetDownloadsAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Download []> (Endpoints.DOWNLOADS,
									  HttpMethod.Get,
									  pathParams: opt,
									  replacements: new () {
										  { "{r_id}", id.ToString () },
										  { "{id}", "" }
									  });
		}

		public async Task<Response<Download []>> GetDownloadsMemberAsync (ulong resId, ulong memId, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Download []> (Endpoints.DOWNLOADS,
									  HttpMethod.Get,
									  pathParams: opt,
									  replacements: new () {
										  { "{r_id}", resId.ToString () },
										  { "{id}", "members/" + memId.ToString () }
									  });
		}

		public async Task<Response<Download []>> GetDownloadsVersionAsync (ulong resId, ulong memId, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Download []> (Endpoints.DOWNLOADS,
									  HttpMethod.Get,
									  pathParams: opt,
									  replacements: new () {
										  { "{r_id}", resId.ToString () },
										  { "{id}", "versions/" + memId.ToString () }
									  });
		}

		/*
		 * Threads
		 */

		public async Task<Response<Thread []>> GetThreadsAsync (Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Thread []> (Endpoints.REPLIES,
									HttpMethod.Get,
									pathParams: opt,
									replacements: new () {
										{ "{id}", "" },
										{ "{id2}", "" }
									});
		}

		public async Task<Response<Thread>> GetThreadAsync (ulong id)
		{

			return await this.SendEndpointAsync<Thread> (Endpoints.REPLIES,
									HttpMethod.Get,
									replacements: new () {
										{ "{id}", id.ToString () },
										{ "{id2}", "" },
									});
		}

		/*
		 * Replies
		 */

		public async Task<Response<Data.Threads.Reply []>> GetThreadRepliesAsync (ulong id, Sortable? sortingOptions = null)
		{
			var opt = sortingOptions.HasValue ? sortingOptions.Value.ToDict () : null;

			return await this.SendEndpointAsync<Data.Threads.Reply []> (Endpoints.REPLIES,
										  HttpMethod.Get,
										  pathParams: opt,
										  replacements: new () {
											  { "{id}", id.ToString () },
											  { "{id2}", "replies" }
										  });
		}

		public async Task<Response<ulong>> ReplyThreadAsync (ulong id, MessageContent content)
		{
			return await this.SendEndpointAsync<ulong> (Endpoints.REPLIES, HttpMethod.Post, replacements: new () {
				{ "{id}", id.ToString () },
				{ "{id2}", "replies" }
			}, body: content);
		}
	}
}

