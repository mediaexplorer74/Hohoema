// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Video.CommunityVideoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Videos.Ranking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Video
{
  public sealed class CommunityVideoClient
  {
    public static Task<string> GetCommunityVideoRssAsync(
      NiconicoContext context,
      string communityId,
      uint page)
    {
      return context.GetStringAsync(string.Format("{0}/{1}", (object) NiconicoUrls.CommynityVideoPageUrl, (object) communityId), new Dictionary<string, string>()
      {
        {
          "rss",
          "2.0"
        },
        {
          nameof (page),
          page.ToString()
        }
      });
    }

    private static NiconicoVideoRss ParseRssString(string rssText)
    {
      using (StringReader stringReader = new StringReader(rssText))
        return (NiconicoVideoRss) new XmlSerializer(typeof (NiconicoVideoRss)).Deserialize((TextReader) stringReader);
    }

    public static Task<NiconicoVideoRss> GetCommunityVideosAsync(
      NiconicoContext context,
      string communityId,
      uint page)
    {
      return CommunityVideoClient.GetCommunityVideoRssAsync(context, communityId, page).ContinueWith<NiconicoVideoRss>((Func<Task<string>, NiconicoVideoRss>) (prevTask => CommunityVideoClient.ParseRssString(prevTask.Result)));
    }
  }
}
