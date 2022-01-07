using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Resource {
		[JsonPropertyName ("resource_id")]
		public uint ResourceId { get; set; }

		[JsonPropertyName ("author_id")]
		public uint AuthorId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("tag_line")]
		public string TagLine { get; set; }

		[JsonPropertyName ("description")]
		public string Description { get; set; }

		[JsonPropertyName ("release_date")]
		public uint ReleaseDate { get; set; }

		[JsonPropertyName ("last_release_date")]
		public uint LastReleaseDate { get; set; }

		[JsonPropertyName ("category_title")]
		public string CategoryTitle { get; set; }

		[JsonPropertyName ("current_version_id")]
		public uint CurrentVersionId { get; set; }

		[JsonPropertyName ("price")]
		public float Price { get; set; }

		[JsonPropertyName ("currency")]
		public string Currency { get; set; }

		[JsonPropertyName ("purchase_count")]
		public uint PurchaseCount { get; set; }

		[JsonPropertyName ("download_count")]
		public uint DownloadCount { get; set; }

		[JsonPropertyName ("review_count")]
		public uint ReviewCount { get; set; }

		[JsonPropertyName ("review_average")]
		public float ReviewAverage { get; set; }
	}
}

