// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.UserTwitter
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class UserTwitter
  {
    internal UserTwitter(XElement twitterInfoXml)
    {
      this.IsEnabled = twitterInfoXml.GetNamedChildNodeText("status") == "enabled";
      this.ScreenName = twitterInfoXml.GetNamedChildNodeText("screen_name");
      this.FollowersCount = twitterInfoXml.GetNamedChildNodeText("followers_count").ToUInt();
      this.IsVip = twitterInfoXml.GetNamedChildNodeText("is_vip").ToBooleanFrom1();
      this.ProfileImageUrl = twitterInfoXml.GetNamedChildNodeText("profile_image_url").ToUri();
      this.IsAuthenticationRequired = twitterInfoXml.GetNamedChildNodeText("after_auth").ToBooleanFrom1();
      this.Token = twitterInfoXml.GetNamedChildNodeText("tweet_token");
    }

    public bool IsEnabled { get; private set; }

    public string ScreenName { get; private set; }

    public uint FollowersCount { get; private set; }

    public bool IsVip { get; private set; }

    public Uri ProfileImageUrl { get; private set; }

    public bool IsAuthenticationRequired { get; private set; }

    public string Token { get; private set; }
  }
}
