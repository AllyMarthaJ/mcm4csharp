using System;
namespace mcm4csharp.v1.Data.Resources {
	public struct BasicResource {
		public uint ResourceId { get; set; }
		public uint AuthorId { get; set; }
		public string Title { get; set; }
		public string TagLine { get; set; }
		public float Price { get; set; }
		public string Currency { get; set; }
	}
}

