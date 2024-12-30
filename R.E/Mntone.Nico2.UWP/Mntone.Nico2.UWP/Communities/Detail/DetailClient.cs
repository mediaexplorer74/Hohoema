// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Detail.DetailClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Communities.Detail
{
  public sealed class DetailClient
  {
    public static Task<string> GetCommunitySammaryPageHtmlAsync(
      NiconicoContext context,
      string communityId)
    {
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.CommynitySammaryPageUrl + communityId);
    }

    private static CommunityDetailResponse ParseCommunitySammaryPageHtml(string html)
    {
      CommunityDetailResponse communitySammaryPageHtml1 = new CommunityDetailResponse();
      CommunitySammary communitySammary = new CommunitySammary();
      CommunityDetail communityDetail = new CommunityDetail();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      HtmlNode htmlNode1 = htmlDocument.DocumentNode.Element(nameof (html)).Element("body");
      HtmlNode htmlNode2 = htmlNode1.Element("main");
      HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@class='now_live_inner']");
      if (htmlNodeCollection != null)
      {
        foreach (HtmlNode htmlNode3 in (IEnumerable<HtmlNode>) htmlNodeCollection)
        {
          CommunityLiveInfo communityLiveInfo = new CommunityLiveInfo();
          HtmlNode htmlNode4 = htmlNode3.SelectSingleNode("//h2[@class='now_live_title']");
          communityLiveInfo.LiveTitle = htmlNode4.InnerText;
          communityLiveInfo.LiveId = ((IEnumerable<string>) new string(htmlNode3.Attributes["href"].Value.TakeWhile<char>((Func<char, bool>) (x => x != '?')).ToArray<char>()).Split('/')).LastOrDefault<string>() ?? throw new Exception();
          communityDetail.CurrentLiveList.Add(communityLiveInfo);
        }
      }
      HtmlNode htmlNode5 = htmlNode1.Element("header");
      HtmlNode node1 = htmlNode5.SelectSingleNode("//div[@class='communityData']");
      HtmlNode elementByClassName1 = node1.GetElementByClassName("communityDetail");
      try
      {
        HtmlNode htmlNode6 = node1.Element("h2").Element("a");
        communityDetail.Name = htmlNode6.InnerText.Trim('\n', '\t');
      }
      catch
      {
      }
      try
      {
        DateTime dateTimeText = DetailClient.ParseDateTimeText(elementByClassName1.SelectSingleNode("./tr[2]/td").InnerText);
        communityDetail.DateTime = dateTimeText.ToString();
        HtmlNode htmlNode7 = elementByClassName1.SelectSingleNode("./tr[1]/td/a");
        string str1 = ((IEnumerable<string>) htmlNode7.Attributes["href"].Value.Split('/')).LastOrDefault<string>();
        communityDetail.OwnerUserId = str1;
        string str2 = htmlNode7.InnerText.Trim('\n', '\t');
        communityDetail.OwnerUserName = str2;
      }
      catch
      {
        throw;
      }
      try
      {
        HtmlNodeCollection source = elementByClassName1.SelectNodes("./tr[3]//a");
        if (source != null)
          communityDetail.Tags = ((IEnumerable<HtmlNode>) source).Select<HtmlNode, string>((Func<HtmlNode, string>) (x => x.InnerText)).ToList<string>();
      }
      catch
      {
      }
      HtmlNode htmlNode8 = htmlNode5.SelectSingleNode("//div[@class='communityRegist']");
      uint num1 = uint.Parse(htmlNode8.SelectSingleNode("./div/dl/dd[1]").InnerText);
      communityDetail.Level = num1;
      uint num2 = uint.Parse(htmlNode8.SelectSingleNode("./div/dl/dd[2]").FirstChild.InnerText.TrimStart('\n', '\t'));
      communityDetail.MemberCount = num2;
      try
      {
        HtmlNode htmlNode9 = htmlNode2.SelectSingleNode("id('profile_text_content')");
        communityDetail.ProfielHtml = htmlNode9.InnerHtml;
      }
      catch
      {
      }
      HtmlNode htmlNode10 = htmlNode2.SelectSingleNode("//ul[@class='noticeList']");
      try
      {
        if (htmlNode10 != null)
        {
          foreach (HtmlNode element in htmlNode10.Elements("li"))
          {
            CommunityNews communityNews = new CommunityNews();
            HtmlNode node2 = element.SelectSingleNode(".//div[@class='noticeItemHeader']");
            HtmlNode elementByClassName2 = node2.GetElementByClassName("noticeTitle");
            communityNews.Title = elementByClassName2.InnerText;
            HtmlNode htmlNode11 = node2.SelectSingleNode(".//span[@class='date']");
            communityNews.PostDate = DetailClient.ParseDateTimeText(htmlNode11.InnerText);
            HtmlNode htmlNode12 = node2.SelectSingleNode(".//span[@class='author']");
            communityNews.PostAuthor = htmlNode12.InnerText;
            HtmlNode htmlNode13 = element.Element("p");
            communityNews.ContentHtml = htmlNode13.InnerHtml;
            communityDetail.NewsList.Add(communityNews);
          }
        }
      }
      catch
      {
      }
      communitySammary.CommunityDetail = communityDetail;
      HtmlNode htmlNode14 = htmlNode2.SelectSingleNode("//div[@class='area-sideContent']");
      try
      {
        Func<HtmlNode, LiveInfo> func = (Func<HtmlNode, LiveInfo>) (node =>
        {
          LiveInfo communitySammaryPageHtml2 = new LiveInfo();
          communitySammaryPageHtml2.StartTime = DetailClient.ParseDateTimeText(node.GetElementByClassName("liveDate").InnerText);
          HtmlNode htmlNode15 = node.Element("a");
          communitySammaryPageHtml2.Title = htmlNode15.InnerText;
          string str = htmlNode15.Attributes["href"].Value;
          int startIndex = str.IndexOf("lv");
          int num3 = str.LastIndexOf('?');
          communitySammaryPageHtml2.LiveId = str.Substring(startIndex, num3 - startIndex);
          communitySammaryPageHtml2.StreamerName = node.GetElementByClassName("liveBroadcaster").InnerText.Remove(0, 4);
          return communitySammaryPageHtml2;
        });
        HtmlNode htmlNode16 = htmlNode14.SelectSingleNode("./section//h2[contains(.,'生放送のお知らせ')]");
        if (htmlNode16 != null)
        {
          HtmlNode htmlNode17 = htmlNode16.SelectSingleNode("../../ul[1]");
          HtmlNode htmlNode18 = htmlNode16.SelectSingleNode("../../ul[2]");
          foreach (HtmlNode htmlNode19 in htmlNode17.Elements("li").Where<HtmlNode>((Func<HtmlNode, bool>) (x => !x.HasAttributes)))
          {
            LiveInfo liveInfo = func(htmlNode19);
            communityDetail.RecentLiveList.Add(liveInfo);
          }
          foreach (HtmlNode htmlNode20 in htmlNode18.Elements("li").Where<HtmlNode>((Func<HtmlNode, bool>) (x => !x.HasAttributes)))
          {
            LiveInfo liveInfo = func(htmlNode20);
            communityDetail.FutureLiveList.Add(liveInfo);
          }
        }
      }
      catch
      {
      }
      HtmlNode parentNode1 = htmlNode14.SelectSingleNode("./section//h2[contains(.,'コミュニティフォロワー')]").ParentNode.ParentNode;
      try
      {
        uint num4 = uint.Parse(new string(parentNode1.SelectSingleNode(".//span[@class='subinfo']").InnerText.TakeWhile<char>((Func<char, bool>) (x => x >= '0' && x <= '9')).ToArray<char>()));
        communityDetail.MemberCount = num4;
      }
      catch
      {
      }
      try
      {
        foreach (HtmlNode selectNode in (IEnumerable<HtmlNode>) parentNode1.SelectNodes("ul/li"))
        {
          CommunityMember communityMember = new CommunityMember();
          HtmlNode htmlNode21 = selectNode.Elements("a").FirstOrDefault<HtmlNode>();
          if (htmlNode21 != null)
          {
            string s = new string(htmlNode21.Attributes["href"].Value.SkipWhile<char>((Func<char, bool>) (x => x < '0' || x > '9')).TakeWhile<char>((Func<char, bool>) (x => x >= '0' && x <= '9')).ToArray<char>());
            communityMember.UserId = uint.Parse(s);
            HtmlNode htmlNode22 = htmlNode21.Element("img");
            communityMember.IconUrl = new Uri(htmlNode22.Attributes["src"].Value);
            communityMember.Name = htmlNode22.Attributes["title"].Value;
            communityDetail.SampleFollwers.Add(communityMember);
          }
        }
      }
      catch
      {
      }
      HtmlNode parentNode2 = htmlNode14.SelectSingleNode("./section//h2[contains(.,'コミュニティ動画')]").ParentNode.ParentNode;
      try
      {
        uint num5 = uint.Parse(new string(parentNode2.SelectSingleNode(".//span[@class='subinfo']").InnerText.TakeWhile<char>((Func<char, bool>) (x => x >= '0' && x <= '9')).ToArray<char>()));
        communityDetail.VideoCount = num5;
        communityDetail.VideoMaxCount = 10000U;
      }
      catch
      {
      }
      try
      {
        foreach (HtmlNode selectNode in (IEnumerable<HtmlNode>) parentNode2.SelectNodes("ul/li"))
        {
          CommunityVideo communityVideo = new CommunityVideo();
          HtmlNode htmlNode23 = selectNode.Elements("a").FirstOrDefault<HtmlNode>();
          if (htmlNode23 != null)
          {
            string str = new string(htmlNode23.Attributes["href"].Value.SkipWhile<char>((Func<char, bool>) (x => x < '0' || x > '9')).TakeWhile<char>((Func<char, bool>) (x => x >= '0' && x <= '9')).ToArray<char>());
            communityVideo.VideoId = str;
            HtmlNode htmlNode24 = htmlNode23.Element("img");
            communityVideo.ThumbnailUrl = htmlNode24.Attributes["src"].Value;
            communityVideo.Title = htmlNode24.Attributes["title"].Value;
            communityDetail.VideoList.Add(communityVideo);
          }
        }
      }
      catch
      {
      }
      communitySammaryPageHtml1.CommunitySammary = communitySammary;
      communitySammaryPageHtml1.IsStatusOK = true;
      return communitySammaryPageHtml1;
    }

    private static DateTime ParseDateTimeText(string str)
    {
      string[] strArray = str.Trim('\t', '\n').Split(new string[5]
      {
        "年",
        "月",
        "日",
        ":",
        "："
      }, 10, StringSplitOptions.None);
      int year = 0;
      int month = 0;
      int day = 0;
      int result1 = 0;
      int result2 = 0;
      if (strArray.Length >= 3)
      {
        year = int.Parse(strArray[0]);
        month = int.Parse(strArray[1]);
        day = int.Parse(strArray[2]);
      }
      if (strArray.Length >= 5)
      {
        int.TryParse(strArray[3], out result1);
        int.TryParse(strArray[4], out result2);
      }
      return new DateTime(year, month, day, result1, result2, 0);
    }

    public static Task<CommunityDetailResponse> GetCommunityDetailAsync(
      NiconicoContext context,
      string communityId)
    {
      return DetailClient.GetCommunitySammaryPageHtmlAsync(context, communityId).ContinueWith<CommunityDetailResponse>((Func<Task<string>, CommunityDetailResponse>) (prevTask => DetailClient.ParseCommunitySammaryPageHtml(prevTask.Result)));
    }
  }
}
