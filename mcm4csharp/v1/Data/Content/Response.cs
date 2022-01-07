using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct Response<T> {
		[JsonPropertyName ("result")]
		public string Result { get; set; }

		[JsonPropertyName ("data")]
		public T Data { get; set; }

		[JsonPropertyName ("error")]
		public Error Error { get; set; }

		// header value
		public uint ReplyAfterMilliseconds { get; set; }
	}
}

