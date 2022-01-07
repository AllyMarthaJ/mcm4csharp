using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Threads {
	public struct Thread {
		[JsonPropertyName ("thread_id")]
		public uint ThreadId { get; set; }

		[JsonPropertyName ("forum_name")]
		public string ForumName { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("reply_count")]
		public uint ReplyCount { get; set; }

		[JsonPropertyName ("view_count")]
		public uint ViewCount { get; set; }

		[JsonPropertyName ("post_date")]
		public uint PostDate { get; set; }

		[JsonPropertyName ("thread_type")]
		public string ThreadType { get; set; }

		[JsonPropertyName ("thread_open")]
		public bool ThreadOpen { get; set; }

		[JsonPropertyName ("last_post_date")]
		public uint LastPostDate { get; set; }
	}
}

