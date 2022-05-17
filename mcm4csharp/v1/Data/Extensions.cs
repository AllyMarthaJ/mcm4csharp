using System;
using mcm4csharp.v1.Data.Content;

namespace mcm4csharp.v1.Data
{
	public static class Extensions
	{
		public static Dictionary<string, string> ToDict (this Sortable sortable) =>
			new () {
				{ "sort", sortable.Sort },
				{ "order", sortable.Order },
				{ "page", sortable.Page.ToString () }
			};
	}
}

