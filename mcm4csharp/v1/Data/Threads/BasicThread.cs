using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Threads {
	public struct BasicThread {
		[JsonPropertyName ("thread_id")]
		public ulong ThreadId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("reply_count")]
		public ulong ReplyCount { get; set; }

		[JsonPropertyName ("view_count")]
		public ulong ViewCount { get; set; }

		[JsonPropertyName ("creation_date")]
		public ulong CreationDate { get; set; }

		[JsonPropertyName ("last_message_date")]
		public ulong LastMessageDate { get; set; }
	}
}

