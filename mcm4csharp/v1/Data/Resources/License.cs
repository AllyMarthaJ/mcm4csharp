using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct License {
		[JsonPropertyName ("license_id")]
		public ulong LicenseId { get; set; }

		[JsonPropertyName ("purchaser_id")]
		public ulong PurchaserId { get; set; }

		// some bizarre choice for "Get resource license by member", add optional thing here.
		[JsonPropertyName("purchaser_name")]
		public string PurchaserName { get; set; }

		[JsonPropertyName ("validated")]
		public bool Validated { get; set; }

		[JsonPropertyName ("active")]
		public bool Active { get; set; }

		[JsonPropertyName ("start_date")]
		public ulong StartDate { get; set; }

		[JsonPropertyName ("end_date")]
		public ulong EndDate { get; set; }

		[JsonPropertyName ("previous_end_date")]
		public ulong PreviousEndDate { get; set; }
	}
}

