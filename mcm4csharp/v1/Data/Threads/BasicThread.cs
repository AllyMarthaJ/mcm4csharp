using System;
namespace mcm4csharp.v1.Data.Threads {
	public struct BasicThread {
		public uint ThreadId { get; set; }
		public string Title { get; set; }
		public uint ReplyCount { get; set; }
		public uint ViewCount { get; set; }
		public uint CreationDate { get; set; }
		public uint LastMessageDate { get; set; }
	}
}

