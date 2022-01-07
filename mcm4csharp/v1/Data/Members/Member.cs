using System;
namespace mcm4csharp.v1.Data.Members {
	public struct Member {
		public uint MemberId { get; set; }
		public string Username { get; set; }
		public uint JoinDate { get; set; }
		public uint? LastActivityDate { get; set; }
		public bool Banned { get; set; }
		public bool Restricted { get; set; }
		public bool Disabled { get; set; }
		public uint PostCount { get; set; }
		public uint ResourceCount { get; set; }
		public uint PurchaseCount { get; set; }
		public uint PositiveFeedback { get; set; }
		public uint NeutralFeedback { get; set; }
		public uint NegativeFeedback { get; set; }
	}
}

