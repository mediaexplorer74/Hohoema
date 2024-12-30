// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.MyPage.MyPageResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Live.MyPage
{
  public sealed class MyPageResponse
  {
    private List<TimeshiftProgramInfo> _TimeshiftPrograms;
    private List<ProgramInfo> _OnAirPrograms;
    private List<ProgramInfo> _ReservedPrograms;

    internal MyPageResponse(HtmlNode liveListHtml, string language)
    {
      this._TimeshiftPrograms = liveListHtml.GetElementById("TS_list").GetElementById("liveItemsWrap").GetElementById("liveItemsWrap").GetElementsByClassName("liveItems").SelectMany<HtmlNode, HtmlNode>((Func<HtmlNode, IEnumerable<HtmlNode>>) (i => i.GetElementsByClassName("column"))).Select<HtmlNode, HtmlNode>((Func<HtmlNode, HtmlNode>) (i => i.GetElementByClassName("name").GetElementByTagName("a"))).Select<HtmlNode, TimeshiftProgramInfo>((Func<HtmlNode, TimeshiftProgramInfo>) (i => new TimeshiftProgramInfo(i.GetAttributeValue("href", string.Empty).Substring(30), i.GetAttributeValue("title", string.Empty)))).ToList<TimeshiftProgramInfo>();
      HtmlNode elementById1 = liveListHtml.GetElementById("Favorite_list");
      HtmlNode elementById2 = elementById1.GetElementById("subscribeItemsWrap");
      IEnumerable<HtmlNode> elementsByClassName1 = elementById2 != null ? elementById2.GetElementsByClassName("liveItems") : (IEnumerable<HtmlNode>) null;
      if (elementsByClassName1 != null)
      {
        HtmlNode elementById3 = elementById1.GetElementById("all_subscribeItemsWrap");
        IEnumerable<HtmlNode> elementsByClassName2 = elementById3 != null ? elementById3.GetElementsByClassName("liveItems") : (IEnumerable<HtmlNode>) null;
        IEnumerable<HtmlNode> htmlNodes = elementsByClassName1;
        if (elementsByClassName2 != null)
          htmlNodes = htmlNodes.Union<HtmlNode>(elementsByClassName2);
        this._OnAirPrograms = htmlNodes.SelectMany<HtmlNode, HtmlNode>((Func<HtmlNode, IEnumerable<HtmlNode>>) (i => i.GetElementsByTagName("div"))).Select<HtmlNode, ProgramInfo>((Func<HtmlNode, ProgramInfo>) (i => new ProgramInfo(i, language))).ToList<ProgramInfo>();
      }
      else
        this._OnAirPrograms = new List<ProgramInfo>();
      HtmlNode elementById4 = elementById1.GetElementById("subscribeReservedItemsWrap");
      IEnumerable<HtmlNode> elementsByClassName3 = elementById4 != null ? elementById4.GetElementsByClassName("liveItems") : (IEnumerable<HtmlNode>) null;
      if (elementsByClassName3 != null)
      {
        HtmlNode elementById5 = elementById1.GetElementById("all_subscribeReservedItemsWrap");
        IEnumerable<HtmlNode> elementsByClassName4 = elementById5 != null ? elementById5.GetElementsByClassName("liveItems") : (IEnumerable<HtmlNode>) null;
        IEnumerable<HtmlNode> htmlNodes = elementsByClassName3;
        if (elementsByClassName4 != null)
          htmlNodes = htmlNodes.Union<HtmlNode>(elementsByClassName4);
        this._ReservedPrograms = htmlNodes.SelectMany<HtmlNode, HtmlNode>((Func<HtmlNode, IEnumerable<HtmlNode>>) (i => i.GetElementsByTagName("div"))).Select<HtmlNode, ProgramInfo>((Func<HtmlNode, ProgramInfo>) (i => new ProgramInfo(i, language, true))).ToList<ProgramInfo>();
      }
      else
        this._ReservedPrograms = new List<ProgramInfo>();
    }

    public IReadOnlyList<TimeshiftProgramInfo> TimeshiftPrograms
    {
      get => (IReadOnlyList<TimeshiftProgramInfo>) this._TimeshiftPrograms;
    }

    public IReadOnlyList<ProgramInfo> OnAirPrograms
    {
      get => (IReadOnlyList<ProgramInfo>) this._OnAirPrograms;
    }

    public IReadOnlyList<ProgramInfo> ReservedPrograms
    {
      get => (IReadOnlyList<ProgramInfo>) this._ReservedPrograms;
    }
  }
}
