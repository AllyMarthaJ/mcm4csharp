using System;
using System.Text.Json.Serialization;

namespace mcm4csharp.v1.Data.Alerts
{
	public record struct Alert([property:JsonPropertyName("caused_member_id")] ulong CausedMemberId,
				   [property:JsonPropertyName("content_type")] string ContentType,
				   [property:JsonPropertyName("content_id")] ulong ContentId,
				   [property:JsonPropertyName("alert_type")] string AlertType,
				   [property:JsonPropertyName("alert_date")] ulong AlertDate);
}