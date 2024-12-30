// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Tags.TagsResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Live.Tags
{
  public sealed class TagsResponse
  {
    private List<TagInfo> _Tags;

    internal TagsResponse(HtmlNode ulHtml)
    {
      this.IsEditable = ((IEnumerable<HtmlNode>) ulHtml.ChildNodes).Reverse<HtmlNode>().Any<HtmlNode>((Func<HtmlNode, bool>) (child => child.GetElementByClassName("edit") != null));
      this._Tags = ((IEnumerable<HtmlNode>) ulHtml.ChildNodes).Where<HtmlNode>((Func<HtmlNode, bool>) (child => child.GetElementByClassName("nicopedia") != null)).Select<HtmlNode, TagInfo>((Func<HtmlNode, TagInfo>) (child =>
      {
        int num1 = child.GetElementByClassName("category") != null ? 1 : 0;
        string innerText1 = child.GetElementByClassName("nicopedia").InnerText;
        string innerText2 = child.GetElementByClassName("npit").InnerText;
        ushort num2 = innerText2.Substring(1, innerText2.Length - 2).ToUShort();
        string str = innerText1;
        int count = (int) num2;
        return new TagInfo(num1 != 0, str, (ushort) count);
      })).ToList<TagInfo>();
    }

    public bool IsEditable { get; private set; }

    public IReadOnlyList<TagInfo> Tags => (IReadOnlyList<TagInfo>) this._Tags;
  }
}
