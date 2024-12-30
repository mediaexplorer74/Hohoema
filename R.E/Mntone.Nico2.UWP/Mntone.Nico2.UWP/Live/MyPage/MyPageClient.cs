// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.MyPage.MyPageClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.MyPage
{
  internal sealed class MyPageClient
  {
    public static Task<string> GetMyPageDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LiveMyPageUrl);
    }

    public static MyPageResponse ParseMyPageData(string myPageData)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(myPageData);
      HtmlNode htmlNode = htmlDocument.DocumentNode.Element("html");
      string language = htmlNode.GetAttributeValue("lang", "ja-jp");
      if (language == "ja-jp" && myPageData.Contains("locale=\"en-us\""))
        language = "en-us";
      return new MyPageResponse(htmlNode.Element("body").GetElementById("all_cover").GetElementById("all").GetElementByClassName("container").GetElementById("liveList"), language);
    }

    public static Task<MyPageResponse> GetMyPageAsync(NiconicoContext context)
    {
      return MyPageClient.GetMyPageDataAsync(context).ContinueWith<MyPageResponse>((Func<Task<string>, MyPageResponse>) (prevTask => MyPageClient.ParseMyPageData(prevTask.Result)));
    }
  }
}
