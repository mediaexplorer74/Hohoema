﻿// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Embed.Ichiba.Polling
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Embed.Ichiba
{
  [DataContract]
  public sealed class Polling
  {
    [DataMember(Name = "shortIntarval")]
    public int ShortIntarval { get; set; }

    [DataMember(Name = "longIntarval")]
    public int LongIntarval { get; set; }

    [DataMember(Name = "defaultIntarval")]
    public int DefaultIntarval { get; set; }

    [DataMember(Name = "maxNoChangeCount")]
    public int MaxNoChangeCount { get; set; }
  }
}
