using System;
namespace mcm4csharp.v1.Data.Resources {
	public struct Review {
		public uint ReviewId { get; set; }
		public uint ReviewerId { get; set; }
		public uint ReviewDate { get; set; }
		public uint Rating { get; set; }
		public string Message { get; set; }
		public string Response { get; set; }
	}
}

