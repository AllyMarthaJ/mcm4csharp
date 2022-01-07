using System;
namespace mcm4csharp.v1.Data.Members {
	public struct ProfilePost {
		public uint ProfilePostId { get; set; }
		public uint AuthorId { get; set; }
		public uint PostDate { get; set; }
		public string Message { get; set; }
		public uint CommentCount { get; set; }
	}
}

