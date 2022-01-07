using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Threads {
	public class Reply {
		[JsonPropertyName ("reply_id")]
		public uint ReplyId { get; set; }

		[JsonPropertyName ("author_id")]
		public uint AuthorId { get; set; }

		[JsonPropertyName ("post_date")]
		public uint PostDate { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }
	}
}

