using System;
using System.Collections.Concurrent;
using mcm4csharp.v1.Client;

namespace mcm4csharp.v1.Eventful {
	public class ApiListener {
		private ApiClient client;
		private BlockingCollection<dynamic> subscriptionProviders { get; set; } = new BlockingCollection<dynamic> ();
		private bool stop = false;

		public int ReadDelay { get; set; } = 30000;


		public void AddSubscriptionProvider<S, T> (ISubscriptionProvider<S, T> provider)
		{
			this.subscriptionProviders.Add (provider);
		}

		public void Stop ()
		{
			this.stop = true;
		}

		public ApiListener (ApiClient client)
		{
			this.client = client;

			var readThread = new Thread (async () => await readForever ());
			readThread.Start ();
		}

		private async Task readForever ()
		{
			while (!stop) {
				foreach (var provider in this.subscriptionProviders) {
					await provider.UpdateDataAsync (this.client);
				}

				await Task.Delay (this.ReadDelay);
			}
		}
	}
}

