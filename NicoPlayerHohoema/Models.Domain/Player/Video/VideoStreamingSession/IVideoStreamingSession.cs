﻿using System;
using System.Threading.Tasks;

namespace Hohoema.Models.Domain.Player.Video
{

    public interface IVideoStreamingSession : IStreamingSession
    {
        string QualityId { get; }
        NicoVideoQuality Quality { get; }
    }

    public interface IVideoStreamingDownloadSession : IVideoStreamingSession
    {
        Task<Uri> GetDownloadUrlAndSetupDonwloadSession();
    }

}