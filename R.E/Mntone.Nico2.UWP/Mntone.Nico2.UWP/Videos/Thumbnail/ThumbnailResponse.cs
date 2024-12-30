// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Thumbnail.ThumbnailResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Thumbnail
{
  public sealed class ThumbnailResponse
  {
    internal ThumbnailResponse(XElement thumbXml)
    {
      this.Id = thumbXml.GetNamedChildNodeText("video_id");
      this.Title = thumbXml.GetNamedChildNodeText("title");
      this.Description = thumbXml.GetNamedChildNodeText("description");
      this.ThumbnailUrl = thumbXml.GetNamedChildNodeText("thumbnail_url").ToUri();
      this.PostedAt = thumbXml.GetNamedChildNodeText("first_retrieve").ToDateTimeOffsetFromIso8601();
      this.Length = thumbXml.GetNamedChildNodeText("length").ToTimeSpan();
      this.MovieType = thumbXml.GetNamedChildNodeText("movie_type").ToMovieType();
      this.SizeHigh = thumbXml.GetNamedChildNodeText("size_high").ToULong();
      this.SizeLow = thumbXml.GetNamedChildNodeText("size_low").ToULong();
      this.ViewCount = thumbXml.GetNamedChildNodeText("view_counter").ToUInt();
      this.CommentCount = thumbXml.GetNamedChildNodeText("comment_num").ToUInt();
      this.MylistCount = thumbXml.GetNamedChildNodeText("mylist_counter").ToUInt();
      this.LastCommentBody = thumbXml.GetNamedChildNodeText("last_res_body");
      this.PageUrl = thumbXml.GetNamedChildNodeText("watch_url").ToUri();
      this.ThumbnailType = thumbXml.GetNamedChildNodeText("thumb_type").ToThumbnailType();
      this.IsEmbeddable = thumbXml.GetNamedChildNodeText("embeddable").ToBooleanFrom1();
      this.CannotPlayInLive = thumbXml.GetNamedChildNodeText("no_live_play").ToBooleanFrom1();
      this.Tags = new ThumbnailTags(thumbXml.GetNamedChildNode("tags"));
      XElement namedChildNode1 = thumbXml.GetNamedChildNode("user_id");
      if (namedChildNode1 != null)
      {
        this.UserType = UserType.User;
        this.UserId = namedChildNode1.GetText().ToUInt();
        this.UserName = thumbXml.GetNamedChildNodeText("user_nickname");
        this.UserIconUrl = thumbXml.GetNamedChildNodeText("user_icon_url").ToUri();
      }
      else
      {
        XElement namedChildNode2 = thumbXml.GetNamedChildNode("ch_id");
        if (namedChildNode2 == null)
          throw new ArgumentException();
        this.UserType = UserType.Channel;
        this.UserId = namedChildNode2.GetText().ToUInt();
        this.UserName = thumbXml.GetNamedChildNodeText("ch_name");
        this.UserIconUrl = thumbXml.GetNamedChildNodeText("ch_icon_url").ToUri();
      }
    }

    public string Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public Uri ThumbnailUrl { get; private set; }

    public DateTimeOffset PostedAt { get; private set; }

    public TimeSpan Length { get; private set; }

    public MovieType MovieType { get; private set; }

    public ulong SizeHigh { get; private set; }

    public ulong SizeLow { get; private set; }

    public uint ViewCount { get; private set; }

    public uint CommentCount { get; private set; }

    public uint MylistCount { get; private set; }

    public string LastCommentBody { get; private set; }

    public Uri PageUrl { get; private set; }

    public ThumbnailType ThumbnailType { get; private set; }

    public bool IsEmbeddable { get; private set; }

    public bool CannotPlayInLive { get; private set; }

    public ThumbnailTags Tags { get; private set; }

    public UserType UserType { get; private set; }

    public uint UserId { get; private set; }

    public string UserName { get; private set; }

    public Uri UserIconUrl { get; private set; }
  }
}
