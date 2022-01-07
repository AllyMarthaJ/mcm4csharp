using System;
namespace mcm4csharp.v1.Data.Resources
{
	public struct License {
		public uint LicenseId { get; set; }
		public uint PurchaserId { get; set; }
		public bool Validated { get; set; }
		public bool Active { get; set; }
		public uint StartDate { get; set; }
		public uint EndDate { get; set; }
		public uint PreviousEndDate { get; set; }
	}
}

