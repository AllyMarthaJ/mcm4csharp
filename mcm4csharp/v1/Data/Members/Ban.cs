using System;
namespace mcm4csharp.v1.Data.Members {
	public struct Ban {
		public uint MemberId { get; set; }
		public uint BannedById { get; set; }
		public uint BannedDate { get; set; }
		public string Reason { get; set; }
	}
}

