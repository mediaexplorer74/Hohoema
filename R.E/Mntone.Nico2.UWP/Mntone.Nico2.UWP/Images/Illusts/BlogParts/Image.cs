// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.BlogParts.Image
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Illusts.BlogParts
{
  public sealed class Image
  {
    internal Image(XElement imageXml)
    {
      this.Id = "im" + imageXml.GetNamedChildNodeText("id");
      this.Title = imageXml.GetNamedChildNodeText("title");
      this.UserName = imageXml.GetNamedChildNodeText("nickname");
    }

    public string Id { get; private set; }

    public string Title { get; private set; }

    public string UserName { get; private set; }
  }
}
