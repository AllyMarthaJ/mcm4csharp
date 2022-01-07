using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Download {
		[JsonPropertyName ("download_id")]
		public uint DownloadId { get; set; }

		[JsonPropertyName ("version_id")]
		public uint VersionId { get; set; }

		[JsonPropertyName ("downloader_id")]
		public uint DownloaderId { get; set; }

		[JsonPropertyName ("download_date")]
		public uint DownloadDate { get; set; }
	}
}

