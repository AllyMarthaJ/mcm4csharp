using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Version {
		[JsonPropertyName ("version_id")]
		public uint VersionId { get; set; }

		[JsonPropertyName ("update_id")]
		public uint UpdateId { get; set; }

		[JsonPropertyName ("name")]
		public string Name { get; set; }

		[JsonPropertyName ("release_date")]
		public uint ReleaseDate { get; set; }

		[JsonPropertyName ("download_count")]
		public uint DownloadCount { get; set; }
	}
}

