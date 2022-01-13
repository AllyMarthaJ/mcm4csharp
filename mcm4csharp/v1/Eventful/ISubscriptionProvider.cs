using System;
using System.Collections;
using mcm4csharp.v1.Client;
using mcm4csharp.v1.Data.Content;

namespace mcm4csharp.v1.Eventful {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="S">Source type for service requests.</typeparam>
	/// <typeparam name="T">Content type for service responses.</typeparam>
	public interface ISubscriptionProvider<S, T> {
		// Implementation should be as thus:
		// private Dictionary<S, T> sourceContentTracker = new();

		/// <summary>
		/// Raised when a request is made and is successful (not include ratelimiting).
		/// </summary>
		public event EventHandler<SubscribeEventArgs<S, T>> FetchSuccess;

		/// <summary>
		/// Raised when a request is made and is unsuccessful (not including ratelimiting).
		/// </summary>
		public event EventHandler<SubscribeEventArgs<S, Error>> FetchFailed;

		/// <summary>
		/// Raised when a request is made and there are changes to the previous result.
		/// </summary>
		public event EventHandler<SubscribeEventArgs<S, T>> DataChanged;

		/// <summary>
		/// Delay in milliseconds between checking each respective source.
		/// A source is is an element of a subscription provider. 
		/// </summary>
		public int ItemDelay { get; set; }

		/// <summary>
		/// Updates the available data to the subscription provider.
		/// It's the responsiblity of the provider to raise events, to keep everything self-contained.
		/// *Must* make safe calls to the API to prevent ratelimiting!
		/// </summary>
		/// <param name="client">Client to update the data with.</param>
		/// <returns></returns>
		public Task UpdateDataAsync (ApiClient client);

		/// <summary>
		/// Subscribe to a particular source, such as a thread or conversation.
		/// </summary>
		/// <param name="source">Source to subscribe to.</param>
		public void Subscribe (S source);

		/// <summary>
		/// Unsubscribe from a particular source.
		/// </summary>
		/// <param name="source">Source to unsubscribe from.</param>
		public void Unsubscribe (S source);
	}
}

