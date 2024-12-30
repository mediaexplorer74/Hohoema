// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.BlogPartsRanking.BlogPartsRankingResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
  public sealed class BlogPartsRankingResponse
  {
    internal BlogPartsRankingResponse(XElement responseXml)
    {
      this.PageUrl = responseXml.GetNamedChildNodeText("icon_url").ToUri();
      XElement namedChildNode = responseXml.GetNamedChildNode("image_list");
      if (namedChildNode.GetFirstChildNode().GetFirstChildNode() != null)
        this.Images = (IReadOnlyList<Image>) namedChildNode.GetChildNodes().Select<XElement, Image>((Func<XElement, Image>) (imageXml => new Image(imageXml))).ToList<Image>();
      else
        this.Images = (IReadOnlyList<Image>) new List<Image>();
    }

    public Uri PageUrl { get; private set; }

    public IReadOnlyList<Image> Images { get; private set; }
  }
}
