using System.Text.Json.Serialization;

namespace NicoNicoNii.Entities.JSON;

public sealed class SessionKeepAlive
{
    [JsonPropertyName("niconico_response")]
    public NiconicoResponseClass NiconicoResponse { get; set; }

    public sealed class NiconicoResponseClass
    {
        [JsonPropertyName("@status")]
        public string Status { get; set; }

        [JsonPropertyName("error")]
        public ErrorClass Error { get; set; }
    }

    public sealed class ErrorClass
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
