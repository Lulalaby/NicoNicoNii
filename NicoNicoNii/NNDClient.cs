using NicoNicoNii.Entities.JSON;
using NicoNicoNii.Entities.XML;

using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace NicoNicoNii;

public class NndClient
{
    internal readonly HttpClient Client;
    internal readonly HttpClientHandler Handler;

    public NndClient()
    {
        var cc = new CookieContainer();
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            CookieContainer = cc,
            UseCookies = true,
            UseDefaultCredentials = false
        };
        this.Handler = handler;
        this.Client = new(handler);
        ApplyDefaultHeaders(this.Client);
    }

    public NndClient(HttpClientHandler handler)
    {
        this.Handler = handler;
        this.Client = new(handler);
        ApplyDefaultHeaders(this.Client);
    }

    internal DateTimeOffset? LoginDate { get; set; }

    /// <summary>
    ///     Gets the configured HTTP client used for Nico requests.
    /// </summary>
    public HttpClient HttpClient => this.Client;

    public LoginSessionData LoginSessionData { get; internal set; }

    public async Task<LoginSessionData> LoginAsync(string emailTel, string password)
    {
        using var cont = new StringContent($"mail={emailTel}&password={password}&site=nicometro", Encoding.UTF8, "application/x-www-form-urlencoded");
        using var msg = new HttpRequestMessage(HttpMethod.Post, "https://account.nicovideo.jp/login/redirector");
        msg.Content = cont;
        var response = await this.Client.SendAsync(msg);
        var serializer = new XmlSerializer(typeof(LoginSessionData));
        var loginData = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as LoginSessionData;
        this.LoginSessionData = loginData;
        this.Handler.CookieContainer.Add(new("http://api.ce.nicovideo.jp"), new Cookie("user_session", this.LoginSessionData.SessionKey, "/", "nicovideo.jp"));
        this.LoginDate = DateTimeOffset.UtcNow;
        return loginData;
    }

    public async Task<bool> CheckSessionValidityAsync()
    {
        using var msg = new HttpRequestMessage(HttpMethod.Get, "https://api.ce.nicovideo.jp/api/v1/session.alive");
        msg.Headers.Add("X-NICOVITA-SESSION", this.LoginSessionData?.SessionKey);
        var resp = await this.Client.SendAsync(msg);
        var txt = await resp.Content.ReadAsStringAsync();
        var dec = JsonSerializer.Deserialize<SessionKeepAlive>(txt);
        return dec.NiconicoResponse.Status == "ok";
    }

    public async Task<bool> LogoutAsync()
    {
        var responseMessage = await this.Client.GetAsync("https://account.nicovideo.jp/logout", HttpCompletionOption.ResponseHeadersRead);
        this.LoginDate = null;
        return responseMessage.IsSuccessStatusCode;
    }

    private static void ApplyDefaultHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
        client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("ja,en-US;q=0.9,en;q=0.8");
        client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"134\", \"Not:A-Brand\";v=\"24\", \"Google Chrome\";v=\"134\"");
        client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
        client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
        client.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
    }
}
