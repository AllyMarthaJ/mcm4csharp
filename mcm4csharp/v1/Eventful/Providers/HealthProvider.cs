using System;
using mcm4csharp.v1.Client;
using mcm4csharp.v1.Data.Content;

namespace mcm4csharp.v1.Eventful.Providers
{
	/// <summary>
	/// <para>Subscription provider which tracks the APIs health.</para>
	/// <para>Intended use is example only, does not respect (un-)subscription.</para>
	/// </summary>
	public sealed class HealthProvider : ISubscriptionProvider<int, string> {
		private string? healthTracker = null;

		public event EventHandler<SubscribeEventArgs<int, string>> FetchSuccess;
		public event EventHandler<SubscribeEventArgs<int, Error>> FetchFailed;
		public event EventHandler<SubscribeEventArgs<int, string>> DataChanged;

		public int ItemDelay { get; set; } = 3000;

		public async Task UpdateDataAsync (ApiClient client)
		{
			var safeRequest = async () => await client.GetHealthAsync ();
			var exec = await client.SafeRequestAsync (safeRequest);

			if (exec.Result != "success") {
				OnFetchFailed (new SubscribeEventArgs<int, Error> (0, exec.Error));
				return;
			} else {
				OnFetchSuccess (new SubscribeEventArgs<int, string> (0, exec.Data));
				if (exec.Data != healthTracker) {
					OnDataChanged (new SubscribeEventArgs<int, string> (0, exec.Data));
					healthTracker = exec.Data;
				}
			}
		}

		public void Subscribe (int instance)
		{
			return;
		}

		public void Unsubscribe (int instance)
		{
			return;
		}

		protected void OnFetchSuccess (SubscribeEventArgs<int, string> args) => FetchSuccess?.Invoke (this, args);

		protected void OnFetchFailed (SubscribeEventArgs<int, Error> args) => FetchFailed?.Invoke (this, args);

		protected void OnDataChanged (SubscribeEventArgs<int, string> args) => DataChanged?.Invoke (this, args);
	}
}

