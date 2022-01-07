using System;
namespace mcm4csharp.v1.Data.Resources {
	public struct Resource {
		public uint ResourceId { get; set; }
		public uint AuthorId { get; set; }
		public string Title { get; set; }
		public string TagLine { get; set; }
		public string Description { get; set; }
		public uint ReleaseDate { get; set; }
		public uint LastReleaseDate { get; set; }
		public string CategoryTitle { get; set; }
		public uint CurrentVersionId { get; set; }
		public float Price { get; set; }
		public string Currency { get; set; }
		public uint PurchaseCount { get; set; }
		public uint DownloadCount { get; set; }
		public uint ReviewCount { get; set; }
		public float ReviewAverage { get; set; }
	}
}

