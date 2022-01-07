using System;
namespace mcm4csharp.v1.Data.Conversations {
	public struct Reply {
		public uint MessageId { get; set; }
		public uint MessageDate { get; set; }
		public uint AuthorId { get; set; }
		public string Message { get; set; }
	}
}

