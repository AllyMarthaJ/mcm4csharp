using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Members {
	public struct Member {
		[JsonPropertyName ("member_id")]
		public ulong MemberId { get; set; }

		[JsonPropertyName ("username")]
		public string Username { get; set; }

		[JsonPropertyName ("join_date")]
		public ulong JoinDate { get; set; }

		[JsonPropertyName ("last_activity_date")]
		public ulong? LastActivityDate { get; set; }

		[JsonPropertyName ("banned")]
		public bool Banned { get; set; }

		[JsonPropertyName ("restricted")]
		public bool Restricted { get; set; }

		[JsonPropertyName ("disabled")]
		public bool Disabled { get; set; }

		[JsonPropertyName ("post_count")]
		public ulong PostCount { get; set; }

		[JsonPropertyName ("resource_count")]
		public ulong ResourceCount { get; set; }

		[JsonPropertyName ("purchase_count")]
		public ulong PurchaseCount { get; set; }

		[JsonPropertyName ("feedback_positive")]
		public ulong PositiveFeedback { get; set; }

		[JsonPropertyName ("feedback_neutral")]
		public ulong NeutralFeedback { get; set; }

		[JsonPropertyName ("feedback_negative")]
		public ulong NegativeFeedback { get; set; }
	}
}

