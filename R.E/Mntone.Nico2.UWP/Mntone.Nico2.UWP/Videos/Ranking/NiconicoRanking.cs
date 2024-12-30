// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Ranking.NiconicoRanking
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Ranking
{
  public sealed class NiconicoRanking
  {
    public const string NiconicoRankingDomain = "http://www.nicovideo.jp/ranking/";

    internal static string MakeRankingUrlParameters(
      RankingTarget target,
      RankingTimeSpan timeSpan,
      RankingCategory category)
    {
      return string.Format("{0}/{1}/{2}?rss=2.0", (object) target.ToString(), (object) timeSpan.ToString(), (object) category.ToString());
    }

    public static async Task<NiconicoVideoRss> GetRankingData(
      RankingTarget target,
      RankingTimeSpan timeSpan,
      RankingCategory category)
    {
      string requestUri = "http://www.nicovideo.jp/ranking/" + NiconicoRanking.MakeRankingUrlParameters(target, timeSpan, category);
      try
      {
        using (HttpClient client = new HttpClient())
        {
          using (Stream stream = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri)
          {
            Headers = {
              AcceptLanguage = {
                new StringWithQualityHeaderValue("ja", 0.5)
              },
              UserAgent = {
                new ProductInfoHeaderValue("NicoPlayerHohoema_UWP", "1.0")
              }
            }
          })).Content.ReadAsStreamAsync())
            return (NiconicoVideoRss) new XmlSerializer(typeof (NiconicoVideoRss)).Deserialize(stream);
        }
      }
      catch (HttpRequestException ex)
      {
      }
      catch (Exception ex)
      {
      }
      return (NiconicoVideoRss) null;
    }
  }
}
