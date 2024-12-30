// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Comment
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images
{
  public sealed class Comment
  {
    internal Comment(XElement commentXml)
    {
      this.Id = commentXml.GetNamedChildNodeText("comment_id").ToULong();
      this.ImageId = "im" + commentXml.GetNamedChildNodeText("image_id");
      this.ResId = commentXml.GetNamedChildNodeText("res_id").ToULong();
      this.Value = commentXml.GetNamedChildNodeText("content");
      this.Command = commentXml.GetNamedChildNodeText("command");
      this.PostedAt = (commentXml.GetNamedChildNodeText("created") + "+09:00").ToDateTimeOffsetFromIso8601();
      this.Frame = commentXml.GetNamedChildNodeText("frame").ToInt();
      this.UserHash = commentXml.GetNamedChildNodeText("user_hash");
      this.IsAnonymous = commentXml.GetNamedChildNodeText("anonymous_flag").ToBooleanFrom1();
    }

    public ulong Id { get; private set; }

    public string ImageId { get; private set; }

    public ulong ResId { get; private set; }

    public string Value { get; private set; }

    public string Command { get; private set; }

    public DateTimeOffset PostedAt { get; private set; }

    public int Frame { get; private set; }

    public string UserHash { get; private set; }

    public bool IsAnonymous { get; private set; }
  }
}
