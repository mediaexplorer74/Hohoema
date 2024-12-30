// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.Data.DataResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Users.Data
{
  public sealed class DataResponse
  {
    internal DataResponse(XElement responseXml)
    {
      XElement namedChildNode1 = responseXml.GetNamedChildNode("image_list");
      this.Images = namedChildNode1.GetFirstChildNode().GetFirstChildNode() == null ? (IReadOnlyList<Image>) new List<Image>() : (IReadOnlyList<Image>) namedChildNode1.GetChildNodes().Select<XElement, Image>((Func<XElement, Image>) (imageXml => new Image(imageXml))).ToList<Image>();
      XElement namedChildNode2 = responseXml.GetNamedChildNode("comment_list");
      if (namedChildNode2.GetFirstChildNode().GetFirstChildNode() != null)
        this.Comments = (IReadOnlyList<Comment>) namedChildNode2.GetChildNodes().Select<XElement, Comment>((Func<XElement, Comment>) (commentXml => new Comment(commentXml))).ToList<Comment>();
      else
        this.Comments = (IReadOnlyList<Comment>) new List<Comment>();
    }

    public IReadOnlyList<Image> Images { get; private set; }

    public IReadOnlyList<Comment> Comments { get; private set; }
  }
}
