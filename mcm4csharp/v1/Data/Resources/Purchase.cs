using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Purchase {
		[JsonPropertyName ("purchase_id")]
		public uint PurchaseId { get; set; }

		[JsonPropertyName ("purchaser_id")]
		public uint PurchaserId { get; set; }

		[JsonPropertyName ("license_id")]
		public uint LicenseId { get; set; }

		[JsonPropertyName ("renewal")]
		public bool Renewal { get; set; }

		[JsonPropertyName ("status")]
		public string Status { get; set; }

		[JsonPropertyName ("price")]
		public float Price { get; set; }

		[JsonPropertyName ("currency")]
		public string Currency { get; set; }

		[JsonPropertyName ("purchase_date")]
		public uint PurchaseDate { get; set; }

		[JsonPropertyName ("validation_date")]
		public uint ValidationDate { get; set; }
	}
}

