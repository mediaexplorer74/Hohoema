﻿// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroup.MylistGroupDetailResponseContainer
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist.MylistGroup
{
  [DataContract]
  public class MylistGroupDetailResponseContainer
  {
    [DataMember(Name = "nicovideo_mylistgroup_response")]
    public MylistGroupDetailResponse nicovideo_mylistgroup_response { get; set; }
  }
}
