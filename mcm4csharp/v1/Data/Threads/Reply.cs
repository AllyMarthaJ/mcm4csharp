using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Threads {
	public struct Reply {
		[JsonPropertyName ("reply_id")]
		public ulong ReplyId { get; set; }

		[JsonPropertyName ("author_id")]
		public ulong AuthorId { get; set; }

		[JsonPropertyName ("post_date")]
		public ulong PostDate { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }
	}
}

