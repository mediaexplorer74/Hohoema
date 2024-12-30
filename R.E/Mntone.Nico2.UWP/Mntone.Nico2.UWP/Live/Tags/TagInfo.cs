// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Tags.TagInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

#nullable disable
namespace Mntone.Nico2.Live.Tags
{
  public sealed class TagInfo
  {
    internal TagInfo(bool isCategoryTag, string value, ushort count)
    {
      this.IsCategoryTag = isCategoryTag;
      this.Value = value;
      this.Count = count;
    }

    public bool IsCategoryTag { get; internal set; }

    public string Value { get; private set; }

    public ushort Count { get; private set; }
  }
}
