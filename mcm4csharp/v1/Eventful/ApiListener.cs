using System;
using System.Collections.Concurrent;
using mcm4csharp.v1.Client;

namespace mcm4csharp.v1.Eventful {
	/// <summary>
	/// Readonly forever-listener for the API tracking changes via subscription providers.
	/// </summary>
	public class ApiListener {
		private ApiClient client;
		private BlockingCollection<dynamic> subscriptionProviders { get; set; } = new BlockingCollection<dynamic> ();
		private bool stop = false;

		/// <summary>
		/// Time in milliseconds between attempting to read from each subscription provider.
		/// </summary>
		public int ReadDelay { get; set; } = 30000;

		/// <summary>
		/// Add a subscription provider to watch.
		/// </summary>
		/// <typeparam name="S">Source type</typeparam>
		/// <typeparam name="T">Content type</typeparam>
		/// <param name="provider">The provider to watch.</param>
		public void AddSubscriptionProvider<S, T> (ISubscriptionProvider<S, T> provider)
		{
			this.subscriptionProviders.Add (provider);
		}

		/// <summary>
		/// Stop listening forever.
		/// </summary>
		public void Stop ()
		{
			this.stop = true;
		}

		/// <summary>
		/// Start listening forever.
		/// </summary>
		public void Start()
		{
			this.stop = false;

			var readThread = new Thread (async () => await readForever ());
			readThread.Start ();
		}

		public ApiListener (ApiClient client)
		{
			this.client = client;

			this.Start ();
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

