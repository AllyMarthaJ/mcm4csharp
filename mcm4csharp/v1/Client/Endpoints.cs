﻿using System;
namespace mcm4csharp.v1.Client
{
	public class Endpoints
	{
		public const string HEALTH = "v1/health";
		public const string ALERTS = "v1/alerts";
		public const string CONVERSATIONS = "v1/conversations";
		public const string CONVERSATIONS_ID = "v1/conversations/{id}/replies";
		public const string MEMBERS = "v1/members/{id}";
		public const string PROFILE_POSTS = "v1/members/self/profile-posts/{id}";
		public const string RESOURCES = "v1/resources/{id}";
		public const string VERSIONS = "v1/resources/{r_id}/versions/{v_id}";
		public const string UPDATES = "v1/resources/{r_id}/updates/{u_id}";
	}
}

