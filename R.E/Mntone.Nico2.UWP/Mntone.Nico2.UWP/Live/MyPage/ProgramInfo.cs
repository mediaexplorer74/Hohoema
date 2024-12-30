// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.MyPage.ProgramInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Mntone.Nico2.Live.MyPage
{
  public sealed class ProgramInfo
  {
    private const string FACE_OUT_IMAGE_URL = "img/10/cmn/icon/icon_face.gif?090827";
    private const string ENCOUNTER_IMAGE_URL = "img/10/cmn/icon/icon_totsu.gif?090827";
    private const string CRUISE_IMAGE_URL = "img/10/cmn/icon/icon_cruise.gif?110621";
    private readonly Regex communityImageSrcRegex = new Regex("^http://icon\\.nimg\\.jp/(?:community/s/\\d{1,12}/|channel/s/)(co\\d{1,14}|ch\\d{1,14})\\.jpg(?:\\?\\d+)?$");

    internal ProgramInfo(HtmlNode liveItemHtml, string language, bool isReserved = false)
    {
      this.CommunityType = ((IEnumerable<string>) liveItemHtml.GetAttributeValue("class", string.Empty).Split(' ')).Any<string>((Func<string, bool>) (c => c == "liveItem_ch")) ? CommunityType.Channel : CommunityType.Community;
      Match match = this.communityImageSrcRegex.Match(liveItemHtml.GetElementByTagName("a").GetElementByTagName("img").GetAttributeValue("src", string.Empty));
      if (match.Groups.Count >= 2)
        this.CommunityID = match.Groups[1].Value;
      HtmlNode elementByClassName = liveItemHtml.GetElementByClassName("liveItemTxt");
      string input = elementByClassName.GetElementByClassName("start_time").GetElementByTagName("strong").InnerText.Trim();
      if (isReserved)
      {
        switch (language)
        {
          case "en-us":
            DateTimeOffset dateTimeOffset1 = DateTimeOffset.ParseExact(input.Substring(0, 24), "MMM dd (ddd') Opens 'HH:mm", (IFormatProvider) new CultureInfo("en-us"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset1 = dateTimeOffset1.Subtract(TimeSpan.FromHours(9.0));
            this.OpenedAt = dateTimeOffset1;
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.ParseExact(input.Substring(0, 13) + input.Substring(26), "MMM dd (ddd') Starts 'HH:mm", (IFormatProvider) new CultureInfo("en-us"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset2 = dateTimeOffset2.Subtract(TimeSpan.FromHours(9.0));
            this.StartedAt = dateTimeOffset2;
            break;
          case "zh-tw":
            string str1 = input.Substring(0, 6) + "週" + input.Substring(6, 3);
            DateTimeOffset dateTimeOffset3 = DateTimeOffset.ParseExact(str1 + input.Substring(9, 8), "MM/dd(ddd') 進場 'HH:mm", (IFormatProvider) new CultureInfo("zh-tw"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset3 = dateTimeOffset3.Subtract(TimeSpan.FromHours(9.0));
            this.OpenedAt = dateTimeOffset3;
            this.StartedAt = DateTimeOffset.ParseExact(str1 + input.Substring(18), "MM/dd(ddd') 開場 'HH:mm", (IFormatProvider) new CultureInfo("zh-tw"), DateTimeStyles.AssumeUniversal).Subtract(TimeSpan.FromHours(9.0));
            break;
          default:
            DateTimeOffset dateTimeOffset4 = DateTimeOffset.ParseExact(input.Substring(0, 17), "MM/dd(ddd') 開場 'HH:mm", (IFormatProvider) new CultureInfo("ja-jp"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset4 = dateTimeOffset4.Subtract(TimeSpan.FromHours(9.0));
            this.OpenedAt = dateTimeOffset4;
            DateTimeOffset dateTimeOffset5 = DateTimeOffset.ParseExact(input.Substring(0, 9) + input.Substring(18), "MM/dd(ddd') 開演 'HH:mm", (IFormatProvider) new CultureInfo("ja-jp"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset5 = dateTimeOffset5.Subtract(TimeSpan.FromHours(9.0));
            this.StartedAt = dateTimeOffset5;
            break;
        }
      }
      else
      {
        switch (language)
        {
          case "en-us":
            DateTimeOffset dateTimeOffset6 = DateTimeOffset.ParseExact(input, "'Starts: 'MM/dd(ddd) HH:mm", (IFormatProvider) new CultureInfo("en-us"), DateTimeStyles.AssumeUniversal);
            dateTimeOffset6 = dateTimeOffset6.Subtract(TimeSpan.FromHours(9.0));
            this.OpenedAt = this.StartedAt = dateTimeOffset6;
            goto label_11;
          case "zh-tw":
            input = input.Substring(0, 6) + "週" + input.Substring(6);
            break;
        }
        DateTimeOffset dateTimeOffset7 = DateTimeOffset.ParseExact(input, "MM/dd(ddd) HH:mm 開始", (IFormatProvider) new CultureInfo("ja-jp"), DateTimeStyles.AssumeUniversal);
        dateTimeOffset7 = dateTimeOffset7.Subtract(TimeSpan.FromHours(9.0));
        this.OpenedAt = this.StartedAt = dateTimeOffset7;
      }
label_11:
      HtmlNode elementByTagName1 = elementByClassName.GetElementByTagName("h3");
      if (this.IsCommunity)
      {
        IEnumerable<string> source = elementByTagName1.GetElementsByTagName("img").Select<HtmlNode, string>((Func<HtmlNode, string>) (img => img.GetAttributeValue("src", string.Empty)));
        this.Category = source.First<string>().ToDetailCategory();
        foreach (string str2 in source.Skip<string>(1))
        {
          switch (str2)
          {
            case "img/10/cmn/icon/icon_face.gif?090827":
              this.IsFaceOut = true;
              continue;
            case "img/10/cmn/icon/icon_totsu.gif?090827":
              this.IsEnconter = true;
              continue;
            case "img/10/cmn/icon/icon_cruise.gif?110621":
              this.IsCruise = true;
              continue;
            default:
              continue;
          }
        }
      }
      HtmlNode elementByTagName2 = elementByTagName1.GetElementByTagName("a");
      this.Title = elementByTagName2.GetAttributeValue("title", string.Empty);
      string attributeValue = elementByTagName2.GetAttributeValue("href", string.Empty);
      int startIndex = this.IsCommunity ? 31 : 30;
      int num = attributeValue.IndexOf('?');
      this.ID = num < 0 ? attributeValue.Substring(startIndex) : attributeValue.Substring(startIndex, num - startIndex);
      this.CommunityName = elementByClassName.GetElementsByTagName("p").Last<HtmlNode>().GetAttributeValue("title", string.Empty);
    }

    public bool IsOfficial => this.CommunityType == CommunityType.Official;

    public bool IsChannel => this.CommunityType == CommunityType.Channel;

    public bool IsCommunity => this.CommunityType == CommunityType.Community;

    public CommunityType CommunityType { get; internal set; }

    public DetailCategoryType Category { get; private set; }

    public bool IsFaceOut { get; private set; }

    public bool IsEnconter { get; private set; }

    public bool IsCruise { get; private set; }

    public DateTimeOffset OpenedAt { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public string Title { get; private set; }

    public string ID { get; private set; }

    public string CommunityName { get; private set; }

    public string CommunityID { get; private set; }
  }
}
