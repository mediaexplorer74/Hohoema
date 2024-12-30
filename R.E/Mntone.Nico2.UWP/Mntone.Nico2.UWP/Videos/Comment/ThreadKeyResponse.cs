// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.ThreadKeyResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  internal class ThreadKeyResponse
  {
    public ThreadKeyResponse(string threadKey, string force184 = null)
    {
      this.ThreadKey = threadKey;
      this.Force184 = force184;
    }

    public string ThreadKey { get; set; }

    public string Force184 { get; set; }
  }
}
