// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.NicoRepoApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.NicoRepo.LoginUserNicoRepo;
using Mntone.Nico2.NicoRepo.UserNicoRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  public sealed class NicoRepoApi
  {
    private NiconicoContext _context;
    public static readonly string NicoRepoApiBaseUrl = string.Format("{0}nicorepo/", (object) "http://www.nicovideo.jp/api/");
    public static readonly string NicoRepoTimelineApiUrl = string.Format("{0}timeline/", (object) NicoRepoApi.NicoRepoApiBaseUrl);

    public static string MakeNicoRepoUrl_LoginUser(
      NicoRepoTimelineType timelineType,
      string lastTimelineItemId = null)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      if (lastTimelineItemId != null)
        dict.Add("cursor", lastTimelineItemId);
      dict.Add("client_app", "pc_myrepo");
      long timeMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      dict.Add("_", timeMilliseconds.ToString());
      return string.Format("{0}my/{1}?{2}", (object) NicoRepoApi.NicoRepoTimelineApiUrl, (object) timelineType.ToString(), (object) HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) dict));
    }

    public static string MakeNicoRepoUrl_User(uint userId, string lastTimelineItemId = null)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      if (lastTimelineItemId != null)
        dict.Add("cursor", lastTimelineItemId);
      dict.Add("client_app", "pc_profilerepo");
      long timeMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      dict.Add("_", timeMilliseconds.ToString());
      return string.Format("{0}user/{1}?{2}", (object) NicoRepoApi.NicoRepoTimelineApiUrl, (object) userId.ToString(), (object) HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) dict));
    }

    internal NicoRepoApi(NiconicoContext context) => this._context = context;

    public Task<NicoRepoResponse> GetLoginUserNicoRepo(
      NicoRepoTimelineType timelineType,
      string lastNicoRepoItemId = null)
    {
      return LoginUserNicoRepoClient.GetLoginUserNicoRepoAsync(this._context, timelineType, lastNicoRepoItemId);
    }

    public Task<NicoRepoResponse> GetUserNicoRepo(uint userId, string lastNicoRepoItemId = null)
    {
      return UserNicoRepoClient.GetUserNicoRepoAsync(this._context, userId, lastNicoRepoItemId);
    }
  }
}
