// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Related.RelatedClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Related
{
  internal sealed class RelatedClient
  {
    public static async Task<string> GetRelatedVideoDataAsync(
      NiconicoContext context,
      string videoId,
      uint from,
      uint limit,
      Sort sortMethod,
      Order sortDir)
    {
      string query = HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) new Dictionary<string, string>()
      {
        {
          "v",
          videoId
        },
        {
          nameof (from),
          from.ToString()
        },
        {
          nameof (limit),
          limit.ToString()
        },
        {
          nameof (sortMethod),
          sortMethod.ToShortString()
        },
        {
          nameof (sortDir),
          sortDir.ToShortString()
        }
      });
      return await context.GetClient().GetStringAsync(string.Format("{0}?{1}", (object) NiconicoUrls.RelatedVideoApiUrl, (object) query));
    }

    private static T ParseXml<T>(string xml)
    {
      using (StringReader stringReader = new StringReader(xml))
        return (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) stringReader);
    }

    public static Task<NicoVideoResponse> GetRelatedVideoAsync(
      NiconicoContext context,
      string videoId,
      uint from,
      uint limit,
      Sort sortMethod,
      Order sortDir)
    {
      return RelatedClient.GetRelatedVideoDataAsync(context, videoId, from, limit, sortMethod, sortDir).ContinueWith<NicoVideoResponse>((Func<Task<string>, NicoVideoResponse>) (prevTask => RelatedClient.ParseXml<NicoVideoResponse>(prevTask.Result)));
    }
  }
}
