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
		public event EventHandler<SubscribeEventArgs<S, T>> FetchSuccess;
		public event EventHandler<SubscribeEventArgs<S, Error>> FetchFailed;
		public event EventHandler<SubscribeEventArgs<S, T>> DataChanged;

		public int ItemDelay { get; set; }

		/// <summary>
		/// It's the responsiblity of the provider to raise events, to keep self-contained.
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public Task UpdateDataAsync (ApiClient client);

		public void Subscribe (S source);
		public void Unsubscribe (S source);
	}

	public class SubscribeEventArgs<S, T> : EventArgs {
		public S Source { get; set; }
		public T Content { get; set; }

		public SubscribeEventArgs (S source, T content)
		{
			this.Source = source;
			this.Content = content;
		}
	}
}

