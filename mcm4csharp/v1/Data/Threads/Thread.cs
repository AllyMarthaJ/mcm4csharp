using System;
namespace mcm4csharp.v1.Data.Threads {
	public struct Thread {
		public uint ThreadId { get; set; }
		public string ForumName { get; set; }
		public string Title { get; set; }
		public uint ReplyCount { get; set; }
		public uint ViewCount { get; set; }
		public uint PostDate { get; set; }
		public string ThreadType { get; set; }
		public bool ThreadOpen { get; set; }
		public uint LastPostDate { get; set; }
	}
}

