using System;
namespace mcm4csharp.v1.Data.Resources {
	public struct Purchase {
		public uint PurchaseId { get; set; }
		public uint PurchaserId { get; set; }
		public uint LicenseId { get; set; }
		public bool Renewal { get; set; }
		public string Status { get; set; }
		public float Price { get; set; }
		public string Currency { get; set; }
		public uint PurchaseDate { get; set; }
		public uint ValidationDate { get; set; }
	}
}

