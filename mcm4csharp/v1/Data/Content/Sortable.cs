using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content {
	public struct Sortable {
		/// <summary>
		/// What field to be sorted on.
		/// </summary>
		[JsonPropertyName ("sort")]
		public string Sort { get; set; }

		/// <summary>
		/// Order of sorting; "asc" or "desc".
		/// </summary>
		[JsonPropertyName ("order")]
		public string Order { get; set; }

		/// <summary>
		/// Page number >= 1.
		/// </summary>
		[JsonPropertyName ("page")]
		public ulong Page { get; set; }

		public Dictionary<string, string> ToDict () =>
			new () {
				{ "sort", Sort },
				{ "order", Order },
				{ "page", Page.ToString () }
			};
	}
}

