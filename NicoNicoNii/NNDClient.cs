using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

using NicoNicoNii.Entities.JSON;
using NicoNicoNii.Entities.XML;

namespace NicoNicoNii;

public class NndClient
{
	internal readonly HttpClient Client;
	internal readonly HttpClientHandler Handler;
	internal DateTimeOffset? LoginDate { get; set; }

	public LoginSessionData LoginSessionData { get; internal set; }

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
	}

	public NndClient(HttpClientHandler handler)
	{
		this.Handler = handler;
		this.Client = new(handler);
	}

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
}