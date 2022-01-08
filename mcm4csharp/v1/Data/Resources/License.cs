using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct License {
		[JsonPropertyName ("license_id")]
		public uint LicenseId { get; set; }

		[JsonPropertyName ("purchaser_id")]
		public uint PurchaserId { get; set; }

		// some bizarre choice for "Get resource license by member", add optional thing here.
		[JsonPropertyName("purchaser_name")]
		public string PurchaserName { get; set; }

		[JsonPropertyName ("validated")]
		public bool Validated { get; set; }

		[JsonPropertyName ("active")]
		public bool Active { get; set; }

		[JsonPropertyName ("start_date")]
		public uint StartDate { get; set; }

		[JsonPropertyName ("end_date")]
		public uint EndDate { get; set; }

		[JsonPropertyName ("previous_end_date")]
		public uint PreviousEndDate { get; set; }
	}
}

