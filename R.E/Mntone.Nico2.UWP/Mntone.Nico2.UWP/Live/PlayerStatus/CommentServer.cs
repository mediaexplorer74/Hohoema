// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.CommentServer
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
  public sealed class CommentServer
  {
    internal CommentServer(XElement commentServerXml, XElement threadIdsXml)
    {
      this.Host = commentServerXml.GetNamedChildNodeText("addr");
      this.Port = commentServerXml.GetNamedChildNodeText("port").ToUShort();
      if (threadIdsXml.GetFirstChildNode() != null)
        this.ThreadIds = (IReadOnlyList<uint>) threadIdsXml.GetChildNodes().Select<XElement, uint>((Func<XElement, uint>) (threadIdXml => threadIdXml.GetText().ToUInt())).ToList<uint>();
      else
        this.ThreadIds = (IReadOnlyList<uint>) new List<uint>()
        {
          commentServerXml.GetNamedChildNodeText("thread").ToUInt()
        };
    }

    public string Host { get; private set; }

    public ushort Port { get; private set; }

    public IReadOnlyList<uint> ThreadIds { get; private set; }
  }
}
