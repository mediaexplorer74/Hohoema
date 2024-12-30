// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.WatchApiResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Videos.Flv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class WatchApiResponse : FlvResponse
  {
    [DataMember(Name = "flashvars")]
    public Flashvars flashvars { get; set; }

    [DataMember(Name = "videoDetail")]
    public VideoDetail videoDetail { get; set; }

    [DataMember(Name = "channelInfo")]
    public ChannelInfo channelInfo { get; set; }

    [DataMember(Name = "uploaderInfo")]
    public UploaderInfo UploaderInfo { get; set; }

    [DataMember(Name = "viewerInfo")]
    public ViewerInfo viewerInfo { get; set; }

    [DataMember(Name = "tagRelatedMarquee")]
    public object tagRelatedMarquee { get; set; }

    [DataMember(Name = "googleAdNgReasons")]
    public List<string> googleAdNgReasons { get; set; }

    [DataMember(Name = "playlistToken")]
    public string playlistToken { get; set; }

    [DataMember(Name = "tagRelatedBanner")]
    public object tagRelatedBanner { get; set; }

    public Dictionary<string, string> GetFlvInfo()
    {
      return ((IEnumerable<string>) WebUtility.UrlDecode(this.flashvars.flvInfo).Split('&')).ToDictionary<string, string, string>((Func<string, string>) (source => source.Substring(0, source.IndexOf('='))), (Func<string, string>) (source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1))));
    }

    [OnDeserialized]
    private void SetValuesOnDeserialized(StreamingContext context)
    {
      this.SetupFlvData(this.GetFlvInfo());
    }
  }
}
