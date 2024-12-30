// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Comment
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Comment
  {
    internal Comment(XElement streamXml, CommentServer commentServer)
    {
      this.IsLocked = streamXml.GetNamedChildNodeText("comment_lock").ToBooleanFrom1();
      string namedChildNodeText1 = streamXml.GetNamedChildNodeText("font_scale");
      this.Scale = !string.IsNullOrEmpty(namedChildNodeText1) ? namedChildNodeText1.ToSingle() : 1f;
      this.Perm = streamXml.GetNamedChildNodeText("perm");
      if (streamXml.GetNamedChildNodeText("split_top").ToBooleanFrom1())
        this.Position = CommentPosition.Bottom;
      else if (streamXml.GetNamedChildNodeText("split_bottom").ToBooleanFrom1())
      {
        this.Position = CommentPosition.Top;
      }
      else
      {
        int num = streamXml.GetNamedChildNodeText("header_comment").ToBooleanFrom1() ? 1 : 0;
        bool booleanFrom1 = streamXml.GetNamedChildNodeText("footer_comment").ToBooleanFrom1();
        this.Position = num == 0 ? (booleanFrom1 ? CommentPosition.Bottom : CommentPosition.Default) : (booleanFrom1 ? CommentPosition.Both : CommentPosition.Top);
      }
      this.FilteringLevel = (CommentFilteringLevel) streamXml.GetNamedChildNodeText("ng_scoring").ToUShort();
      this.SexMode = (CommentSexMode) streamXml.GetNamedChildNodeText("danjo_comment_mode").ToInt();
      XElement namedChildNode = streamXml.GetNamedChildNode("quesheet");
      if (namedChildNode != null)
        this.Commands = (IReadOnlyList<Command>) namedChildNode.GetChildNodes().Select<XElement, Command>((Func<XElement, Command>) (queXml => new Command(queXml))).ToList<Command>();
      this.IsRestrict = streamXml.GetNamedChildNodeText("is_restrict").ToBooleanFrom1();
      string namedChildNodeText2 = streamXml.GetNamedChildNodeText("product_comment");
      if (!string.IsNullOrEmpty(namedChildNodeText2))
        this.LimitMode = (CommentLimitMode) namedChildNodeText2.ToInt();
      this.Server = commentServer;
    }

    public bool IsLocked { get; private set; }

    public float Scale { get; private set; }

    public string Perm { get; private set; }

    public CommentPosition Position { get; private set; }

    public CommentFilteringLevel FilteringLevel { get; private set; }

    public CommentSexMode SexMode { get; private set; }

    public IReadOnlyList<Command> Commands { get; private set; }

    public bool IsRestrict { get; private set; }

    public CommentLimitMode LimitMode { get; private set; }

    public CommentServer Server { get; private set; }
  }
}
