﻿using LiteDB;
using Hohoema.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Models.Domain.Niconico.UserFeature
{
    public class VideoPlayedHistoryRepository : LiteDBServiceBase<VideoPlayHistoryEntry>
    {
        public VideoPlayedHistoryRepository(ILiteDatabase liteDatabase) : base(liteDatabase)
        {
        }

        public VideoPlayHistoryEntry VideoPlayed(string videoId)
        {
            var history = _collection.FindById(videoId);
            if (history != null)
            {
                history.PlayCount++;
            }
            else
            {
                history = new VideoPlayHistoryEntry
                {
                    VideoId = videoId,
                    PlayCount = 1,
                    LastPlayed = DateTime.Now
                };
            }

            _collection.Upsert(history);

            return history;
        }

        public VideoPlayHistoryEntry Get(string videoId)
        {
            return _collection.FindById(videoId);
        }


        public bool IsVideoPlayed(string videoId)
        {
            return _collection.FindById(videoId)?.PlayCount > 0;
        }

        public int ClearAllHistories()
        {
            return _collection.DeleteAll();
        }
    }

    public class VideoPlayHistoryEntry
    {
        [BsonId]
        public string VideoId { get; set; }

        public uint PlayCount { get; set; } = 0;

        public DateTime LastPlayed { get; set; }
    }
}