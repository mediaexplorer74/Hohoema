﻿// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NGMS_Leaf
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [DataContract]
  public class NGMS_Leaf
  {
    [DataMember(Name = "thread")]
    public string Thread { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "leaf")]
    public int? Leaf { get; set; }
  }
}
