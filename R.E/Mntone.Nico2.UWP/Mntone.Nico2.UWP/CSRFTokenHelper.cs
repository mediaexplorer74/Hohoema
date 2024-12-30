// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.CSRFTokenHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2
{
  public static class CSRFTokenHelper
  {
    public static async Task<MylistAdditionInfo> GetMylistToken(
      this NiconicoContext context,
      string group_id,
      string videoId)
    {
      string uri = NiconicoUrls.MakeMylistAddVideoTokenApiUrl(videoId);
      MylistAdditionInfo info = new MylistAdditionInfo()
      {
        GroupId = group_id
      };
      string stringAsync = await context.GetClient().GetStringAsync(uri);
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(stringAsync);
      int num = stringAsync.IndexOf("NicoAPI.token = '");
      info.Token = new string(stringAsync.Skip<char>(num + "NicoAPI.token = '".Length).TakeWhile<char>((Func<char, bool>) (x => '\'' != x)).ToArray<char>());
      foreach (HtmlNode descendant in htmlDocument.DocumentNode.Descendants("input"))
      {
        if (descendant.Attributes.Contains("type") && !(descendant.Attributes["type"].Value != "hidden"))
          info.Values.Add(descendant.Attributes["name"].Value, descendant.Attributes["value"].Value);
      }
      return info;
    }

    public static async Task<string> GetToken(this NiconicoContext context)
    {
      string mylistMyPageUrl = NiconicoUrls.MylistMyPageUrl;
      return await context.GetClient().GetStringAsync(mylistMyPageUrl).ContinueWith<string>((Func<Task<string>, string>) (x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60)));
    }
  }
}
