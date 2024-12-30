// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Tags.TagsClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.Tags
{
  internal sealed class TagsClient
  {
    public static Task<string> GetTagsDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LiveGatePageUrl + requestId);
    }

    public static TagsResponse ParseTagsData(string tagsData)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(tagsData);
      return new TagsResponse(htmlDocument.DocumentNode);
    }

    public static Task<TagsResponse> GetTagsAsync(NiconicoContext context, string requestId)
    {
      return TagsClient.GetTagsDataAsync(context, requestId).ContinueWith<TagsResponse>((Func<Task<string>, TagsResponse>) (prevTask => TagsClient.ParseTagsData(prevTask.Result)));
    }
  }
}
