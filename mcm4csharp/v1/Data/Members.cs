using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Members
{
	public record struct Ban([property:JsonPropertyName("member_id")] ulong MemberId,
				 [property:JsonPropertyName("banned_by_id")] ulong BannedById,
				 [property:JsonPropertyName("ban_date")] ulong BannedDate,
				 [property:JsonPropertyName("reason")] string Reason);

	public record struct Member([property:JsonPropertyName("member_id")] ulong MemberId,
				    [property:JsonPropertyName("username")] string Username,
				    [property:JsonPropertyName("join_date")] ulong JoinDate,
				    [property:JsonPropertyName("last_activity_date")] ulong? LastActivityDate,
				    [property:JsonPropertyName("banned")] bool Banned,
				    [property:JsonPropertyName("suspended")] bool Suspended,
				    [property:JsonPropertyName("restricted")] bool Restricted,
				    [property:JsonPropertyName("disabled")] bool Disabled,
				    [property:JsonPropertyName("premium")] bool Premium,
				    [property:JsonPropertyName("supreme")] bool Supreme,
				    [property:JsonPropertyName("ultimate")] bool Ultimate,
				    [property:JsonPropertyName("discord_id")] ulong? DiscordId,
				    [property:JsonPropertyName("avatar_url")] string AvatarUrl,
				    [property:JsonPropertyName("post_count")] ulong PostCount,
				    [property:JsonPropertyName("resource_count")] ulong ResourceCount,
				    [property:JsonPropertyName("purchase_count")] ulong PurchaseCount,
				    [property:JsonPropertyName("feedback_positive")] ulong PositiveFeedback,
				    [property:JsonPropertyName("feedback_neutral")] ulong NeutralFeedback,
				    [property:JsonPropertyName("feedback_negative")] ulong NegativeFeedback);

	public record struct ProfilePost([property:JsonPropertyName("profile_post_id")] ulong ProfilePostId,
					 [property:JsonPropertyName("author_id")] ulong AuthorId,
					 [property:JsonPropertyName("post_date")] ulong PostDate,
					 [property:JsonPropertyName("message")] string Message,
					 [property:JsonPropertyName("comment_count")] ulong CommentCount);
}

