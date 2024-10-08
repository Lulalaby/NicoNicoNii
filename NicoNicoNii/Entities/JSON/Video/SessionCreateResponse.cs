﻿using System.Text.Json.Serialization;

namespace NicoNicoNii.Entities.JSON.Video;

public sealed class SessionCreateResponse
{
    [JsonPropertyName("meta")]
    public MetaClass Meta { get; set; }

    [JsonPropertyName("data")]
    public DataClass Data { get; set; }

    public sealed class DataClass
    {
        [JsonPropertyName("session")]
        public Session Session { get; set; }
    }

    public sealed class Session
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("recipe_id")]
        public string RecipeId { get; set; }

        [JsonPropertyName("content_id")]
        public string ContentId { get; set; }

        [JsonPropertyName("content_src_id_sets")]
        public List<ContentSrcIdSet> ContentSrcIdSets { get; set; } = new();

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("timing_constraint")]
        public string TimingConstraint { get; set; }

        [JsonPropertyName("keep_method")]
        public KeepMethod KeepMethod { get; set; }

        [JsonPropertyName("protocol")]
        public Protocol Protocol { get; set; }

        [JsonPropertyName("play_seek_time")]
        public long? PlaySeekTime { get; set; }

        [JsonPropertyName("play_speed")]
        public double? PlaySpeed { get; set; }

        [JsonPropertyName("play_control_range")]
        public PlayControlRange PlayControlRange { get; set; }

        [JsonPropertyName("content_uri")]
        public Uri ContentUri { get; set; }

        [JsonPropertyName("session_operation_auth")]
        public SessionOperationAuth SessionOperationAuth { get; set; }

        [JsonPropertyName("content_auth")]
        public ContentAuth ContentAuth { get; set; }

        [JsonPropertyName("runtime_info")]
        public RuntimeInfo RuntimeInfo { get; set; }

        [JsonPropertyName("client_info")]
        public ClientInfo ClientInfo { get; set; }

        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("modified_time")]
        public long? ModifiedTime { get; set; }

        [JsonPropertyName("priority")]
        public double? Priority { get; set; }

        [JsonPropertyName("content_route")]
        public long? ContentRoute { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("content_status")]
        public string ContentStatus { get; set; }
    }

    public sealed class ClientInfo
    {
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }

        [JsonPropertyName("remote_ip")]
        public string RemoteIp { get; set; }

        [JsonPropertyName("tracking_info")]
        public string TrackingInfo { get; set; }
    }

    public sealed class ContentAuth
    {
        [JsonPropertyName("auth_type")]
        public string AuthType { get; set; }

        [JsonPropertyName("max_content_count")]
        public long? MaxContentCount { get; set; }

        [JsonPropertyName("content_key_timeout")]
        public long? ContentKeyTimeout { get; set; }

        [JsonPropertyName("service_id")]
        public string ServiceId { get; set; }

        [JsonPropertyName("service_user_id")]
        public string ServiceUserId { get; set; }

        [JsonPropertyName("content_auth_info")]
        public ContentAuthInfo ContentAuthInfo { get; set; }
    }

    public sealed class ContentAuthInfo
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public sealed class ContentSrcIdSet
    {
        [JsonPropertyName("content_src_ids")]
        public List<ContentSrcId> ContentSrcIds { get; set; } = new();

        [JsonPropertyName("allow_subset")]
        public string AllowSubset { get; set; }
    }

    public sealed class ContentSrcId
    {
        [JsonPropertyName("src_id_to_mux")]
        public SrcIdToMux SrcIdToMux { get; set; }
    }

    public sealed class SrcIdToMux
    {
        [JsonPropertyName("video_src_ids")]
        public List<string> VideoSrcIds { get; set; } = new();

        [JsonPropertyName("audio_src_ids")]
        public List<string> AudioSrcIds { get; set; } = new();
    }

    public sealed class KeepMethod
    {
        [JsonPropertyName("heartbeat")]
        public Heartbeat Heartbeat { get; set; }
    }

    public sealed class Heartbeat
    {
        [JsonPropertyName("lifetime")]
        public long? Lifetime { get; set; }

        [JsonPropertyName("onetime_token")]
        public string OnetimeToken { get; set; }

        [JsonPropertyName("deletion_timeout_on_no_stream")]
        public long? DeletionTimeoutOnNoStream { get; set; }
    }

    public sealed class PlayControlRange
    {
        [JsonPropertyName("max_play_speed")]
        public double? MaxPlaySpeed { get; set; }

        [JsonPropertyName("min_play_speed")]
        public double? MinPlaySpeed { get; set; }
    }

    public sealed class Protocol
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parameters")]
        public ProtocolParameters Parameters { get; set; }
    }

    public sealed class ProtocolParameters
    {
        [JsonPropertyName("http_parameters")]
        public HttpParameters HttpParameters { get; set; }
    }

    public sealed class HttpParameters
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("parameters")]
        public HttpParametersParameters Parameters { get; set; }
    }

    public sealed class HttpParametersParameters
    {
        [JsonPropertyName("http_output_download_parameters")]
        public HttpOutputDownloadParameters HttpOutputDownloadParameters { get; set; }
    }

    public sealed class HttpOutputDownloadParameters
    {
        [JsonPropertyName("file_extension")]
        public string FileExtension { get; set; }

        [JsonPropertyName("transfer_preset")]
        public string TransferPreset { get; set; }

        [JsonPropertyName("use_ssl")]
        public string UseSsl { get; set; }

        [JsonPropertyName("use_well_known_port")]
        public string UseWellKnownPort { get; set; }
    }

    public sealed class RuntimeInfo
    {
        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("execution_history")]
        public List<object> ExecutionHistory { get; set; } = new();

        [JsonPropertyName("thumbnailer_state")]
        public List<object> ThumbnailerState { get; set; } = new();
    }

    public sealed class SessionOperationAuth
    {
        [JsonPropertyName("session_operation_auth_by_signature")]
        public SessionOperationAuthBySignature SessionOperationAuthBySignature { get; set; }
    }

    public sealed class SessionOperationAuthBySignature
    {
        [JsonPropertyName("created_time")]
        public long? CreatedTime { get; set; }

        [JsonPropertyName("expire_time")]
        public long? ExpireTime { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }

    public sealed class MetaClass
    {
        [JsonPropertyName("status")]
        public long? Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
