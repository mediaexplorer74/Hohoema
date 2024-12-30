// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NMSG_Response
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  public class NMSG_Response
  {
    public NGMS_Thread_Response Thread { get; set; }

    internal List<JToken> _CommentsSource { get; } = new List<JToken>();

    public IEnumerable<NMSG_Chat> ParseComments()
    {
      return this._CommentsSource.Select<JToken, NMSG_Chat>((Func<JToken, NMSG_Chat>) (x => x.ToObject<NMSG_Chat>()));
    }

    public NGMS_GlobalNumRes GlobalNumRes { get; set; }
  }
}
