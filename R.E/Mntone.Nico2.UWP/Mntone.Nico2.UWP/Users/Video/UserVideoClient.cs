// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Video.UserVideoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.Video
{
  internal sealed class UserVideoClient
  {
    public static Task<string> GetUserDataAsync(
      NiconicoContext context,
      uint user_id,
      uint page,
      Sort sortMethod,
      Order sortDir)
    {
      string uri = NiconicoUrls.MakeUserVideoRssUrl(user_id.ToString(), page, sortMethod.ToShortString(), sortDir.ToShortString());
      return context.GetClient().GetStringAsync(uri);
    }

    public static UserVideoResponse ParseUserData(string xml)
    {
      UserVideoRss userVideoRss;
      using (StringReader stringReader = new StringReader(xml))
        userVideoRss = (UserVideoRss) new XmlSerializer(typeof (UserVideoRss)).Deserialize((TextReader) stringReader);
      return new UserVideoResponse()
      {
        UserId = uint.Parse(userVideoRss.Channel.Link.Split('/')[2]),
        UserName = userVideoRss.Channel.Creator,
        Items = userVideoRss.Channel.Item.Select<Item, VideoData>((Func<Item, VideoData>) (x =>
        {
          HtmlDocument htmlDocument = new HtmlDocument();
          string str = x.Description.Trim(' ', '\n');
          htmlDocument.LoadHtml(str);
          HtmlNode elementByClassName1 = htmlDocument.DocumentNode.GetElementByClassName("nico-description");
          HtmlNode node = htmlDocument.DocumentNode.GetElementByClassName("nico-info").Element("small");
          HtmlNode elementByClassName2 = node.GetElementByClassName("nico-info-length");
          int[] array = ((IEnumerable<string>) node.GetElementByClassName("nico-info-date").InnerText.Split(new char[4]
          {
            '年',
            '月',
            '日',
            '：'
          }, StringSplitOptions.RemoveEmptyEntries)).Select<string, int>((Func<string, int>) (y => int.Parse(y))).ToArray<int>();
          DateTime dateTime = new DateTime(array[0], array[1], array[2], array[3], array[4], array[5]);
          return new VideoData()
          {
            VideoId = ((IEnumerable<string>) x.Link2.Split('/')).Last<string>(),
            Title = x.Title,
            SubmitTime = dateTime,
            ThumbnailUrl = new Uri(x.Thumbnail.Url),
            Description = elementByClassName1.InnerText,
            Length = elementByClassName2.InnerText.ToTimeSpan()
          };
        })).ToList<VideoData>()
      };
    }

    public static Task<UserVideoResponse> GetUserAsync(
      NiconicoContext context,
      uint user_id,
      uint page,
      Sort sortMethod,
      Order sortDir)
    {
      return UserVideoClient.GetUserDataAsync(context, user_id, page, sortMethod, sortDir).ContinueWith<UserVideoResponse>((Func<Task<string>, UserVideoResponse>) (prevTask => UserVideoClient.ParseUserData(prevTask.Result)));
    }
  }
}
