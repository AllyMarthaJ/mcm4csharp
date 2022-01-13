using System;
namespace mcm4csharp.v1.Eventful {
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

