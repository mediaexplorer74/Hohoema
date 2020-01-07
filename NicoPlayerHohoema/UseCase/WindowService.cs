﻿using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace NicoPlayerHohoema.Services
{
    public sealed class WindowService : BindableBase, IDisposable
    {
        private readonly PlayerViewManager _playerViewManager;
        private readonly IScheduler _scheduler;
        private readonly ApplicationView _applicationView;
        CompositeDisposable _disposables = new CompositeDisposable();

        public WindowService(PlayerViewManager playerViewManager, IScheduler scheduler)
        {
            _playerViewManager = playerViewManager;
            _scheduler = scheduler;

            _applicationView = ApplicationView.GetForCurrentView();

            IsFullScreen = new ReactiveProperty<bool>(_scheduler, _applicationView.IsFullScreenMode, ReactivePropertyMode.DistinctUntilChanged);
            IsFullScreen
                .Subscribe(isFullScreen =>
                {

                    IsCompactOverlay.Value = false;

                    if (isFullScreen)
                    {
                        if (!_applicationView.TryEnterFullScreenMode())
                        {
                            IsFullScreen.Value = false;
                        }
                    }
                    else
                    {
                        _applicationView.ExitFullScreenMode();
                    }
                })
            .AddTo(_disposables);


            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4))
            {
                IsCompactOverlay = new ReactiveProperty<bool>(_scheduler,
                    _applicationView.ViewMode == ApplicationViewMode.CompactOverlay
                    );

                // This device supports all APIs in UniversalApiContract version 2.0
                IsCompactOverlay
                .Subscribe(async isCompactOverlay =>
                {
                    if (_applicationView.IsViewModeSupported(ApplicationViewMode.CompactOverlay))
                    {
                        if (isCompactOverlay)
                        {
                            ViewModePreferences compactOptions = ViewModePreferences.CreateDefault(ApplicationViewMode.CompactOverlay);
                            compactOptions.CustomSize = new Windows.Foundation.Size(500, 280);

                            var result = await _applicationView.TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay, compactOptions);
                            if (result)
                            {
                                _applicationView.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                                _applicationView.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                            }
                        }
                        else
                        {
                            var result = await _applicationView.TryEnterViewModeAsync(ApplicationViewMode.Default);
                        }
                    }
                })
                .AddTo(_disposables);
            }
            else
            {
                IsCompactOverlay = new ReactiveProperty<bool>(_scheduler, false);
            }



            IsSmallWindowModeEnable = _playerViewManager
                .ObserveProperty(x => x.IsPlayerSmallWindowModeEnabled)
                .ToReadOnlyReactiveProperty(eventScheduler: _scheduler)
                .AddTo(_disposables);


        }


        // Settings
        public ReactiveProperty<bool> IsFullScreen { get; private set; }
        public ReactiveProperty<bool> IsCompactOverlay { get; private set; }
        public ReadOnlyReactiveProperty<bool> IsSmallWindowModeEnable { get; private set; }




        // TODO
        // CompactOverlay
        // FullScreen
        private DelegateCommand _ToggleFullScreenCommand;
        public DelegateCommand ToggleFullScreenCommand
        {
            get
            {
                return _ToggleFullScreenCommand
                    ?? (_ToggleFullScreenCommand = new DelegateCommand(() =>
                    {
                        IsFullScreen.Value = !IsFullScreen.Value;
                    }
                    ));
            }
        }


        private DelegateCommand _ToggleCompactOverlayCommand;
        public DelegateCommand ToggleCompactOverlayCommand
        {
            get
            {
                return _ToggleCompactOverlayCommand
                    ?? (_ToggleCompactOverlayCommand = new DelegateCommand(() =>
                    {
                        IsCompactOverlay.Value = !IsCompactOverlay.Value;
                    }
                    , () => ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4)
                    ));
            }
        }

        private DelegateCommand _PlayerSmallWindowDisplayCommand;
        public DelegateCommand PlayerSmallWindowDisplayCommand
        {
            get
            {
                return _PlayerSmallWindowDisplayCommand
                    ?? (_PlayerSmallWindowDisplayCommand = new DelegateCommand(() =>
                    {
                        _playerViewManager.IsPlayerSmallWindowModeEnabled = true;
                    }
                    ));
            }
        }

        private DelegateCommand _PlayerDisplayWithMainViewCommand;
        public DelegateCommand PlayerDisplayWithMainViewCommand
        {
            get
            {
                return _PlayerDisplayWithMainViewCommand
                    ?? (_PlayerDisplayWithMainViewCommand = new DelegateCommand(() =>
                    {
                        _ = _playerViewManager.ChangePlayerViewModeAsync(PlayerViewMode.PrimaryView);
                    }
                    ));
            }
        }

        private DelegateCommand _PlayerDisplayWithSecondaryViewCommand;
        public DelegateCommand PlayerDisplayWithSecondaryViewCommand
        {
            get
            {
                return _PlayerDisplayWithSecondaryViewCommand
                    ?? (_PlayerDisplayWithSecondaryViewCommand = new DelegateCommand(() =>
                    {
                        _ = _playerViewManager.ChangePlayerViewModeAsync(PlayerViewMode.SecondaryView);
                    }
                    ));
            }
        }


        private DelegateCommand _ClosePlayerCommand;
        public DelegateCommand ClosePlayerCommand => _ClosePlayerCommand
            ?? (_ClosePlayerCommand = new DelegateCommand(() =>
            {
                _playerViewManager.ClosePlayer();
            }));



        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
