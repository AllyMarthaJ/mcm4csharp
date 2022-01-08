using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Members {
	public struct ProfilePost {
		[JsonPropertyName("profile_post_id")]
		public ulong ProfilePostId { get; set; }

		[JsonPropertyName("author_id")]
		public ulong AuthorId { get; set; }

		[JsonPropertyName("post_date")]
		public ulong PostDate { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("comment_count")]
		public ulong CommentCount { get; set; }
	}
}

