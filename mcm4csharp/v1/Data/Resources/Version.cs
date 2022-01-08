using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Version {
		[JsonPropertyName ("version_id")]
		public ulong VersionId { get; set; }

		[JsonPropertyName ("update_id")]
		public ulong UpdateId { get; set; }

		[JsonPropertyName ("name")]
		public string Name { get; set; }

		[JsonPropertyName ("release_date")]
		public ulong ReleaseDate { get; set; }

		[JsonPropertyName ("download_count")]
		public ulong DownloadCount { get; set; }
	}
}

