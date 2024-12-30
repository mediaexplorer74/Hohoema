// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Flv.FlvResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Mntone.Nico2.Videos.Flv
{
  public class FlvResponse
  {
    internal FlvResponse()
    {
    }

    internal FlvResponse(Dictionary<string, string> wwwFormData) => this.SetupFlvData(wwwFormData);

    internal void SetupFlvData(Dictionary<string, string> wwwFormData)
    {
      string str1 = this.SafeGetValue(wwwFormData, "thread_id");
      this.ThreadId = str1 != null ? str1.ToUInt() : uint.MaxValue;
      string str2 = this.SafeGetValue(wwwFormData, "l");
      this.Length = str2 != null ? str2.ToTimeSpanFromSecondsString() : TimeSpan.FromSeconds(0.0);
      string str3 = this.SafeGetValue(wwwFormData, "url");
      Uri uri1 = str3 != null ? str3.ToUri() : (Uri) null;
      if ((object) uri1 == null)
        uri1 = (Uri) null;
      this.VideoUrl = uri1;
      string str4 = this.SafeGetValue(wwwFormData, "link");
      Uri uri2 = str4 != null ? str4.ToUri() : (Uri) null;
      if ((object) uri2 == null)
        uri2 = (Uri) null;
      this.ReportUrl = uri2;
      string str5 = this.SafeGetValue(wwwFormData, "ms");
      Uri uri3 = str5 != null ? str5.ToUri() : (Uri) null;
      if ((object) uri3 == null)
        uri3 = (Uri) null;
      this.CommentServerUrl = uri3;
      string str6 = this.SafeGetValue(wwwFormData, "ms_sub");
      Uri uri4 = str6 != null ? str6.ToUri() : (Uri) null;
      if ((object) uri4 == null)
        uri4 = (Uri) null;
      this.SubCommentServerUrl = uri4;
      string str7 = this.SafeGetValue(wwwFormData, "deleted");
      this.PrivateReason = str7 != null ? (PrivateReasonType) str7.ToUShort() : PrivateReasonType.None;
      string str8 = this.SafeGetValue(wwwFormData, "user_id");
      this.UserId = str8 != null ? str8.ToUInt() : uint.MaxValue;
      string str9 = this.SafeGetValue(wwwFormData, "is_premium");
      this.IsPremium = str9 != null && str9.ToBooleanFrom1();
      this.UserName = this.SafeGetValue(wwwFormData, "nickname");
      this.LoadedAt = DateTimeOffset.FromFileTime(10000L * long.Parse(this.SafeGetValue(wwwFormData, "time")) + 116444736000000000L);
      string str10 = this.SafeGetValue(wwwFormData, "needs_key");
      this.IsKeyRequired = str10 != null && str10.ToBooleanFrom1();
      string str11 = this.SafeGetValue(wwwFormData, "optional_thread_id");
      this.OptionalThreadId = str11 != null ? str11.ToUInt() : uint.MaxValue;
      this.ChannelFilter = this.SafeGetValue(wwwFormData, "ng_ch") ?? string.Empty;
      this.FlashMediaServerToken = this.SafeGetValue(wwwFormData, "fmst") ?? string.Empty;
      this.AppsHost = this.SafeGetValue(wwwFormData, "hms");
      this.AppsPort = this.SafeGetValue(wwwFormData, "hmsp").ToUShort();
      this.AppsThreadId = (uint) this.SafeGetValue(wwwFormData, "hmst").ToUShort();
      this.AppsTicket = this.SafeGetValue(wwwFormData, "hmstk");
    }

    private string SafeGetValue(Dictionary<string, string> dict, string key)
    {
      return dict.ContainsKey(key) ? dict[key] : (string) null;
    }

    public uint ThreadId { get; private set; }

    public TimeSpan Length { get; private set; }

    public Uri VideoUrl { get; private set; }

    public Uri ReportUrl { get; private set; }

    public Uri CommentServerUrl { get; private set; }

    public Uri SubCommentServerUrl { get; private set; }

    public PrivateReasonType PrivateReason { get; private set; }

    public bool IsDeleted
    {
      get
      {
        return this.PrivateReason != PrivateReasonType.None && this.PrivateReason != PrivateReasonType.Private;
      }
    }

    public uint UserId { get; private set; }

    public bool IsPremium { get; private set; }

    public string UserName { get; private set; }

    public DateTimeOffset LoadedAt { get; private set; }

    public bool IsKeyRequired { get; private set; }

    public uint OptionalThreadId { get; private set; }

    public string ChannelFilter { get; private set; }

    public string FlashMediaServerToken { get; private set; }

    public string AppsHost { get; private set; }

    public ushort AppsPort { get; private set; }

    public uint AppsThreadId { get; set; }

    public string AppsTicket { get; private set; }
  }
}
