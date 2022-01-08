using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Resources {
	public struct Update {
		[JsonPropertyName ("update_id")]
		public ulong UpdateId { get; set; }

		[JsonPropertyName ("title")]
		public string Title { get; set; }

		[JsonPropertyName ("message")]
		public string Message { get; set; }

		[JsonPropertyName ("update_date")]
		public ulong UpdateDate { get; set; }
	}
}

