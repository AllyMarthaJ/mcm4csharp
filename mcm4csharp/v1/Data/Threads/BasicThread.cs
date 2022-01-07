using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Threads {
	public struct BasicThread {
		[JsonPropertyName ("thread_id")]
		public uint ThreadId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("reply_count")]
		public uint ReplyCount { get; set; }

		[JsonPropertyName ("view_count")]
		public uint ViewCount { get; set; }

		[JsonPropertyName ("creation_date")]
		public uint CreationDate { get; set; }

		[JsonPropertyName ("last_message_date")]
		public uint LastMessageDate { get; set; }
	}
}

