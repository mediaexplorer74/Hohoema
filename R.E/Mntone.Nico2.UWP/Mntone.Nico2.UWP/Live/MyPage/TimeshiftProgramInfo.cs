// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.MyPage.TimeshiftProgramInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

#nullable disable
namespace Mntone.Nico2.Live.MyPage
{
  public sealed class TimeshiftProgramInfo
  {
    internal TimeshiftProgramInfo(string id, string title)
    {
      this.ID = id;
      this.Title = title;
    }

    public string ID { get; internal set; }

    public string Title { get; private set; }
  }
}
