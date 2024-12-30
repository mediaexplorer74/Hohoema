// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Community.CommunityClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches.Community
{
  public sealed class CommunityClient
  {
    public static Task<string> GetCommunitySearchPageHtmlAsync(
      NiconicoContext context,
      string str,
      uint page,
      CommunitySearchSort sort,
      Order order,
      CommunitySearchMode mode)
    {
      return context.GetStringAsync(NiconicoUrls.CommynitySearchPageUrl + str, new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          nameof (page),
          page.ToString()
        },
        {
          nameof (order),
          order.ToChar().ToString()
        },
        {
          nameof (sort),
          sort.ToShortString()
        },
        {
          nameof (mode),
          mode.ToShortString()
        }
      });
    }

    private static CommunitySearchResponse ParseCommunitySearchPageHtml(string html)
    {
      CommunitySearchResponse communitySearchPageHtml = new CommunitySearchResponse();
      if (string.IsNullOrEmpty(html))
        return communitySearchPageHtml;
      HtmlDocument htmlDocument = new HtmlDocument();
      try
      {
        htmlDocument.LoadHtml(html);
      }
      catch
      {
        return communitySearchPageHtml;
      }
      HtmlNode elementById = htmlDocument.DocumentNode.Element(nameof (html)).GetElementByTagName("body").GetElementById("site-body").GetElementById("contents0727").GetElementById("main0727");
      try
      {
        uint num = uint.Parse(string.Join("", elementById.GetElementsByClassName("pagenavi").FirstOrDefault<HtmlNode>().GetElementByClassName("pagelink").Element("strong").InnerText.Split(',')));
        communitySearchPageHtml.TotalCount = num;
      }
      catch
      {
        throw;
      }
      try
      {
        foreach (HtmlNode element in elementById.Elements("div").ElementAt<HtmlNode>(1).Element("div").Elements("div"))
        {
          NicoCommynity nicoCommynity = new NicoCommynity();
          HtmlNode htmlNode1 = element.Element("table").Element("tr");
          HtmlNode htmlNode2 = htmlNode1.Elements("td").ElementAt<HtmlNode>(0);
          HtmlNode htmlNode3 = htmlNode2.Element("a");
          string str = ((IEnumerable<string>) htmlNode3.Attributes["href"].Value.Split('/')).LastOrDefault<string>();
          nicoCommynity.Id = !string.IsNullOrEmpty(str) ? str : throw new Exception("Community Idの取得に失敗");
          string uriString = htmlNode3.Element("img").Attributes["src"].Value;
          nicoCommynity.IconUrl = new Uri(uriString);
          IEnumerable<HtmlNode> source = htmlNode2.Element("p").Elements("strong");
          uint num1 = uint.Parse(source.ElementAt<HtmlNode>(0).InnerText);
          uint num2 = uint.Parse(source.ElementAt<HtmlNode>(1).InnerText.Trim(','));
          uint num3 = uint.Parse(source.ElementAt<HtmlNode>(2).InnerText.Trim(','));
          nicoCommynity.Level = num1;
          nicoCommynity.MemberCount = num2;
          nicoCommynity.VideoCount = num3;
          HtmlNode node = htmlNode1.Elements("td").ElementAt<HtmlNode>(1);
          string innerText1 = node.GetElementByClassName("date").InnerText;
          nicoCommynity.DateTime = innerText1;
          string innerText2 = node.GetElementByClassName("title").Element("a").InnerText;
          nicoCommynity.Name = innerText2;
          string innerText3 = node.GetElementByClassName("desc").InnerText;
          nicoCommynity.ShortDescription = innerText3;
          communitySearchPageHtml.Communities.Add(nicoCommynity);
        }
      }
      catch (Exception ex)
      {
        return communitySearchPageHtml;
      }
      communitySearchPageHtml.IsStatusOK = true;
      return communitySearchPageHtml;
    }

    public static Task<CommunitySearchResponse> CommunitySearchAsync(
      NiconicoContext context,
      string keyword,
      uint page,
      CommunitySearchSort sort,
      Order order,
      CommunitySearchMode mode)
    {
      return CommunityClient.GetCommunitySearchPageHtmlAsync(context, keyword, page, sort, order, mode).ContinueWith<CommunitySearchResponse>((Func<Task<string>, CommunitySearchResponse>) (prevTask => CommunityClient.ParseCommunitySearchPageHtml(prevTask.Result)));
    }
  }
}
