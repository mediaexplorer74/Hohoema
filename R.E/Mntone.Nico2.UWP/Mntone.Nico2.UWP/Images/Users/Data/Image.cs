// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.Data.Image
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Images.Illusts;
using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Users.Data
{
  public sealed class Image
  {
    internal Image(XElement imageXml)
    {
      this.Id = "im" + imageXml.GetNamedChildNodeText("id");
      this.UserId = imageXml.GetNamedChildNodeText("user_id").ToUInt();
      this.Title = imageXml.GetNamedChildNodeText("title");
      this.Description = imageXml.GetNamedChildNodeText("description");
      this.ViewCount = imageXml.GetNamedChildNodeText("view_count").ToUInt();
      this.CommentCount = imageXml.GetNamedChildNodeText("comment_count").ToUInt();
      this.ClipCount = imageXml.GetNamedChildNodeText("clip_count").ToUInt();
      this.LastCommentBody = imageXml.GetNamedChildNodeText("summary");
      this.Genre = (Genre) imageXml.GetNamedChildNodeText("genre").ToInt();
      this.ImageType = imageXml.GetNamedChildNodeText("image_type").ToUShort();
      this.IllustType = imageXml.GetNamedChildNodeText("illust_type").ToUShort();
      this.InspectionStatus = imageXml.GetNamedChildNodeText("inspection_status").ToUShort();
      this.IsAnonymous = imageXml.GetNamedChildNodeText("anonymous_flag").ToBooleanFrom1();
      this.PublicStatus = imageXml.GetNamedChildNodeText("public_status").ToUShort();
      this.IsDeleted = imageXml.GetNamedChildNodeText("delete_flag").ToBooleanFrom1();
      this.DeleteType = imageXml.GetNamedChildNodeText("delete_type").ToUShort();
      this.PostedAt = (imageXml.GetNamedChildNodeText("created") + "+09:00").ToDateTimeOffsetFromIso8601();
    }

    public string Id { get; private set; }

    public uint UserId { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public uint ViewCount { get; private set; }

    public uint CommentCount { get; private set; }

    public uint ClipCount { get; private set; }

    public string LastCommentBody { get; private set; }

    public Genre Genre { get; private set; }

    public bool IsAnonymous { get; private set; }

    public ushort ImageType { get; private set; }

    public ushort IllustType { get; private set; }

    public ushort InspectionStatus { get; private set; }

    public ushort PublicStatus { get; private set; }

    public bool IsDeleted { get; private set; }

    public ushort DeleteType { get; private set; }

    public DateTimeOffset PostedAt { get; private set; }
  }
}
