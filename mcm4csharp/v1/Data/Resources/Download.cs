using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Download {
		[JsonPropertyName ("download_id")]
		public ulong DownloadId { get; set; }

		[JsonPropertyName ("version_id")]
		public ulong VersionId { get; set; }

		[JsonPropertyName ("downloader_id")]
		public ulong DownloaderId { get; set; }

		[JsonPropertyName ("download_date")]
		public ulong DownloadDate { get; set; }
	}
}

