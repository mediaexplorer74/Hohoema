// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.User.UserDetail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

#nullable disable
namespace Mntone.Nico2.Users.User
{
  public class UserDetail
  {
    public string UserId { get; set; }

    public string Nickname { get; set; }

    public string ThumbnailUri { get; set; }

    public bool IsPremium { get; set; }

    public Sex? Gender { get; set; }

    public string Region { get; set; }

    public string BirthDay { get; set; }

    public uint FollowerCount { get; set; }

    public uint StampCount { get; set; }

    public bool IsOwnerVideoPrivate { get; set; }

    public string Description { get; set; }

    public uint TotalVideoCount { get; set; }
  }
}
