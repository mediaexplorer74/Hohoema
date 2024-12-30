// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.SessionApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class SessionApi
  {
    [DataMember(Name = "recipe_id")]
    public string RecipeId { get; set; }

    [DataMember(Name = "player_id")]
    public string PlayerId { get; set; }

    [DataMember(Name = "videos")]
    public IList<string> Videos { get; set; }

    [DataMember(Name = "audios")]
    public IList<string> Audios { get; set; }

    [DataMember(Name = "movies")]
    public IList<object> Movies { get; set; }

    [DataMember(Name = "protocols")]
    public IList<string> Protocols { get; set; }

    [DataMember(Name = "auth_types")]
    public AuthTypes AuthTypes { get; set; }

    [DataMember(Name = "service_user_id")]
    public string ServiceUserId { get; set; }

    [DataMember(Name = "token")]
    public string Token { get; set; }

    [DataMember(Name = "signature")]
    public string Signature { get; set; }

    [DataMember(Name = "content_id")]
    public string ContentId { get; set; }

    [DataMember(Name = "heartbeat_lifetime")]
    public int HeartbeatLifetime { get; set; }

    [DataMember(Name = "content_key_timeout")]
    public int ContentKeyTimeout { get; set; }

    [DataMember(Name = "priority")]
    public double Priority { get; set; }

    [DataMember(Name = "urls")]
    public IList<UrlInfo> Urls { get; set; }
  }
}
