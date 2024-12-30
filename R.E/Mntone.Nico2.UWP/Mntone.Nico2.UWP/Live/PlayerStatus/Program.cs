// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Program
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Program
  {
    internal Program(
      XElement streamXml,
      XElement playerXml,
      XElement nsenXml,
      ProgramTwitter programTwitter)
    {
      this.Id = streamXml.GetNamedChildNodeText("id");
      this.Title = streamXml.GetNamedChildNodeText("title");
      this.Description = streamXml.GetNamedChildNodeText("description");
      this.WatchCount = streamXml.GetNamedChildNodeText("watch_count").ToUInt();
      this.CommentCount = streamXml.GetNamedChildNodeText("comment_count").ToUInt();
      this.CommunityType = streamXml.GetNamedChildNodeText("provider_type").ToCommunityType();
      this.CommunityId = streamXml.GetNamedChildNodeText("default_community");
      this.BroadcasterId = streamXml.GetNamedChildNodeText("owner_id").ToUInt();
      this.BroadcasterName = streamXml.GetNamedChildNodeText("owner_name");
      this.International = streamXml.GetNamedChildNodeText("international").ToUShort();
      this.BaseAt = streamXml.GetNamedChildNodeText("base_time").ToDateTimeOffsetFromUnixTime();
      this.OpenedAt = streamXml.GetNamedChildNodeText("open_time").ToDateTimeOffsetFromUnixTime();
      this.StartedAt = streamXml.GetNamedChildNodeText("start_time").ToDateTimeOffsetFromUnixTime();
      this.EndedAt = streamXml.GetNamedChildNodeText("end_time").ToDateTimeOffsetFromUnixTime();
      string namedChildNodeText1 = streamXml.GetNamedChildNodeText("timeshift_time");
      if (!string.IsNullOrEmpty(namedChildNodeText1))
        this.TimeshiftAt = namedChildNodeText1.ToDateTimeOffsetFromUnixTime();
      this.CrowdedUrl = streamXml.GetNamedChildNodeText("full_video").ToUri();
      this.KickOutUrl = streamXml.GetNamedChildNodeText("kickout_video").ToUri();
      this.KickOutImageUrl = playerXml.GetNamedChildNode("dialog_image").GetNamedChildNodeText("oidashi").ToUri();
      Uri uri = streamXml.GetNamedChildNodeText("picture_url").ToUri();
      if (uri != (Uri) null)
      {
        this.CommunityImageUrl = uri;
        this.CommunitySmallImageUrl = streamXml.GetNamedChildNodeText("thumb_url").ToUri();
      }
      this.TicketUrl = streamXml.GetNamedChildNodeText("product_ticket_url").ToUri();
      this.BannerUrl = streamXml.GetNamedChildNodeText("product_banner_url").ToUri();
      this.ShutterUrl = streamXml.GetNamedChildNodeText("shutter_url").ToUri();
      this.IsRerun = streamXml.GetNamedChildNodeText("is_rerun_stream").ToBooleanFrom1();
      this.IsArchive = streamXml.GetNamedChildNodeText("archive").ToBooleanFrom1();
      if (streamXml.GetNamedChildNodeText("is_dj_stream").ToBooleanFrom1())
      {
        this.ExtendedType = ProgramExtendedType.NewComer;
        this.NsenType = string.Empty;
        this.NsenCommand = string.Empty;
      }
      else if (streamXml.GetNamedChildNodeText("is_cruise_stream").ToBooleanFrom1())
      {
        this.ExtendedType = ProgramExtendedType.Cruise;
        this.NsenType = string.Empty;
        this.NsenCommand = string.Empty;
      }
      else if (nsenXml != null && nsenXml.GetNamedChildNodeText("is_ns_stream").ToBooleanFrom1())
      {
        this.ExtendedType = ProgramExtendedType.Nsen;
        this.NsenType = nsenXml.GetNamedChildNodeText("nstype");
        this.NsenCommand = nsenXml.GetNamedChildNodeText("nspanel");
      }
      else
      {
        this.ExtendedType = ProgramExtendedType.None;
        this.NsenType = string.Empty;
        this.NsenCommand = string.Empty;
      }
      this.IsHighQuality = streamXml.GetNamedChildNodeText("hqstream").ToBooleanFrom1();
      this.IsInfinity = streamXml.GetNamedChildNodeText("infinity_mode").ToBooleanFrom1();
      this.IsReserved = streamXml.GetNamedChildNodeText("is_reserved").ToBooleanFrom1();
      this.IsArchivePlayServer = streamXml.GetNamedChildNodeText("is_archiveplayserver").ToBooleanFrom1();
      this.IsTimeshiftEnabled = streamXml.GetNamedChildNodeText("is_nonarchive_timeshift_enabled").ToBooleanFrom1();
      string namedChildNodeText2 = streamXml.GetNamedChildNodeText("is_product_stream");
      if (namedChildNodeText2 != null)
      {
        this.IsProductEnabled = namedChildNodeText2.ToBooleanFrom1();
        this.IsTrialEnabled = streamXml.GetNamedChildNodeText("is_trial").ToBooleanFrom1();
        this.IsBannerForced = streamXml.GetNamedChildNodeText("product_fixed_banner").ToBooleanFrom1();
      }
      this.IsNoticeBalloonEnabled = playerXml.GetNamedChildNodeText("is_notice_viewer_balloon_enabled").ToBooleanFrom1();
      this.IsErrorReportEnabled = playerXml.GetNamedChildNodeText("error_report").ToBooleanFrom1();
      this.Twitter = programTwitter;
    }

    public string Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public uint WatchCount { get; private set; }

    public uint CommentCount { get; private set; }

    public bool IsOfficial => this.CommunityType == CommunityType.Official;

    public bool IsChannel => this.CommunityType == CommunityType.Channel;

    public bool IsCommunity => this.CommunityType == CommunityType.Community;

    public CommunityType CommunityType { get; private set; }

    public string CommunityId { get; private set; }

    public uint BroadcasterId { get; private set; }

    public string BroadcasterName { get; private set; }

    public ushort International { get; private set; }

    public DateTimeOffset BaseAt { get; private set; }

    public DateTimeOffset OpenedAt { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public DateTimeOffset EndedAt { get; private set; }

    public DateTimeOffset TimeshiftAt { get; private set; }

    public Uri CrowdedUrl { get; private set; }

    public Uri KickOutUrl { get; private set; }

    public Uri KickOutImageUrl { get; private set; }

    public Uri CommunityImageUrl { get; private set; }

    public Uri CommunitySmallImageUrl { get; private set; }

    public Uri TicketUrl { get; private set; }

    public Uri BannerUrl { get; private set; }

    public Uri ShutterUrl { get; private set; }

    public bool IsRerun { get; private set; }

    public bool IsArchive { get; private set; }

    public bool IsLive => !this.IsArchive;

    public bool IsNewComer => this.ExtendedType == ProgramExtendedType.NewComer;

    public bool IsCruise => this.ExtendedType == ProgramExtendedType.Cruise;

    public bool IsNsen => this.ExtendedType == ProgramExtendedType.Nsen;

    public ProgramExtendedType ExtendedType { get; private set; }

    public bool IsHighQuality { get; private set; }

    public bool IsInfinity { get; private set; }

    public bool IsReserved { get; private set; }

    public bool IsArchivePlayServer { get; private set; }

    public bool IsTimeshiftEnabled { get; private set; }

    public bool IsProductEnabled { get; private set; }

    public bool IsTrialEnabled { get; private set; }

    public bool IsBannerForced { get; private set; }

    public bool IsNoticeBalloonEnabled { get; private set; }

    public bool IsErrorReportEnabled { get; private set; }

    public string NsenType { get; private set; }

    public string NsenCommand { get; private set; }

    public ProgramTwitter Twitter { get; private set; }
  }
}
