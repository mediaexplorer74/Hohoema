﻿using Mntone.Nico2.Users.Series;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.Niconico.Video.Series;

namespace Hohoema.Presentation.ViewModels.Niconico.Series
{
    public class UserSeriesItemViewModel : ISeries
    {
        private readonly UserSeries _userSeries;

        public UserSeriesItemViewModel(UserSeries userSeries)
        {
            _userSeries = userSeries;
        }

        public string Id => _userSeries.Id.ToString();

        public string Title => _userSeries.Title;

        public bool IsListed => _userSeries.IsListed;

        public string Description => _userSeries.Description;

        public string ThumbnailUrl => _userSeries.ThumbnailUrl.OriginalString;

        public int ItemsCount => (int)_userSeries.ItemsCount;

        public string ProviderType => _userSeries.Owner.Type;

        public string ProviderId => _userSeries.Owner.Id;        
    }

    
}