// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.UserMylist.UserMylistClient
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
namespace Mntone.Nico2.Mylist.UserMylist
{
  public class UserMylistClient
  {
    public static async Task<string> GetUserMylistDataAsync(NiconicoContext context, string user_id)
    {
      return await context.GetClient().GetConvertedStringAsync(NiconicoUrls.MakeUserPageUrl(user_id) + "/mylist");
    }

    private static List<MylistGroupData> ParseMylistPageHtml(string rawHtml)
    {
      List<MylistGroupData> mylistGroupDataList = new List<MylistGroupData>();
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(rawHtml);
      return htmlDocument.DocumentNode.GetElementByTagName("html").GetElementByTagName("body").GetElementByClassName("wrapper").GetElementById("mylist").GetElementByClassName("articleBody").GetElementsByClassName("outer").Select<HtmlNode, MylistGroupData>((Func<HtmlNode, MylistGroupData>) (x =>
      {
        MylistGroupData mylistPageHtml = new MylistGroupData();
        HtmlNode elementByClassName = x.GetElementByClassName("section");
        HtmlNode elementByTagName1 = elementByClassName.GetElementByTagName("h4");
        HtmlNode elementByTagName2 = elementByTagName1.GetElementByTagName("a");
        mylistPageHtml.Id = new string(elementByTagName2.GetAttributeValue("href", "").Skip<char>("mylist/".Count<char>()).ToArray<char>());
        mylistPageHtml.Name = elementByTagName2.InnerText;
        mylistPageHtml.Count = int.Parse(new string(elementByTagName1.GetElementByTagName("span").InnerText.Skip<char>(2).TakeWhile<char>((Func<char, bool>) (c => c >= '0' && c <= '9')).ToArray<char>()));
        mylistPageHtml.IconId = elementByTagName2.GetElementByClassName("folderIcon").GetAttributeValue("class", "").Last<char>().ToString();
        IEnumerable<HtmlNode> elementsByClassName = elementByClassName.GetElementsByClassName("mylistDescription");
        HtmlNode htmlNode = elementsByClassName != null ? elementsByClassName.SingleOrDefault<HtmlNode>((Func<HtmlNode, bool>) (y => y.GetAttributeValue("data-nico-mylist-desc-full", "") == "true")) : (HtmlNode) null;
        mylistPageHtml.Description = htmlNode != null ? htmlNode.InnerText : "";
        mylistPageHtml.ThumbnailUrls = x.GetElementByClassName("thumbContainer").GetElementByTagName("ul").GetElementsByTagName("li").Select<HtmlNode, Uri>((Func<HtmlNode, Uri>) (thumb => new Uri(thumb.GetElementByTagName("img").GetAttributeValue("src", "")))).ToList<Uri>();
        return mylistPageHtml;
      })).ToList<MylistGroupData>();
    }

    public static List<MylistGroupData> ParseRss(string rss)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (UserMylistRss));
      UserMylistRss userMylistRss = (UserMylistRss) null;
      using (StringReader stringReader = new StringReader(rss))
        userMylistRss = (UserMylistRss) xmlSerializer.Deserialize((TextReader) stringReader);
      return userMylistRss.Channel.Item.Select<Item, MylistGroupData>((Func<Item, MylistGroupData>) (x => new MylistGroupData()
      {
        Id = ((IEnumerable<string>) x.Link.Split('/')).Last<string>(),
        IsPublic = "1",
        Description = x.Description,
        Name = x.Title,
        ThumbnailUrls = new List<Uri>()
        {
          new Uri(x.Thumbnail.Url)
        }
      })).ToList<MylistGroupData>();
    }

    public static Task<List<MylistGroupData>> GetUserMylistAsync(
      NiconicoContext context,
      string user_id)
    {
      return UserMylistClient.GetUserMylistDataAsync(context, user_id).ContinueWith<List<MylistGroupData>>((Func<Task<string>, List<MylistGroupData>>) (prevTask => UserMylistClient.ParseMylistPageHtml(prevTask.Result)));
    }
  }
}
