using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Conversations {
	public struct Reply {
		[JsonPropertyName ("message_id")]
		public ulong MessageId { get; set; }

		[JsonPropertyName ("message_date")]
		public ulong MessageDate { get; set; }

		[JsonPropertyName ("author_id")]
		public ulong AuthorId { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }
	}
}

