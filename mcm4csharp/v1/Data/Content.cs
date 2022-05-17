using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Content
{
	public record struct ConversationContent([property:JsonPropertyName("recipient_ids")] ulong [] RecipientIds,
						 [property:JsonPropertyName("title")] string Title,
						 [property:JsonPropertyName("message")] string Message);

	public record struct Error([property:JsonPropertyName("code")] string Code,
				   [property:JsonPropertyName("message")] string Message);

	public record struct LicenseContent([property:JsonPropertyName("purchaser_id")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] ulong PurchaserId,
					    [property:JsonPropertyName("permanent")] bool Permanent,
					    [property:JsonPropertyName("active")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] bool Active,
					    [property:JsonPropertyName("start_date")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] ulong StartDate,
					    [property:JsonPropertyName("end_date")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] ulong EndDate);

	public record MessageContent([property:JsonPropertyName("message")] string Message);

	public record struct ResourceContent([property:JsonPropertyName("title")] string Title,
					     [property:JsonPropertyName("tag_line")] string TagLine,
					     [property:JsonPropertyName("description")] string Description);

	public record struct Response<T>([property:JsonPropertyName("result")] string Result,
					 [property:JsonPropertyName("data")] T Data,
					 [property:JsonPropertyName("error")] Error Error,
					 ulong RetryAfterMilliseconds);

	public record struct ResponseContent([property:JsonPropertyName("response")] string Response);
	
	public record struct SelfUpdateContent([property:JsonPropertyName("custom_title")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string CustomTitle,
					       [property:JsonPropertyName("about_me")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string AboutMe,
					       [property:JsonPropertyName("signature")][property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string Signature);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="Sort">What field to be sorted on.</param>
	/// <param name="Order">Order of sorting; "asc" or "desc".</param>
	/// <param name="Page">Page number >= 1.</param>
	public record struct Sortable([property:JsonPropertyName("sort")] string Sort,
				      [property:JsonPropertyName("order")] string Order,
				      [property:JsonPropertyName("page")] ulong Page);
}

