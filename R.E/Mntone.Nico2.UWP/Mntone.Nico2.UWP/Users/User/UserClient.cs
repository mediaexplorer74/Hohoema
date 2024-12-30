// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.User.UserClient
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
namespace Mntone.Nico2.Users.User
{
  internal sealed class UserClient
  {
    private static Task<string> GetUserDetailDataAsync(NiconicoContext context, string user_id)
    {
      return context.GetClient().GetConvertedStringAsync(string.Format("{0}/video", (object) NiconicoUrls.MakeUserPageUrl(user_id)));
    }

    private static UserDetail ParseUserDetailData(string rawHtml)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(rawHtml);
      UserDetail userDetailData = new UserDetail();
      HtmlNode elementByTagName1 = htmlDocument.DocumentNode.GetElementByTagName("html").GetElementByTagName("body");
      HtmlNode elementByClassName1 = elementByTagName1.GetElementByClassName("userDetail");
      HtmlNode elementByClassName2 = elementByClassName1.GetElementByClassName("avatar");
      HtmlNode elementByClassName3 = elementByClassName1.GetElementByClassName("profile");
      userDetailData.ThumbnailUri = elementByClassName2.GetElementByTagName("img").GetAttributeValue("src", "");
      string innerText = elementByClassName3.GetElementByTagName("h2").InnerText;
      userDetailData.Nickname = innerText.Remove(innerText.Length - 2);
      HtmlNode[] array1 = elementByClassName3.GetElementByClassName("account").GetElementsByTagName("p").ToArray<HtmlNode>();
      userDetailData.IsPremium = array1[0].GetElementByTagName("span").InnerText.EndsWith("プレミアム会員");
      try
      {
        uint[] array2 = ((IEnumerable<HtmlNode>) elementByClassName3.GetElementByClassName("stats").SelectNodes("./li//span")).Select<HtmlNode, uint>((Func<HtmlNode, uint>) (x => uint.Parse(string.Join<char>("", x.InnerText.Where<char>((Func<char, bool>) (y => y != ',')).TakeWhile<char>((Func<char, bool>) (y => y >= '0' && y <= '9')))))).ToArray<uint>();
        userDetailData.FollowerCount = array2[0];
        userDetailData.StampCount = array2[1];
      }
      catch (Exception ex)
      {
      }
      try
      {
        UserDetail userDetail = userDetailData;
        HtmlNode elementById1 = elementByClassName3.GetElementByClassName("userDetailComment").GetElementById("user_description");
        string str;
        if (elementById1 == null)
        {
          str = (string) null;
        }
        else
        {
          HtmlNode elementById2 = elementById1.GetElementById("description_full");
          str = elementById2 != null ? elementById2.GetElementByTagName("span")?.InnerHtml : (string) null;
        }
        if (str == null)
          str = "";
        userDetail.Description = str;
      }
      catch
      {
        userDetailData.Description = "";
      }
      HtmlNode elementById = elementByTagName1.GetElementByClassName("wrapper").GetElementById("video");
      try
      {
        HtmlNode elementByTagName2 = elementById.GetElementByTagName("h3");
        if (elementByTagName2 != null)
        {
          string str = new string(elementByTagName2.InnerText.Skip<char>(5).TakeWhile<char>((Func<char, bool>) (x => x >= '0' && x <= '9')).ToArray<char>());
          if (str.Count<char>() > 0)
            userDetailData.TotalVideoCount = uint.Parse(str);
          else
            userDetailData.IsOwnerVideoPrivate = true;
        }
        else
          userDetailData.IsOwnerVideoPrivate = true;
      }
      catch
      {
        userDetailData.TotalVideoCount = 0U;
        userDetailData.IsOwnerVideoPrivate = true;
      }
      return userDetailData;
    }

    public static Task<UserDetail> GetUserDetailAsync(NiconicoContext context, string user_id)
    {
      return UserClient.GetUserDetailDataAsync(context, user_id).ContinueWith<UserDetail>((Func<Task<string>, UserDetail>) (prevTask => UserClient.ParseUserDetailData(prevTask.Result)));
    }

    private static Task<string> GetUserDataAsync(NiconicoContext context, string user_id)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}?{1}={2}", (object) NiconicoUrls.UserPageUrl, (object) nameof (user_id), (object) user_id));
    }

    private static Mntone.Nico2.Users.User.User ParseUserData(string xml)
    {
      using (StringReader stringReader = new StringReader(xml))
        return (Mntone.Nico2.Users.User.User) new XmlSerializer(typeof (UserResponse)).Deserialize((TextReader) stringReader);
    }

    public static Task<Mntone.Nico2.Users.User.User> GetUserAsync(
      NiconicoContext context,
      string user_id)
    {
      return UserClient.GetUserDataAsync(context, user_id).ContinueWith<Mntone.Nico2.Users.User.User>((Func<Task<string>, Mntone.Nico2.Users.User.User>) (prevTask => UserClient.ParseUserData(prevTask.Result)));
    }
  }
}
