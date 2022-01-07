using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Conversations {
	public struct Reply {
		[JsonPropertyName ("message_id")]
		public uint MessageId { get; set; }

		[JsonPropertyName ("message_date")]
		public uint MessageDate { get; set; }

		[JsonPropertyName ("author_id")]
		public uint AuthorId { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }
	}
}

