using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Members {
	public struct Member {
		[JsonPropertyName ("member_id")]
		public uint MemberId { get; set; }

		[JsonPropertyName ("username")]
		public string Username { get; set; }

		[JsonPropertyName ("join_date")]
		public uint JoinDate { get; set; }

		[JsonPropertyName ("last_activity_date")]
		public uint? LastActivityDate { get; set; }

		[JsonPropertyName ("banned")]
		public bool Banned { get; set; }

		[JsonPropertyName ("restricted")]
		public bool Restricted { get; set; }

		[JsonPropertyName ("disabled")]
		public bool Disabled { get; set; }

		[JsonPropertyName ("post_count")]
		public uint PostCount { get; set; }

		[JsonPropertyName ("resource_count")]
		public uint ResourceCount { get; set; }

		[JsonPropertyName ("purchase_count")]
		public uint PurchaseCount { get; set; }

		[JsonPropertyName ("feedback_positive")]
		public uint PositiveFeedback { get; set; }

		[JsonPropertyName ("feedback_neutral")]
		public uint NeutralFeedback { get; set; }

		[JsonPropertyName ("feedback_negative")]
		public uint NegativeFeedback { get; set; }
	}
}

