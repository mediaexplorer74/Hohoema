// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Description.DescriptionResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using Mntone.Nico2.Live.Tags;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Live.Description
{
  public sealed class DescriptionResponse
  {
    private List<TagInfo> _Tags;

    internal DescriptionResponse(HtmlNode coverHtml, string language)
    {
      HtmlNode elementByClassName1 = coverHtml.GetElementByClassName("container").GetElementById("gate").GetElementByClassName("infobox");
      HtmlNode node1 = elementByClassName1.Element("h2");
      this.IsHighQuality = node1.GetAttributeValue("class", string.Empty) == "hq";
      this.Title = node1.GetElementByClassName("program-title").InnerText;
      HtmlNode elementByClassName2 = elementByClassName1.GetElementByClassName("textbox");
      HtmlNode elementByClassName3 = elementByClassName2.GetElementByClassName("bg");
      HtmlNode node2 = elementByClassName3 != null ? elementByClassName3.GetElementByClassName("lebox") : elementByClassName2.GetElementByClassName("lebox");
      HtmlNode elementById = node2.GetElementById("bn_gbox");
      HtmlNode htmlNode1 = elementById.GetElementByClassName("bn").Element("meta");
      if (htmlNode1.GetAttributeValue("itemprop", string.Empty) == "thumbnail")
        this.ThumbnailUrl = htmlNode1.GetAttributeValue("content", string.Empty).ToUri();
      HtmlNode htmlNode2 = elementById.GetElementByClassName("blbox").GetElementByClassName("hmf").GetElementByClassName("kaijo").Element("meta");
      if (htmlNode2.GetAttributeValue("itemprop", string.Empty) == "datePublished")
        this.OpenedAt = htmlNode2.GetAttributeValue("content", string.Empty).ToDateTimeOffsetFromIso8601();
      this.Description = node2.GetElementByClassName("bgm").GetElementByClassName("text_area").InnerHtml;
      HtmlNode elementByTagName1 = node2.GetElementById("livetags").GetElementByTagName("table");
      HtmlNode elementByTagName2 = elementByTagName1.GetElementByTagName("tbody");
      this._Tags = (elementByTagName2 != null ? elementByTagName2.GetElementByTagName("tr") : elementByTagName1.GetElementByTagName("tr")).GetElementsByTagName("td").Last<HtmlNode>().GetElementsByTagName("nobr").Select<HtmlNode, TagInfo>((Func<HtmlNode, TagInfo>) (child =>
      {
        HtmlNode elementByTagName3 = child.GetElementByTagName("img");
        bool isCategoryTag = elementByTagName3 != null && elementByTagName3.GetAttributeValue("src", string.Empty) == "img/watch/icon_ctgry/ja-jp.gif";
        string str = child.GetElementByClassName("nicopedia").InnerText.Trim(' ', '\t', '\n');
        string innerText = child.GetElementByClassName("npit").InnerText;
        ushort count = !string.IsNullOrEmpty(innerText) ? innerText.Substring(1, innerText.Length - 2).ToUShort() : (ushort) 0;
        return new TagInfo(isCategoryTag, str, count);
      })).ToList<TagInfo>();
    }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public bool IsHighQuality { get; private set; }

    public Uri ThumbnailUrl { get; private set; }

    public DateTimeOffset OpenedAt { get; private set; }

    public IReadOnlyList<TagInfo> Tags => (IReadOnlyList<TagInfo>) this._Tags;
  }
}
