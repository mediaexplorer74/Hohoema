// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Info.InfoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Users.Info
{
  internal sealed class InfoClient
  {
    public static Task<string> GetInfoDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.UserPageUrl + "/top");
    }

    public static InfoResponse ParseInfoData(string userInfoData)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(userInfoData);
      HtmlNode htmlNode1 = htmlDocument.DocumentNode.Element("html");
      string attributeValue = htmlNode1.GetAttributeValue("lang", "ja-jp");
      HtmlNode htmlNode2 = htmlNode1.Element("head");
      string str = "var User = ";
      int num = htmlNode2.InnerHtml.IndexOf(str);
      string data = new string(htmlNode2.InnerHtml.Skip<char>(str.Length + num).TakeWhile<char>((Func<char, bool>) (x => x != ';')).ToArray<char>()).Replace("!!document.cookie.match(/nicoadult\\s*=\\s*1/)", "");
      UserMyPageJSInfo info;
      try
      {
        info = JsonSerializerExtensions.Load<UserMyPageJSInfo>(data);
      }
      catch (Exception ex)
      {
        ex.Data.Add((object) "User Info Json", (object) data);
        throw ex;
      }
      return new InfoResponse(htmlNode1.Element("body"), attributeValue, info);
    }

    public static Task<InfoResponse> GetInfoAsync(NiconicoContext context)
    {
      return InfoClient.GetInfoDataAsync(context).ContinueWith<InfoResponse>((Func<Task<string>, InfoResponse>) (prevTask => InfoClient.ParseInfoData(prevTask.Result)));
    }
  }
}
