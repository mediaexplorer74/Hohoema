// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Thumbnail.ThumbnailTags
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Thumbnail
{
  public sealed class ThumbnailTags
  {
    internal ThumbnailTags(XElement tagsXml)
    {
      this.Domain = tagsXml.GetNamedAttributeText("domain");
      this.Value = (IReadOnlyList<ThumbnailTag>) tagsXml.GetChildNodes().Select<XElement, ThumbnailTag>((Func<XElement, ThumbnailTag>) (tagXml => new ThumbnailTag(tagXml))).ToList<ThumbnailTag>();
    }

    public string Domain { get; private set; }

    public IReadOnlyList<ThumbnailTag> Value { get; private set; }
  }
}
