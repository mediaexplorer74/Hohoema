// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.HtmlDocumentExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2
{
  internal static class HtmlDocumentExtensions
  {
    public static IEnumerable<HtmlNode> GetElementsByClassName(this HtmlNode node, string className)
    {
      return ((IEnumerable<HtmlNode>) node.ChildNodes).Where<HtmlNode>((Func<HtmlNode, bool>) (n => ((IEnumerable<string>) n.GetAttributeValue("class", string.Empty).Split(' ')).Any<string>((Func<string, bool>) (s => s == className))));
    }

    public static IEnumerable<HtmlNode> GetElementsByTagName(this HtmlNode node, string tagName)
    {
      return ((IEnumerable<HtmlNode>) node.ChildNodes).Where<HtmlNode>((Func<HtmlNode, bool>) (n => n.Name == tagName));
    }

    public static HtmlNode GetElementByClassName(this HtmlNode node, string className)
    {
      return node.GetElementsByClassName(className).SingleOrDefault<HtmlNode>();
    }

    public static HtmlNode GetElementByTagName(this HtmlNode node, string tagName)
    {
      return node.GetElementsByTagName(tagName).SingleOrDefault<HtmlNode>();
    }

    public static HtmlNode GetElementById(this HtmlNode node, string idName)
    {
      return ((IEnumerable<HtmlNode>) node.ChildNodes).Where<HtmlNode>((Func<HtmlNode, bool>) (n => n.GetAttributeValue("id", string.Empty) == idName)).SingleOrDefault<HtmlNode>();
    }
  }
}
