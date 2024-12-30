// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.XmlDocumentExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2
{
  internal static class XmlDocumentExtensions
  {
    public static XElement GetDocumentRootNode(this XDocument node)
    {
      return node.Elements().First<XElement>();
    }

    public static XAttribute GetNamedAttribute(this XElement node, string name)
    {
      return node.Attribute((XName) name);
    }

    public static string GetText(this XAttribute node)
    {
      return node == null ? string.Empty : node.Value.Trim();
    }

    public static string GetNamedAttributeText(this XElement node, string name)
    {
      return node.GetNamedAttribute(name).GetText();
    }

    public static IEnumerable<XElement> GetChildNodes(this XElement node) => node.Elements();

    public static IEnumerable<XElement> GetNamedChildNodes(this XElement node, string name)
    {
      return node.Elements((XName) name);
    }

    public static XElement GetFirstChildNode(this XElement node)
    {
      return node.Elements().ElementAtOrDefault<XElement>(0);
    }

    public static XElement GetChildNodeAt(this XElement node, int index)
    {
      return node.Elements().ElementAtOrDefault<XElement>(index);
    }

    public static XElement GetNamedChildNode(this XElement node, string name)
    {
      return node.Element((XName) name);
    }

    public static string GetNamedChildNodeText(this XElement node, string name)
    {
      return node.GetNamedChildNode(name).GetText();
    }

    public static string GetName(this XElement node) => node.Name.LocalName;

    public static string GetText(this XElement node)
    {
      return node == null ? string.Empty : node.Value.Trim();
    }
  }
}
