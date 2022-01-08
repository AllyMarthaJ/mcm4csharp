using System;
namespace mcm4csharp.v1.Client {
	public class SortableFields {
		/// <summary>
		/// Supports: Fetch alerts.
		/// </summary>
		public static readonly string CausedMemberId = "caused_member_id";

		/// <summary>
		/// Supports: Fetch alerts.
		/// </summary>
		public static readonly string ContentType = "content_type";

		/// <summary>
		/// Supports: Fetch alerts.
		/// </summary>
		public static readonly string ContentId = "content_id";

		/// <summary>
		/// Supports: Fetch alerts.
		/// </summary>
		public static readonly string AlertType = "alert_type";

		/// <summary>
		/// Supports: Fetch alerts.
		/// </summary>
		public static readonly string AlertDate = "alert_date";

		/// <summary>
		/// Supports: Fetch unread conversations.
		/// </summary>
		public static readonly string CreationDate = "creation_date";

		/// <summary>
		/// Supports: Fetch unread conversations.
		/// </summary>
		public static readonly string CreatorId = "creator_id";

		/// <summary>
		/// Supports: Fetch unread conversations, resources, updates, threads.
		/// </summary>
		public static readonly string Title = "title";

		/// <summary>
		/// Supports: Fetch unread conversations, threads.
		/// </summary>
		public static readonly string ReplyCount = "reply_count";

		/// <summary>
		/// Supports: Fetch unread conversations, threads.
		/// </summary>
		public static readonly string LastMessageDate = "last_message_date";

		/// <summary>
		/// Supports: Fetch unread conversations.
		/// </summary>
		public static readonly string LastReadDate = "last_read_date";

		/// <summary>
		/// Supports: Fetch unread replies to conversation, profile posts,
		/// thread replies.
		/// </summary>
		public static readonly string AuthorId = "author_id";

		/// <summary>
		/// Supports: Fetch unread replies to conversation.
		/// </summary>
		public static readonly string MessageDate = "message_date";

		/// <summary>
		/// Supports: Fetch unread replies to conversation, profile posts,
		/// thread replies,
		/// updates, reviews.
		/// </summary>
		public static readonly string Message = "message";

		/// <summary>
		/// Supports: Fetch profile posts, thread replies.
		/// </summary>
		public static readonly string PostDate = "post_date";

		/// <summary>
		/// Supports: Fetch unread replies to conversation.
		/// </summary>
		public static readonly string CommentCount = "comment_count";

		/// <summary>
		/// Supports: Fetch resources.
		/// </summary>
		public static readonly string LastUpdateDate = "last_update_date";

		/// <summary>
		/// Supports: Fetch resources.
		/// </summary>
		public static readonly string SubmissionDate = "submission_date";

		/// <summary>
		/// Supports: Fetch resources.
		/// </summary>
		public static readonly string Downloads = "downloads";

		/// <summary>
		/// Supports: Fetch resources, reviews.
		/// </summary>
		public static readonly string Rating = "rating";

		/// <summary>
		/// Supports: Fetch resources.
		/// </summary>
		public static readonly string PurchaseCount = "purchase_count";

		/// <summary>
		/// Supports: Fetch resources, purchases.
		/// </summary>
		public static readonly string Price = "price";

		/// <summary>
		/// Supports: Fetch versions.
		/// </summary>
		public static readonly string ReleaseDate = "release_date";

		/// <summary>
		/// Supports: Fetch versions.
		/// </summary>
		public static readonly string Name = "name";

		/// <summary>
		/// Supports: Fetch versions.
		/// </summary>
		public static readonly string DownloadCount = "download_count";

		/// <summary>
		/// Supports: Fetch updates.
		/// </summary>
		public static readonly string UpdateDate = "update_date";

		/// <summary>
		/// Supports: Fetch reviews.
		/// </summary>
		public static readonly string ReviewDate = "review_date";

		/// <summary>
		/// Supports: Fetch reviews.
		/// </summary>
		public static readonly string ReviewerId = "reviewer_id";

		/// <summary>
		/// Supports: Fetch reviews.
		/// </summary>
		public static readonly string Response = "response";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string PurchaseDate = "purchase_date";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string ValidationDate = "validation_date";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string PurchaseId = "purchase_id";

		/// <summary>
		/// Supports: Fetch purchases, licenses.
		/// </summary>
		public static readonly string PurchaserId = "purchaser_id";

		/// <summary>
		/// Supports: Fetch purchases, licenses.
		/// </summary>
		public static readonly string LicenseId = "license_id";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string Renewal = "renewal";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string Status = "status";

		/// <summary>
		/// Supports: Fetch purchases.
		/// </summary>
		public static readonly string Currency = "currency";

		/// <summary>
		/// Supports: Fetch licenses.
		/// </summary>
		public static readonly string StartEnd = "start_end";

		/// <summary>
		/// Supports: Fetch licenses.
		/// </summary>
		public static readonly string EndDate = "end_date";

		/// <summary>
		/// Supports: Fetch licenses.
		/// </summary>
		public static readonly string PreviousEndDate = "previous_end_date";

		/// <summary>
		/// Supports: Fetch licenses.
		/// </summary>
		public static readonly string Validated = "validated";

		/// <summary>
		/// Supports: Fetch licenses.
		/// </summary>
		public static readonly string Active = "active";

		/// <summary>
		/// Supports: Fetch downloads.
		/// </summary>
		public static readonly string DownloadDate = "download_date";

		/// <summary>
		/// Supports: Fetch downloads.
		/// </summary>
		public static readonly string VersionId = "version_id";

		/// <summary>
		/// Supports: Fetch downloads.
		/// </summary>
		public static readonly string DownloaderId = "downloader_id";

		/// <summary>
		/// Supports: Fetch threads.
		/// </summary>
		public static readonly string ViewCount = "view_count";

		/// <summary>
		/// Supports: Fetch threads.
		/// </summary>
		public static readonly string ThreadOpen = "thread_open";

		public static string DefaultAlerts() => AlertDate;
		public static string DefaultUnreadConversations () => LastMessageDate;
		public static string DefaultUnreadReplies () => MessageDate;
		public static string DefaultProfilePosts () => PostDate;
		public static string DefaultResources () => LastUpdateDate;
		public static string DefaultVersions () => ReleaseDate;
		public static string DefaultUpdates () => UpdateDate;
		public static string DefaultReviews () => ReviewDate;
		public static string DefaultPurchases () => PurchaseDate;
		public static string DefaultLicenses () => StartEnd;
		public static string DefaultResourceDownloads () => DownloadDate;
		public static string DefaultThreads () => CreationDate;
		public static string DefaultThreadReplies () => PostDate;

		public static readonly string [] Alerts = new [] { CausedMemberId, ContentType, ContentId, AlertType, AlertDate };
		public static readonly string [] UnreadConversations = new [] {CreationDate, CreatorId, Title, ReplyCount, LastMessageDate, LastReadDate };
		public static readonly string [] UnreadReplies = new [] { AuthorId, MessageDate, Message };
		public static readonly string [] ProfilePosts = new [] { AuthorId, PostDate, Message, CommentCount };
		public static readonly string [] Resources = new [] { LastUpdateDate, SubmissionDate, Title, Downloads, Rating, PurchaseCount, Price };
		public static readonly string [] Versions = new [] { ReleaseDate, Name, DownloadCount };
		public static readonly string [] Updates = new [] { UpdateDate, Title, Message };
		public static readonly string [] Reviews = new [] { ReviewDate, ReviewerId, Rating, Message, Response };
		public static readonly string [] Purchases = new [] { PurchaseDate, ValidationDate, PurchaseId, PurchaserId, LicenseId, Renewal, Status, Price, Currency };
		public static readonly string [] Licenses = new [] { StartEnd, EndDate, PreviousEndDate, LicenseId, PurchaserId, Validated, Active };
		public static readonly string [] ResourceDownloads = new [] { DownloadDate, VersionId, DownloaderId };
		public static readonly string [] Threads = new [] { Title, ReplyCount, ViewCount, CreationDate, LastMessageDate, ThreadOpen };
		public static readonly string [] ThreadReplies = new [] { AuthorId, PostDate, Message };
	}
}

