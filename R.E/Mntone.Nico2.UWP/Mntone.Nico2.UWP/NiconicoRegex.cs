// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoRegex
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Text.RegularExpressions;

#nullable disable
namespace Mntone.Nico2
{
  public sealed class NiconicoRegex
  {
    internal const string VideoIdRegexBase = "(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\\d{1,14}";
    internal const string LiveIdRegexBase = "lv\\d{1,14}";
    internal const string ImageIdRegexBase = "(?:[sm]g|im|bk)\\d{1,14}";
    internal const string ThemeIdRegexBase = "sg\\d{1,14}";
    internal const string IllustIdRegexBase = "im\\d{1,14}";
    internal const string ElectronicBookIdRegexBase = "bk\\d{1,14}";
    internal const string MangaIdRegexBase = "mg\\d{1,14}";
    internal const string CommunityIdRegexBase = "co\\d{1,14}";
    internal const string ChannelIdRegexBase = "ch\\d{1,14}";
    internal const string ArticleIdRegexBase = "ar\\d{1,14}";
    internal const string NewsIdRegexBase = "nw\\d{1,14}";
    internal const string CommonIdRegexBase = "nc\\d{1,14}";
    internal const string AppsIdRegexBase = "ap\\d{1,14}";
    internal const string WatchIdRegexBase = "watch/\\d{1,10}";
    internal const string UserIdRegexBase = "user/\\d{1,10}";
    internal const string MyListRegexBase = "mylist/\\d{1,10}";
    internal const string MyVideoRegexBase = "myvideo/\\d{1,10}";
    internal const string ClipIdRegexBase = "clip/\\d{1,10}";
    internal const string ComicIdRegexBase = "comic/\\d{1,10}";
    internal const string AdsIdRegexBase = "(?:dw\\d+|az[A-Z0-9]{10}|ys[a-zA-Z0-9-]+_[a-zA-Z0-9-]+|ga\\d+|ip[\\d_]+|gg[a-zA-Z0-9]+-[a-zA-Z0-9-]+)";

    internal NiconicoRegex()
    {
    }

    public static bool IsVideoId(string id)
    {
      return Regex.IsMatch(id, "^(?:sm|nm|so|ca|ax|yo|nl|ig|na|cw|z[a-e]|om|sk|yk)\\d{1,14}$");
    }

    public static bool IsLiveId(string id) => Regex.IsMatch(id, "^lv\\d{1,14}$");

    public static bool IsImageId(string id) => Regex.IsMatch(id, "^(?:[sm]g|im|bk)\\d{1,14}$");

    public static bool IsThemeId(string id) => Regex.IsMatch(id, "^sg\\d{1,14}$");

    public static bool IsIllustrationId(string id) => Regex.IsMatch(id, "^im\\d{1,14}$");

    public static bool IsMangaId(string id) => Regex.IsMatch(id, "^mg\\d{1,14}$");

    public static bool IsElectronicBookId(string id) => Regex.IsMatch(id, "^bk\\d{1,14}$");

    public static bool IsCommunityId(string id) => Regex.IsMatch(id, "^co\\d{1,14}$");

    public static bool IsChannelId(string id) => Regex.IsMatch(id, "^ch\\d{1,14}$");
  }
}
