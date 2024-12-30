// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoContext
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Channels;
using Mntone.Nico2.Communities;
using Mntone.Nico2.Dictionaries;
using Mntone.Nico2.Embed;
using Mntone.Nico2.Images;
using Mntone.Nico2.Live;
using Mntone.Nico2.Mylist;
using Mntone.Nico2.NicoRepo;
using Mntone.Nico2.Searches;
using Mntone.Nico2.Users;
using Mntone.Nico2.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

#nullable disable
namespace Mntone.Nico2
{
  public sealed class NiconicoContext : IDisposable
  {
    private VideoApi _Video;
    private LiveApi _Live;
    private ImageApi _Image;
    private SearchApi _Search;
    private DictionaryApi _Dictionary;
    private CommunityApi _Community;
    private ChannelApi _Channel;
    private UserApi _User;
    private MylistApi _Mylist;
    private NicoRepoApi _NicoRepo;
    private EmbedApi _Embed;
    private NiconicoSession _CurrentSession;
    private string _AdditionalUserAgent;
    private const string XNiconicoId = "x-niconico-id";
    private const string XNiconicoAuthflag = "x-niconico-authflag";
    private const string MailTelName = "mail_tel";
    private const string PasswordName = "password";
    private const string UserSessionName = "user_session";
    internal const string DefaultUserAgent = "OpenNiconico/2.0";
    private readonly Uri NiconicoCookieUrl = new Uri("http://nicovideo.jp/");

    public NiconicoContext()
    {
    }

    public NiconicoContext(NiconicoAuthenticationToken token) => this.AuthenticationToken = token;

    public NiconicoContext(NiconicoAuthenticationToken token, NiconicoSession session)
      : this(token)
    {
      this.CurrentSession = session;
    }

    public void Dispose() => this.DisposeImpl();

    private void DisposeImpl()
    {
      if (this.HttpClient == null)
        return;
      this.HttpClient.Dispose();
      this.HttpClient = (HttpClient) null;
    }

    public void ClearAuthenticationCache()
    {
      if (!ApiInformation.IsMethodPresent("Windows.Web.Http.Filters.HttpBaseProtocolFilter", nameof (ClearAuthenticationCache)))
        return;
      this.ProtocolFilter?.ClearAuthenticationCache();
    }

    public async Task<NiconicoSignInStatus> SignInAsync()
    {
      NiconicoContext niconicoContext = this;
      // ISSUE: reference to a compiler-generated method
      return await WindowsRuntimeSystemExtensions.AsTask<HttpResponseMessage, HttpProgress>(niconicoContext.GetClient().PostAsync(new Uri(NiconicoUrls.LogOnApiUrl), (IHttpContent) new HttpFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) new System.Collections.Generic.Dictionary<string, string>()
      {
        {
          "mail_tel",
          niconicoContext.AuthenticationToken.MailOrTelephone
        },
        {
          "password",
          niconicoContext.AuthenticationToken.Password
        }
      }))).ContinueWith<Task<NiconicoSignInStatus>>(new Func<Task<HttpResponseMessage>, Task<NiconicoSignInStatus>>(niconicoContext.\u003CSignInAsync\u003Eb__6_0)).Unwrap<NiconicoSignInStatus>();
    }

    public IHttpContent LastRedirectHttpContent { get; private set; }

    public HttpRequestMessage LastRedirectHttpRequestMessage { get; private set; }

    public Task<NiconicoSignInStatus> GetIsSignedInAsync() => this.GetIsSignedInOnInternalAsync();

    internal Task<NiconicoSignInStatus> GetIsSignedInOnInternalAsync()
    {
      return WindowsRuntimeSystemExtensions.AsTask<HttpResponseMessage, HttpProgress>(this.GetClient().GetAsync(new Uri(NiconicoUrls.TopPageUrl))).ContinueWith<NiconicoSignInStatus>((Func<Task<HttpResponseMessage>, NiconicoSignInStatus>) (prevTask =>
      {
        HttpResponseMessage result = prevTask.Result;
        return result.StatusCode == 200 ? (((IDictionary<string, string>) result.Headers)["x-niconico-authflag"].ToUInt() == 0U ? NiconicoSignInStatus.Failed : NiconicoSignInStatus.Success) : (result.StatusCode == 503 ? NiconicoSignInStatus.ServiceUnavailable : NiconicoSignInStatus.Failed);
      }));
    }

    public Task<NiconicoSignInStatus> SignOutOffAsync()
    {
      return WindowsRuntimeSystemExtensions.AsTask<HttpResponseMessage, HttpProgress>(this.GetClient().GetAsync(new Uri(NiconicoUrls.LogOffUrl))).ContinueWith<Task<NiconicoSignInStatus>>((Func<Task<HttpResponseMessage>, Task<NiconicoSignInStatus>>) (prevTask => this.GetIsSignedInOnInternalAsync())).Unwrap<NiconicoSignInStatus>();
    }

    internal HttpClient GetClient()
    {
      if (this.HttpClient == null)
      {
        this.ProtocolFilter = new HttpBaseProtocolFilter();
        this.ProtocolFilter.CacheControl.put_ReadBehavior((HttpCacheReadBehavior) 1);
        this.HttpClient = new HttpClient((IHttpFilter) this.ProtocolFilter);
        ((IDictionary<string, string>) this.HttpClient.DefaultRequestHeaders).Add("user-agent", this._AdditionalUserAgent != null ? "OpenNiconico/2.0 (" + this._AdditionalUserAgent + ")" : "OpenNiconico/2.0");
        ((IDictionary<string, string>) this.HttpClient.DefaultRequestHeaders).Add("accept-language", "ja, en;q=0.5");
      }
      return this.HttpClient;
    }

    internal Task<HttpResponseMessage> GetAsync(string url) => this.GetClient().GetAsync(url);

    internal Task<string> GetStringAsync(string url, System.Collections.Generic.Dictionary<string, string> query)
    {
      string str = string.Join("&", query.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key + "=" + Uri.EscapeDataString(x.Value))));
      string uri = string.Format("{0}?{1}", (object) url, (object) str);
      return this.GetClient().GetStringAsync(uri);
    }

    internal Task<string> GetStringAsync(string url) => this.GetClient().GetStringAsync(url);

    internal Task<string> PostAsync(string url, bool withToken = true)
    {
      System.Collections.Generic.Dictionary<string, string> keyvalues = new System.Collections.Generic.Dictionary<string, string>();
      return this.PostAsync(url, keyvalues, withToken);
    }

    internal async Task<string> PostAsync(
      string url,
      System.Collections.Generic.Dictionary<string, string> keyvalues,
      bool withToken = true)
    {
      NiconicoContext context = this;
      if (withToken && !keyvalues.ContainsKey("token"))
        keyvalues.Add("token", await context.GetToken());
      HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) keyvalues);
      return await context.PostAsync(url, (IHttpContent) content);
    }

    internal async Task<string> PostAsync(string url, IHttpContent content)
    {
      using (HttpResponseMessage res = await this.GetClient().PostAsync(new Uri(url), content))
        return res.IsSuccessStatusCode ? await res.Content.ReadAsStringAsync() : "";
    }

    public VideoApi Video => this._Video ?? (this._Video = new VideoApi(this));

    public LiveApi Live => this._Live ?? (this._Live = new LiveApi(this));

    public ImageApi Image => this._Image ?? (this._Image = new ImageApi(this));

    public SearchApi Search => this._Search ?? (this._Search = new SearchApi(this));

    public DictionaryApi Dictionary
    {
      get => this._Dictionary ?? (this._Dictionary = new DictionaryApi(this));
    }

    public CommunityApi Community => this._Community ?? (this._Community = new CommunityApi(this));

    public ChannelApi Channel => this._Channel ?? (this._Channel = new ChannelApi(this));

    public UserApi User => this._User ?? (this._User = new UserApi(this));

    public MylistApi Mylist => this._Mylist ?? (this._Mylist = new MylistApi(this));

    public NicoRepoApi NicoRepo => this._NicoRepo ?? (this._NicoRepo = new NicoRepoApi(this));

    public EmbedApi Embed => this._Embed ?? (this._Embed = new EmbedApi(this));

    public NiconicoAuthenticationToken AuthenticationToken { get; set; }

    public NiconicoSession CurrentSession
    {
      get => this._CurrentSession;
      set
      {
        this._CurrentSession = value;
        this.DisposeImpl();
      }
    }

    public string AdditionalUserAgent
    {
      get => this._AdditionalUserAgent;
      set => this._AdditionalUserAgent = value;
    }

    public HttpBaseProtocolFilter ProtocolFilter { get; private set; }

    public HttpClient HttpClient { get; private set; }
  }
}
