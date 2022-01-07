using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Members {
	public struct Ban {
		[JsonPropertyName ("member_id")]
		public uint MemberId { get; set; }

		[JsonPropertyName ("banned_by_id")]
		public uint BannedById { get; set; }

		[JsonPropertyName ("ban_date")]
		public uint BannedDate { get; set; }

		[JsonPropertyName ("reason")]
		public string Reason { get; set; }
	}
}

