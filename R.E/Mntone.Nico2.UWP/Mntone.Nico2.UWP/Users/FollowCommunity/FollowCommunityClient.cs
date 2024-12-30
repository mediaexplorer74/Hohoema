// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.FollowCommunity.FollowCommunityClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Users.FollowCommunity
{
  public sealed class FollowCommunityClient
  {
    public static Task<string> GetMyPageCommunityHtmlAsync(NiconicoContext context, int page)
    {
      string uri = NiconicoUrls.UserFavCommunityPageUrl;
      if (page > 0)
        uri = uri + "?page=" + (page + 1).ToString();
      return context.GetClient().GetConvertedStringAsync(uri);
    }

    private static FollowCommunityResponse PerseFollowCommunityPageHtml(string html)
    {
      FollowCommunityResponse communityResponse = new FollowCommunityResponse();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      HtmlNode elementByClassName1 = htmlDocument.DocumentNode.Element(nameof (html)).Element("body").GetElementsByClassName("wrapper").First<HtmlNode>().GetElementById("favCommunity").GetElementByClassName("articleBody");
      if (elementByClassName1.GetElementByClassName("noListMsg") != null)
        return communityResponse;
      foreach (HtmlNode node in elementByClassName1.GetElementsByClassName("outer"))
      {
        FollowCommunityInfo followCommunityInfo = new FollowCommunityInfo();
        followCommunityInfo.IconUrl = node.GetElementByClassName("thumbContainer").Element("a").Element("img").Attributes["src"].Value;
        HtmlNode elementByClassName2 = node.GetElementByClassName("section");
        HtmlNode htmlNode1 = elementByClassName2.Element("h5").Element("a");
        string innerText = htmlNode1.InnerText;
        followCommunityInfo.CommunityName = innerText;
        string str = ((IEnumerable<string>) htmlNode1.Attributes["href"].Value.Split('/')).Last<string>();
        followCommunityInfo.CommunityId = str;
        IEnumerable<HtmlNode> source = elementByClassName2.Element("ul").Elements("li");
        int num1 = int.Parse(((IEnumerable<string>) source.ElementAt<HtmlNode>(0).InnerText.Split(':')).Last<string>().Replace(",", ""));
        followCommunityInfo.VideoCount = num1;
        int num2 = int.Parse(((IEnumerable<string>) source.ElementAt<HtmlNode>(1).InnerText.Split(':')).Last<string>().Replace(",", ""));
        followCommunityInfo.MemberCount = num2;
        HtmlNode htmlNode2 = elementByClassName2.Element("p");
        followCommunityInfo.ShortDescription = htmlNode2.InnerText;
        communityResponse.Items.Add(followCommunityInfo);
      }
      return communityResponse;
    }

    public static Task<FollowCommunityResponse> GetFollowCommunityAsync(
      NiconicoContext context,
      int page)
    {
      return FollowCommunityClient.GetMyPageCommunityHtmlAsync(context, page).ContinueWith<FollowCommunityResponse>((Func<Task<string>, FollowCommunityResponse>) (prevTask => FollowCommunityClient.PerseFollowCommunityPageHtml(prevTask.Result)));
    }

    public static async Task<bool> AddFollowCommunity(
      NiconicoContext context,
      string communityId,
      string title = "",
      string comment = "",
      bool notify = false)
    {
      string url = NiconicoUrls.CommunityJoinPageUrl + communityId;
      if (!(await context.GetAsync(url)).IsSuccessStatusCode)
        return false;
      await Task.Delay(1000);
      HttpFormUrlEncodedContent urlEncodedContent = new HttpFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) new Dictionary<string, string>()
      {
        {
          "mode",
          "commit"
        },
        {
          nameof (title),
          title ?? "フォローリクエスト"
        },
        {
          nameof (comment),
          comment ?? ""
        },
        {
          nameof (notify),
          notify ? "1" : ""
        }
      });
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Upgrade-Insecure-Requests"] = "1";
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Referer"] = url;
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
      httpRequestMessage.put_Content((IHttpContent) urlEncodedContent);
      return (await context.HttpClient.SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode;
    }

    public static async Task<CommunityLeaveToken> GetCommunityLeaveToken(
      NiconicoContext context,
      string communityId)
    {
      string stringAsync = await context.GetStringAsync(NiconicoUrls.CommunityLeavePageUrl + communityId);
      CommunityLeaveToken communityLeaveToken = new CommunityLeaveToken()
      {
        CommunityId = communityId
      };
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(stringAsync);
      foreach (HtmlNode selectNode in (IEnumerable<HtmlNode>) htmlDocument.DocumentNode.SelectNodes("//main//input"))
      {
        switch (selectNode.GetAttributeValue("name", ""))
        {
          case "time":
            string attributeValue1 = selectNode.GetAttributeValue("value", "");
            communityLeaveToken.Time = attributeValue1;
            continue;
          case "commit_key":
            string attributeValue2 = selectNode.GetAttributeValue("value", "");
            communityLeaveToken.CommitKey = attributeValue2;
            continue;
          case "commit":
            string attributeValue3 = selectNode.GetAttributeValue("value", "");
            communityLeaveToken.Commit = attributeValue3;
            continue;
          default:
            continue;
        }
      }
      return communityLeaveToken;
    }

    public static async Task<bool> RemoveFollowCommunity(
      NiconicoContext context,
      CommunityLeaveToken token)
    {
      string uriString = NiconicoUrls.CommunityLeavePageUrl + token.CommunityId;
      HttpFormUrlEncodedContent urlEncodedContent = new HttpFormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) new Dictionary<string, string>()
      {
        {
          "time",
          token.Time
        },
        {
          "commit_key",
          token.CommitKey
        },
        {
          "commit",
          token.Commit
        }
      });
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(uriString));
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Upgrade-Insecure-Requests"] = "1";
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Referer"] = uriString;
      ((IDictionary<string, string>) httpRequestMessage.Headers)["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
      httpRequestMessage.put_Content((IHttpContent) urlEncodedContent);
      return (await context.HttpClient.SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode;
    }
  }
}
