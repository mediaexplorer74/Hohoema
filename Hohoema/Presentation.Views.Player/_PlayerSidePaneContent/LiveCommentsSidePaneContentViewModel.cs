﻿using Mntone.Nico2.Videos.Comment;
using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Niconico.Video;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using Hohoema.Models.Domain.Player;
using Hohoema.Models.UseCase.NicoVideos.Player;
using Hohoema.Presentation.ViewModels.Niconico.Live;
using Hohoema.Presentation.ViewModels.Niconico.Share;

namespace Hohoema.Presentation.ViewModels.Player.PlayerSidePaneContent
{
	public class LiveCommentsSidePaneContentViewModel : SidePaneContentViewModelBase
	{
		public LiveCommentsSidePaneContentViewModel(
            CommentFilteringFacade commentFiltering, 
            Microsoft.Toolkit.Uwp.UI.AdvancedCollectionView comments,
            IScheduler scheduler,
            NicoVideoOwnerCacheRepository nicoVideoOwnerRepository,
            OpenLinkCommand openLinkCommand,
            CopyToClipboardCommand copyToClipboardCommand
            )
		{
            _playerSettings = commentFiltering;
            Comments = comments;
            _scheduler = scheduler;
            _nicoVideoOwnerRepository = nicoVideoOwnerRepository;
            OpenLinkCommand = openLinkCommand;
            CopyToClipboardCommand = copyToClipboardCommand;
            NicoLiveUserIdAddToNGCommand = new NicoLiveUserIdAddToNGCommand(_playerSettings, _nicoVideoOwnerRepository);
            NicoLiveUserIdRemoveFromNGCommand = new NicoLiveUserIdRemoveFromNGCommand(_playerSettings);
            IsCommentListScrollWithVideo = new ReactiveProperty<bool>(_scheduler, false)
				.AddTo(_CompositeDisposable);

            NGUsers = new ObservableCollection<CommentFliteringRepository.FilteringCommentOwnerId>(_playerSettings.GetFilteringCommentOwnerIdList());
            IsNGCommentUserIdEnabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.IsEnableFilteringCommentOwnerId, _scheduler)
                .AddTo(_CompositeDisposable);
        }


		public void UpdatePlayPosition(uint videoPosition)
		{
			if (IsCommentListScrollWithVideo.Value)
			{
				// 表示位置の更新
			}
		}

		public ReactiveProperty<bool> IsCommentListScrollWithVideo { get; private set; }

        public ReactiveProperty<bool> IsNGCommentUserIdEnabled { get; private set; }


        public ObservableCollection<CommentFliteringRepository.FilteringCommentOwnerId> NGUsers { get; }

        /*
        ReadOnlyObservableCollection<Comment> _Comments;

        public ReadOnlyObservableCollection<Comment> Comments
        {
            get { return _Comments; }
            set { SetProperty(ref _Comments, value); }
        }
        */


        Microsoft.Toolkit.Uwp.UI.AdvancedCollectionView _Comments;
        private readonly CommentFilteringFacade _playerSettings;
        private readonly IScheduler _scheduler;
        private readonly NicoVideoOwnerCacheRepository _nicoVideoOwnerRepository;
        
        public Microsoft.Toolkit.Uwp.UI.AdvancedCollectionView Comments
        {
            get { return _Comments; }
            set { SetProperty(ref _Comments, value); }
        }

        public OpenLinkCommand OpenLinkCommand { get; }
        public CopyToClipboardCommand CopyToClipboardCommand { get; }
        public NicoLiveUserIdAddToNGCommand NicoLiveUserIdAddToNGCommand { get; }
        public NicoLiveUserIdRemoveFromNGCommand NicoLiveUserIdRemoveFromNGCommand { get; }
    }
}