// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Description.DescriptionClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.Description
{
  internal sealed class DescriptionClient
  {
    public static Task<string> GetDescriptionDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LiveGatePageUrl + requestId);
    }

    public static DescriptionResponse ParseDescriptionData(string userInfoData)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(userInfoData);
      HtmlNode htmlNode = htmlDocument.DocumentNode.Element("html");
      string attributeValue = htmlNode.GetAttributeValue("lang", "ja-jp");
      return new DescriptionResponse(htmlNode.Element("body").GetElementById("all_cover").GetElementById("all"), attributeValue);
    }

    public static Task<DescriptionResponse> GetDescriptionAsync(
      NiconicoContext context,
      string requestId)
    {
      return DescriptionClient.GetDescriptionDataAsync(context, requestId).ContinueWith<DescriptionResponse>((Func<Task<string>, DescriptionResponse>) (prevTask => DescriptionClient.ParseDescriptionData(prevTask.Result)));
    }
  }
}
