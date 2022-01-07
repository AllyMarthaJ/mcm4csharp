using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Alerts {
	public struct Alert {
		[JsonPropertyName ("caused_member_id")]
		public uint CausedMemberId { get; set; }

		[JsonPropertyName ("content_type")]
		public string ContentType { get; set; }

		[JsonPropertyName ("content_id")]
		public uint ContentId { get; set; }

		[JsonPropertyName ("alert_type")]
		public string AlertType { get; set; }

		[JsonPropertyName ("alert_date")]
		public uint AlertDate { get; set; }
	}
}

