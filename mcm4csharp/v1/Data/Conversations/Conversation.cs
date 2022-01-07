using System;
namespace mcm4csharp.v1.Data.Conversations {
	public struct Conversation {
		public uint ConversationId { get; set; }
		public string Title { get; set; }
		public uint CreationDate { get; set; }
		public uint CreatorId { get; set; }
		public uint LastMessageDate { get; set; }
		public uint LastReadDate { get; set; }
		public bool Open { get; set; }
		public uint ReplyCount { get; set; }
		public uint [] RecipientIds { get; set; }
	}
}

