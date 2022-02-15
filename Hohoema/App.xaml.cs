﻿using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Application;
using Hohoema.Models.Domain.Niconico;
using Hohoema.Models.Domain.Niconico.Follow.LoginUser;
using Hohoema.Models.Domain.Niconico.NicoRepo;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Domain.Pins;
using Hohoema.Models.Domain.Player;
using Hohoema.Models.Domain.Player.Video;
using Hohoema.Models.Domain.Player.Video.Cache;
using Hohoema.Models.Domain.Subscriptions;
using Hohoema.Models.Helpers;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.Migration;
using Hohoema.Models.UseCase.Niconico.Player;
using Hohoema.Models.UseCase.Subscriptions;
using Hohoema.Models.UseCase.VideoCache;
using Hohoema.Presentation.Services;
using Hohoema.Models.UseCase.PageNavigation;
using Hohoema.Presentation.ViewModels;
using LiteDB;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.Helpers;
using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.DryIoc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Hohoema.Presentation.Views.Pages;
using Hohoema.Models.UseCase.Niconico.Account;
using Hohoema.Models.UseCase.Niconico.Follow;
using Hohoema.Models.Domain.Notification;
using Hohoema.Models.Domain.VideoCache;
using Windows.Storage.AccessCache;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Extensions.Logging;
using Hohoema.Models.Infrastructure;
using Hohoema.Models.UseCase.Playlist;
using Hohoema.Models.UseCase.Niconico.Player.Comment;
using Hohoema.Models.UseCase.Niconico.Video;
using Hohoema.Presentation.ViewModels.Niconico.Video;
using Hohoema.Models.Domain.Player.Comment;
using Hohoema.Models.Domain.Playlist;
using Hohoema.Models.UseCase.Hohoema.LocalMylist;
using DryIoc;
using Prism.Logging;
using ZLogger;
using Cysharp.Text;
using Windows.UI.Core;

namespace Hohoema
{

    /// <summary>
    /// 既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    sealed partial class App : PrismApplication
    {
        const bool _DEBUG_XBOX_RESOURCE = false;

        public SplashScreen SplashScreen { get; private set; }

        private bool _IsPreLaunch;

		public const string ACTIVATION_WITH_ERROR = "error";
        public const string ACTIVATION_WITH_ERROR_OPEN_LOG = "error_open_log";
        public const string ACTIVATION_WITH_ERROR_COPY_LOG = "error_copy_log";

        internal const string IS_COMPLETE_INTRODUCTION = "is_first_launch";

        
        /// <summary>
        /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        ///最初の行であるため、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            UnhandledException += PrismUnityApplication_UnhandledException;

            // XboxOne向けの設定
            // 基本カーソル移動で必要なときだけポインターを出現させる
            this.RequiresPointerMode = Windows.UI.Xaml.ApplicationRequiresPointerMode.WhenRequested;

            // テーマ設定
            // ThemeResourceの切り替えはアプリの再起動が必要
            RequestedTheme = GetTheme();
            
            Microsoft.Toolkit.Uwp.UI.ImageCache.Instance.CacheDuration = TimeSpan.FromDays(7);
            Microsoft.Toolkit.Uwp.UI.ImageCache.Instance.MaxMemoryCacheCount = 0;
            Microsoft.Toolkit.Uwp.UI.ImageCache.Instance.RetryCount = 3;
                       
            this.InitializeComponent();
        }        

        protected override Rules CreateContainerRules()
        {
            return base.CreateContainerRules().WithoutThrowOnRegisteringDisposableTransient();
        }

        public override async Task OnStartAsync(StartArgs args)
        {
            using var initializeLock = await InitializeLock.LockAsync();

            if (args.Arguments is LaunchActivatedEventArgs launchArgs)
            {
                SplashScreen = launchArgs.SplashScreen;
#if DEBUG
                DebugSettings.IsBindingTracingEnabled = true;
#endif
                _IsPreLaunch = launchArgs.PrelaunchActivated;

                Microsoft.Toolkit.Uwp.Helpers.SystemInformation.Instance.TrackAppUse(launchArgs);
            }

            if (args.StartKind == StartKinds.Launch)
            {
                
            }
            else if (args.StartKind == StartKinds.Activate)
            {
                await OnActivateApplicationAsync(args.Arguments as IActivatedEventArgs);
            }
            else if (args.StartKind == StartKinds.Background)
            {
                await BackgroundActivated(args.Arguments as BackgroundActivatedEventArgs);
            }

            await base.OnStartAsync(args);
        }

        UIElement CreateShell()
        {
            
            // Grid
            //   |- HohoemaInAppNotification
            //   |- PlayerWithPageContainerViewModel
            //   |    |- MenuNavigatePageBaseViewModel
            //   |         |- rootFrame 

            _primaryWindowCoreLayout = Container.Resolve<PrimaryWindowCoreLayout>();
            var hohoemaInAppNotification = new Presentation.Views.Controls.HohoemaInAppNotification()
            {
                VerticalAlignment = VerticalAlignment.Bottom
            };

            var grid = new Grid()
            {
                Children =
                {
                    _primaryWindowCoreLayout,
                    hohoemaInAppNotification,
                    new Presentation.Views.NoUIProcessScreen()
                }
            };

#pragma warning disable IDISP001 // Dispose created.
            var unityContainer = Container.GetContainer();
#pragma warning restore IDISP001 // Dispose created.
            var primaryWindowContentNavigationService = _primaryWindowCoreLayout.CreateNavigationService();
            unityContainer.UseInstance(primaryWindowContentNavigationService);

            var primaryViewPlayerNavigationService = _primaryWindowCoreLayout.CreatePlayerNavigationService();
            var name = "PrimaryPlayerNavigationService";
            unityContainer.UseInstance(primaryViewPlayerNavigationService, serviceKey: name);


#if DEBUG
            _primaryWindowCoreLayout.FocusEngaged += (__, args) => Debug.WriteLine("focus engagad: " + args.OriginalSource.ToString());
#endif

            _primaryWindowCoreLayout.IsDebugModeEnabled = IsDebugModeEnabled;

            return grid;
        }

        public override void RegisterTypes(IContainerRegistry container)
        {
            var unityContainer = container.GetContainer();

//            unityContainer.Register<PrimaryViewPlayerManager>(made: Made.Of().Parameters.Name("navigationServiceLazy", x => new Lazy<INavigationService>(() => unityContainer.Resolve<INavigationService>(serviceKey: "PrimaryPlayerNavigationService"))));

            unityContainer.UseInstance<LocalObjectStorageHelper>(new LocalObjectStorageHelper(new SystemTextJsonSerializer()));

            unityContainer.UseInstance<IMessenger>(WeakReferenceMessenger.Default);

            LiteDatabase db = new LiteDatabase($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "hohoema.db")};");
            unityContainer.UseInstance<LiteDatabase>(db);


            unityContainer.RegisterDelegate<IPlayerView>(c => 
            {
                var appearanceSettings = c.Resolve<AppearanceSettings>();
                if (appearanceSettings.PlayerDisplayView == PlayerDisplayView.PrimaryView)
                {
                    return c.Resolve<PrimaryViewPlayerManager>();
                }
                else
                {
                    return c.Resolve<AppWindowSecondaryViewPlayerManager>();
                }
            });

            // MediaPlayerを各ウィンドウごとに一つずつ作るように
            container.RegisterSingleton<MediaPlayer>();

            // 再生プレイリスト管理のクラスは各ウィンドウごとに一つずつ作成
            container.RegisterSingleton<HohoemaPlaylistPlayer>();

            // Service
            container.RegisterSingleton<PageManager>();
            container.RegisterSingleton<PrimaryViewPlayerManager>();
            container.RegisterSingleton<SecondaryViewPlayerManager>();
            container.RegisterSingleton<AppWindowSecondaryViewPlayerManager>();
            container.RegisterSingleton<NiconicoLoginService>();
            container.RegisterSingleton<DialogService>();
            container.RegisterSingleton<INotificationService, NotificationService>();
            container.RegisterSingleton<NoUIProcessScreenContext>();
            container.RegisterSingleton<CurrentActiveWindowUIContextService>();

            // Models
            container.RegisterSingleton<AppearanceSettings>();
            container.RegisterSingleton<PinSettings>();
            container.RegisterSingleton<PlayerSettings>();
            container.RegisterSingleton<VideoFilteringSettings>();
            container.RegisterSingleton<VideoRankingSettings>();
            container.RegisterSingleton<NicoRepoSettings>();
            container.RegisterSingleton<CommentFliteringRepository>();
            container.RegisterSingleton<QueuePlaylist>();

            container.RegisterSingleton<NicoVideoProvider>();


            container.RegisterSingleton<NiconicoSession>();
            container.RegisterSingleton<NicoVideoSessionOwnershipManager>();

            container.RegisterSingleton<LoginUserOwnedMylistManager>();

            container.RegisterSingleton<SubscriptionManager>();

            container.RegisterSingleton<Models.Domain.VideoCache.VideoCacheManager>();
            container.RegisterSingleton<Models.Domain.VideoCache.VideoCacheSettings>();

            // UseCase
            unityContainer.Register<CommentPlayer>();
            container.RegisterSingleton<CommentFilteringFacade>();
            container.RegisterSingleton<MediaPlayerSoundVolumeManager>();
            container.RegisterSingleton<LocalMylistManager>();
            container.RegisterSingleton<VideoItemsSelectionContext>();
            container.RegisterSingleton<WatchHistoryManager>();
            container.RegisterSingleton<ApplicationLayoutManager>();

            container.RegisterSingleton<VideoCacheFolderManager>();

            container.RegisterSingleton<IPlaylistFactoryResolver, PlaylistItemsSourceResolver>();




            // ViewModels
            container.RegisterSingleton<Presentation.ViewModels.Pages.Niconico.VideoRanking.RankingCategoryListPageViewModel>();

            // Frameのキャッシュ無効＋IncrementalLoadingリスト系ページのViewModelがシングルトン、という構成の場合に
            // ListViewの読み込み順序が壊れる問題が発生するためVMは都度生成にしている
            // see@ https://github.com/tor4kichi/Hohoema/issues/836
            container.RegisterSingleton<Presentation.ViewModels.Pages.Niconico.VideoRanking.RankingCategoryPageViewModel>();

            //unityContainer.RegisterType<Presentation.ViewModels.Player.VideoPlayerPageViewModel>(new PerThreadLifetimeManager());
            //unityContainer.RegisterType<Presentation.ViewModels.Player.LivePlayerPageViewModel>(new PerThreadLifetimeManager());

#if DEBUG
            //			BackgroundUpdater.MaxTaskSlotCount = 1;
#endif
            // TODO: プレイヤーウィンドウ上で管理する
            //			var backgroundTask = MediaBackgroundTask.Create();
            //			Container.RegisterInstance(backgroundTask);

        }

        public class ZDebugLoggerFacade : ILoggerFacade
        {
            public ZDebugLoggerFacade(ILogger<App> logger)
            {
                _logger = logger;
            }


            public void Log(string message, Category category, Priority priority)
            {
                _logger.ZLog(ToLogLevel(category, priority), "{0}: {1}. Priority:{2}. Timestamp:{3}.", CategoryText[(int)category], message, priority, DateTime.Now);
            }

            private LogLevel ToLogLevel(Category category, Priority priority)
            {
                return (category, priority) switch
                {
                    (Category.Exception, Priority.High) => LogLevel.Critical,
                    (Category.Debug, _) => LogLevel.Debug,
                    (Category.Exception, _) => LogLevel.Error,
                    (Category.Info, _) => LogLevel.Information,
                    (Category.Warn, _) => LogLevel.Warning,                    
                    _ => LogLevel.None,
                };
            }

            private readonly string[] CategoryText = new[]
            {
                Category.Debug.ToString().ToUpper(),
                Category.Exception.ToString().ToUpper(),
                Category.Info.ToString().ToUpper(),
                Category.Warn.ToString().ToUpper()
            };
            private readonly ILogger _logger;
        }


        public class ZFileLoggerFacade : ILoggerFacade
        {
            private readonly ILogger _logger;

            public ZFileLoggerFacade(ILogger logger)
            {
                _logger = logger;
            }

            public void Log(string message, Category category, Priority priority)
            {
                throw new NotImplementedException();
            }
        }

        public class EmptyLoggerFacade : ILoggerFacade
        {
            public void Log(string message, Category category, Priority priority)
            {
                
            }
        }

        public class DebugOutputStream : Stream
        {
            public DebugOutputStream(IScheduler scheduler)
            {
                _scheduler = scheduler;
            }

            public override bool CanRead => false;

            public override bool CanSeek => false;

            public override bool CanWrite => true;

            long _length;
            private readonly IScheduler _scheduler;

            public override long Length => _length;

            public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override void Flush()
            {
                
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                
            }

            public override void Write(byte[] buffer, int offset, int count)
            {                
                _length = count;
                Debug.Write(System.Text.Encoding.UTF8.GetString(buffer, offset, count));
            }
        }
       
        private ILoggerFactory _loggerFactory;

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            var mainWindowsScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current);
            containerRegistry.RegisterInstance<IScheduler>(mainWindowsScheduler);

            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)    
#if DEBUG
                    .AddZLoggerStream(new DebugOutputStream(mainWindowsScheduler), "debug-plain", opt => { })
#endif                    
                    ;  
                
                if (IsDebugModeEnabled)
                {
                    FileStream _logFileStream = new FileStream(ApplicationData.Current.TemporaryFolder.CreateSafeFileHandle("_log.txt", System.IO.FileMode.OpenOrCreate, FileAccess.Write), FileAccess.Write, 2 ^ 20);
                    _logFileStream.SetLength(0);
                    builder.AddFilter("Hohoema.App", DebugLogLevel)
                        .AddZLoggerStream(_logFileStream, "file-plain", opt => { opt.EnableStructuredLogging = false; })                        
                        ;
                }
                else
                {
#if DEBUG
                    builder.AddFilter("Hohoema.App", LogLevel.Debug);
#else
                    if (Debugger.IsAttached)
                    {
                        builder.AddFilter("Hohoema.App", LogLevel.Debug);
                    }
                    else 
                    {
                        builder.AddFilter("Hohoema.App", LogLevel.Error);
                    }
                    
#endif
                }
            });

            var logger = _loggerFactory.CreateLogger<App>();
            containerRegistry.RegisterInstance<ILoggerFactory>(_loggerFactory);
            containerRegistry.RegisterInstance<ILogger>(logger);
            containerRegistry.RegisterInstance<ILogger<App>>(logger);
#if DEBUG
            containerRegistry.RegisterSingleton<ILoggerFacade, ZDebugLoggerFacade>();
#else
            containerRegistry.RegisterSingleton<ILoggerFacade, EmptyLoggerFacade>();
#endif

            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.BlankPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.DebugPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.SettingsPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.LocalMylist.LocalPlaylistPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.LocalMylist.LocalPlaylistManagePage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.Queue.VideoQueuePage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.Subscription.SubscriptionManagementPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Hohoema.VideoCache.CacheManagementPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Activity.WatchHistoryPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Channel.ChannelVideoPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Community.CommunityPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Community.CommunityVideoPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Follow.FollowManagePage>(); 
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Live.LiveInfomationPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Live.TimeshiftPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Mylist.MylistPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Mylist.OwnerMylistManagePage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Mylist.UserMylistPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.NicoRepo.NicoRepoPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Search.SearchPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Search.SearchResultTagPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Search.SearchResultKeywordPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Search.SearchResultLivePage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Series.SeriesPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Series.UserSeriesPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.User.UserInfoPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.User.UserVideoPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.Video.VideoInfomationPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.VideoRanking.RankingCategoryListPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Pages.Niconico.VideoRanking.RankingCategoryPage>();
            
            containerRegistry.RegisterForNavigation<Presentation.Views.Player.LivePlayerPage>();
            containerRegistry.RegisterForNavigation<Presentation.Views.Player.VideoPlayerPage>();
        }

        public bool IsTitleBarCustomized { get; } = DeviceTypeHelper.IsDesktop && InputCapabilityHelper.IsMouseCapable;

        Models.Helpers.AsyncLock InitializeLock = new Models.Helpers.AsyncLock();
        bool isInitialized = false;
        private async Task EnsureInitializeAsync()
        {
            using var initializeLock = await InitializeLock.LockAsync();

            if (isInitialized) { return; }
            isInitialized = true;

            var logger = Container.Resolve<ILogger>();

            async Task TryMigrationAsync(Type[] migrateTypes)
            {
                foreach (var migrateType in migrateTypes)
                {
                    try
                    {
                        logger.ZLogInformation("Try migrate: {0}", migrateType.Name);
                        var migrater = Container.Resolve(migrateType);
                        if (migrater is IMigrateSync migrateSycn)
                        {
                            migrateSycn.Migrate();
                        }
                        else if (migrater is IMigrateAsync migrateAsync)
                        {
                            await migrateAsync.MigrateAsync();
                        }

                        logger.ZLogInformation("Migration complete : {0}", migrateType.Name);                        
                    }
                    catch (Exception e)
                    {
                        logger.ZLogError(e.ToString());
                        logger.ZLogError("Migration failed : {0}",migrateType.Name);
                    }
                }
            }

            if (Microsoft.Toolkit.Uwp.Helpers.SystemInformation.Instance.IsAppUpdated)
            {
                await TryMigrationAsync(new Type[]
                {
                    //typeof(MigrationCommentFilteringSettings),
                    //typeof(CommentFilteringNGScoreZeroFixture),
                    //typeof(SettingsMigration_V_0_23_0),
                    //typeof(SearchPageQueryMigrate_0_26_0),
                    typeof(VideoCacheDatabaseMigration_V_0_29_0),
                    typeof(SearchTargetMigration_V_1_1_0),
                });
            }
            // 機能切り替え管理クラスをDIコンテナに登録
            // Xaml側で扱いやすくするためApp.xaml上でインスタンス生成させている
            {
                var unityContainer = Container.GetContainer();
                unityContainer.UseInstance(Resources["FeatureFlags"] as FeatureFlags);
            }

            // ローカリゼーション用のライブラリを初期化
            try
            {
                I18NPortable.I18N.Current
#if DEBUG
                //.SetLogger(text => System.Diagnostics.Debug.WriteLine(text))
                .SetNotFoundSymbol("🍣")
#endif
                .SetFallbackLocale("ja")
                .Init(GetType().Assembly);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            Resources["Strings"] = I18NPortable.I18N.Current;

            var appearanceSettings = Container.Resolve<Models.Domain.Application.AppearanceSettings>();
            I18NPortable.I18N.Current.Locale = appearanceSettings.Locale ?? I18NPortable.I18N.Current.Languages.FirstOrDefault(x => x.Locale.StartsWith(CultureInfo.CurrentCulture.TwoLetterISOLanguageName)).Locale ?? I18NPortable.I18N.Current.Locale;

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(I18NPortable.I18N.Current.Locale);

            //Console.WriteLine(settings.AppearanceSettings.Locale);
            //Console.WriteLine(I18N.Current.Locale);
            //Console.WriteLine(CultureInfo.CurrentCulture.Name);


            Resources["IsXbox"] = DeviceTypeHelper.IsXbox;
            Resources["IsMobile"] = DeviceTypeHelper.IsMobile;


            try
            {
#if DEBUG
                if (_DEBUG_XBOX_RESOURCE)
#else
                    if (DeviceTypeHelper.IsXbox)
#endif
                {
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("ms-appx:///Styles/TVSafeColor.xaml")
                    });
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri("ms-appx:///Styles/TVStyle.xaml")
                    });
                }
            }
            catch
            {

            }

            Resources["IsDebug_XboxLayout"] = _DEBUG_XBOX_RESOURCE;

#if DEBUG
            Resources["IsDebug"] = true;
#else
            Resources["IsDebug"] = false;
#endif
            Resources["TitleBarCustomized"] = IsTitleBarCustomized;
            Resources["TitleBarDummyHeight"] = IsTitleBarCustomized ? 32.0 : 0.0;


            if (IsTitleBarCustomized)
            {
                var coreApp = CoreApplication.GetCurrentView();
                coreApp.TitleBar.ExtendViewIntoTitleBar = true;

                var appView = ApplicationView.GetForCurrentView();
                appView.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                appView.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                if (RequestedTheme == ApplicationTheme.Light)
                {
                    appView.TitleBar.ButtonForegroundColor = Colors.Black;
                    appView.TitleBar.ButtonHoverBackgroundColor = Colors.DarkGray;
                    appView.TitleBar.ButtonHoverForegroundColor = Colors.Black;
                    appView.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
                }
                else
                {
                    appView.TitleBar.ButtonForegroundColor = Colors.White;
                    appView.TitleBar.ButtonHoverBackgroundColor = Colors.DimGray;
                    appView.TitleBar.ButtonHoverForegroundColor = Colors.White;
                    appView.TitleBar.ButtonInactiveForegroundColor = Colors.DarkGray;
                }
            }

            // 
            var cacheSettings = Container.Resolve<VideoCacheSettings_Legacy>();
            Resources["IsCacheEnabled"] = cacheSettings.IsEnableCache;

            // ウィンドウコンテンツを作成
            Window.Current.Content = CreateShell();

            // ウィンドウサイズの保存と復元
            if (DeviceTypeHelper.IsDesktop)
            {
                var localObjectStorageHelper = Container.Resolve<Microsoft.Toolkit.Uwp.Helpers.LocalObjectStorageHelper>();
                if (localObjectStorageHelper.KeyExists(SecondaryViewPlayerManager.primary_view_size))
                {
                    var view = ApplicationView.GetForCurrentView();
                    MainViewId = view.Id;
                    _PrevWindowSize = localObjectStorageHelper.Read<Size>(SecondaryViewPlayerManager.primary_view_size);
                    view.TryResizeView(_PrevWindowSize);
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                }
            }

            // XboxOneで外枠表示を行わないように設定
            if (DeviceTypeHelper.IsXbox)
            {
                Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode
                    (Windows.UI.ViewManagement.ApplicationViewBoundsMode.UseCoreWindow);
            }

            // モバイルでナビゲーションバーをアプリに被せないように設定
            if (DeviceTypeHelper.IsMobile)
            {
                // モバイルで利用している場合に、ナビゲーションバーなどがページに被さらないように指定
                ApplicationView.GetForCurrentView().SuppressSystemOverlays = true;
                ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
            }

            // キャッシュ機能の初期化
            {
                var cacheManager = Container.Resolve<VideoCacheFolderManager>();

                await cacheManager.InitializeAsync();
            }




            // 2段階認証を処理するログインサービスをインスタンス化
            var loginService = Container.Resolve<NiconicoLoginService>();

            // ログイン前にログインセッションによって状態が変化するフォローとマイリストの初期化
            var mylitManager = Container.Resolve<LoginUserOwnedMylistManager>();

            {
                var unityContainer = Container.GetContainer();

                unityContainer.Resolve<Models.UseCase.Migration.CommentFilteringNGScoreZeroFixture>().Migration();



                // アプリのユースケース系サービスを配置
                unityContainer.RegisterInstance(unityContainer.Resolve<NotificationCacheVideoDeletedService>());
                unityContainer.RegisterInstance(unityContainer.Resolve<CheckingClipboardAndNotificationService>());
                unityContainer.RegisterInstance(unityContainer.Resolve<FollowNotificationAndConfirmListener>());
                unityContainer.RegisterInstance(unityContainer.Resolve<SubscriptionUpdateManager>());
                unityContainer.RegisterInstance(unityContainer.Resolve<SyncWatchHistoryOnLoggedIn>());
                unityContainer.RegisterInstance(unityContainer.Resolve<FeedResultAddToWatchLater>());
                unityContainer.RegisterInstance(unityContainer.Resolve<LatestSubscriptionVideosNotifier>());

                unityContainer.RegisterInstance(unityContainer.Resolve<VideoPlayRequestBridgeToPlayer>());
                unityContainer.RegisterInstance(unityContainer.Resolve<CloseToastNotificationWhenPlayStarted>());
                unityContainer.RegisterInstance(unityContainer.Resolve<AutoSkipToPlaylistNextVideoWhenPlayFailed>());
                
                unityContainer.RegisterInstance(unityContainer.Resolve<VideoCacheDownloadOperationManager>());
            }

            // バックグラウンドでのトースト通知ハンドリングを初期化
            await RegisterDebugToastNotificationBackgroundHandling();


            // 更新通知を表示
            try
            {
                if (AppUpdateNotice.IsUpdated)
                {
                    var version = Windows.ApplicationModel.Package.Current.Id.Version;
                    var notificationService = Container.Resolve<NotificationService>();
                    notificationService.ShowInAppNotification(new InAppNotificationPayload()
                    {
                        Content = ZString.Format("Hohoema v{0}.{1}.{2} に更新しました", version.Major, version.Minor, version.Build),
                        ShowDuration = TimeSpan.FromSeconds(7),
                        IsShowDismissButton = true,
                        Commands =
                            {
                                new InAppNotificationCommand()
                                {
                                    Command = new DelegateCommand(async () =>
                                    {
                                        await AppUpdateNotice.ShowReleaseNotePageOnBrowserAsync();
                                    }),
                                    Label = "更新情報を確認（ブラウザで表示）"
                                }
                            }
                    });
                    AppUpdateNotice.UpdateLastCheckedVersionInCurrentVersion();
                }
            }
            catch { }


            /*
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated
                || args.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
            {
                if (!ApiContractHelper.Is2018FallUpdateAvailable)
                {

                }
            }
            else
            {
                var pageManager = Container.Resolve<Services.PageManager>();


#if false
            try
            {
                if (localStorge.Read(IS_COMPLETE_INTRODUCTION, false) == false)
                {
                    // アプリのイントロダクションを開始
                    pageManager.OpenIntroductionPage();
                }
                else
                {
                    pageManager.OpenStartupPage();
                }
            }
            catch
            {
                Debug.WriteLine("イントロダクションまたはスタートアップのページ表示に失敗");
                pageManager.OpenPage(HohoemaPageType.RankingCategoryList);
            }
#else
                try
                {
                    pageManager.OpenStartupPage();
                }
                catch
                {
                    Debug.WriteLine("スタートアップのページ表示に失敗");
                    pageManager.OpenPage(HohoemaPageType.RankingCategoryList);
                }
#endif
            }
            */


        }

        

        private async Task OnActivateApplicationAsync(IActivatedEventArgs args)
		{
            var niconicoSession = Container.Resolve<NiconicoSession>();

            // 外部から起動した場合にサインイン動作と排他的動作にさせたい
            // こうしないと再生処理を正常に開始できない
            using (await niconicoSession.SigninLock.LockAsync())
            {
                await Task.Delay(50);
            }

            if (args.Kind == ActivationKind.ToastNotification)
            {
                var toastArgs = args as IActivatedEventArgs as ToastNotificationActivatedEventArgs;
                var arguments = toastArgs.Argument;

                await Container.Resolve<NavigationTriggerFromExternal>().Process(arguments);
            }
        }


        bool _isNavigationStackRestored = false;



        public override async void OnInitialized()
        {
            Window.Current.Activate();

            await EnsureInitializeAsync();

            // ログイン
            try
            {
                var niconicoSession = Container.Resolve<NiconicoSession>();
                if (AccountManager.HasPrimaryAccount())
                {
                    // サインイン処理の待ちを初期化内でしないことで初期画面表示を早める
                    await niconicoSession.SignInWithPrimaryAccount();
                }
            }
            catch
            {
                Container.Resolve<ILogger>().ZLogError("ログイン処理に失敗");
            }

#if !DEBUG
            var navigationService = Container.Resolve<PageManager>();
            var settings = Container.Resolve<AppearanceSettings>();
            navigationService.OpenPage(settings.FirstAppearPageType);
#endif

#if false
            try
            {
                if (!_isNavigationStackRestored)
                {
                    var niconicoSession = Container.Resolve<NiconicoSession>();

                    // 外部から起動した場合にサインイン動作と排他的動作にさせたい
                    // こうしないと再生処理を正常に開始できない
                    using (await niconicoSession.SigninLock.LockAsync())
                    {
                        await Task.Delay(50);
                    }
                    _isNavigationStackRestored = true;
                    //                    await _primaryWindowCoreLayout.RestoreNavigationStack();
                    // TODO: 前回再生中に終了したコンテンツを表示するかユーザーに確認
                    var vm = _primaryWindowCoreLayout.DataContext as PrimaryWindowCoreLayoutViewModel;
                    var lastPlaying = vm.RestoreNavigationManager.GetCurrentPlayerEntry();
                    if (lastPlaying != null)
                    {
                        _ = WeakReferenceMessenger.Default.Send(new VideoPlayRequestMessage() { VideoId = lastPlaying.ContentId, PlaylistId = lastPlaying.PlaylistId, PlaylistOrigin = lastPlaying.PlaylistOrigin, Potision = lastPlaying.Position });
                    }
                }
            }
            catch { }
#endif


            base.OnInitialized();
        }



        async Task BackgroundActivated(BackgroundActivatedEventArgs args)
        {
            var deferral = args.TaskInstance.GetDeferral();

            try
            {
                switch (args.TaskInstance.Task.Name)
                {
                    case "ToastBackgroundTask":
                        var details = args.TaskInstance.TriggerDetails as Windows.UI.Notifications.ToastNotificationActionTriggerDetail;
                        if (details != null)
                        {
                            string arguments = details.Argument;
                            var userInput = details.UserInput;

                            await Task.Run(() => Container.Resolve<NavigationTriggerFromExternal>().Process(arguments));
                        }
                        break;
                }
            }
            finally
            {
                deferral.Complete();
            }
        }



		


#region Page and Application Appiarance

        readonly static Regex ViewToViewModelNameReplaceRegex = new Regex("[^.]+$");
        public override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => 
            {
                var pageToken = viewType.Name;

                if (pageToken.EndsWith("_TV"))
                {
                    pageToken = pageToken.Remove(pageToken.IndexOf("_TV"));
                }
                else if (pageToken.EndsWith("_Mobile"))
                {
                    pageToken = pageToken.Remove(pageToken.IndexOf("_Mobile"));
                }

                var viewModelFullName = ViewToViewModelNameReplaceRegex.Replace(viewType.FullName, $"{pageToken}ViewModel")
                    .Replace(".Views.", ".ViewModels.");

//                var viewModelFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
                var viewModelType = Type.GetType(viewModelFullName);

                if (viewModelType == null)
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.InvariantCulture, pageToken, this.GetType().Namespace + ".ViewModels"),
                        "pageToken");
                }

                return viewModelType;

            });

            base.ConfigureViewModelLocator();
        }




        private Type GetPageType(string pageToken)
        {
            var layoutManager= Container.Resolve<ApplicationLayoutManager>();
            
            Type viewType = null;
            if (layoutManager.AppLayout == ApplicationLayout.TV)
            {
                // pageTokenに対応するXbox表示用のページの型を取得
                try
                {
                    var assemblyQualifiedAppType = this.GetType().AssemblyQualifiedName;

                    var pageNameWithParameter = assemblyQualifiedAppType.Replace(this.GetType().FullName, this.GetType().Namespace + ".Views.{0}Page_TV");

                    var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
                    viewType = Type.GetType(viewFullName);
                }
                catch { }
            }
            else if (layoutManager.AppLayout == ApplicationLayout.Mobile)
            {
                try
                {
                    var assemblyQualifiedAppType = this.GetType().AssemblyQualifiedName;

                    var pageNameWithParameter = assemblyQualifiedAppType.Replace(this.GetType().FullName, this.GetType().Namespace + ".Views.{0}Page_Mobile");

                    var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
                    viewType = Type.GetType(viewFullName);
                }
                catch { }
            }

            return viewType;// ?? base.GetPageType(pageToken);
        }


        
#endregion


#region Multi Window Size Restoring


        private int MainViewId = -1;
        private Size _PrevWindowSize;
        private PrimaryWindowCoreLayout _primaryWindowCoreLayout;

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
		{
			base.OnWindowCreated(args);

            var view = ApplicationView.GetForCurrentView();
            view.VisibleBoundsChanged += (sender, e) => 
            {
                if (MainViewId == sender.Id)
                {
                    var localObjectStorageHelper = Container.Resolve<Microsoft.Toolkit.Uwp.Helpers.LocalObjectStorageHelper>();
                    _PrevWindowSize = localObjectStorageHelper.Read<Size>(SecondaryViewPlayerManager.primary_view_size);
                    localObjectStorageHelper.Save(SecondaryViewPlayerManager.primary_view_size, new Size(sender.VisibleBounds.Width, sender.VisibleBounds.Height));

                    Debug.WriteLine("MainView VisibleBoundsChanged : " + sender.VisibleBounds.ToString());
                }
            };
            view.Consolidated += (sender, e) => 
            {
                if (sender.Id == MainViewId)
                {
                    var localObjectStorageHelper = Container.Resolve<Microsoft.Toolkit.Uwp.Helpers.LocalObjectStorageHelper>();
                    if (_PrevWindowSize != default(Size))
                    {
                        localObjectStorageHelper.Save(SecondaryViewPlayerManager.primary_view_size, _PrevWindowSize);
                    }
                    MainViewId = -1;
                }
            };
        }


#endregion


#region Theme 


        const string ThemeTypeKey = "Theme";

        public static void SetTheme(ApplicationTheme theme)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ThemeTypeKey))
            {
                ApplicationData.Current.LocalSettings.Values[ThemeTypeKey] = theme.ToString();
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values.Add(ThemeTypeKey, theme.ToString());
            }
        }

        public static ApplicationTheme GetTheme()
        {
            try
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey(ThemeTypeKey))
                {
                    return (ApplicationTheme)Enum.Parse(typeof(ApplicationTheme), (string)ApplicationData.Current.LocalSettings.Values[ThemeTypeKey]);
                }
            }
            catch { }

            return ApplicationTheme.Dark;
        }

#endregion


#region Debug

        const string DEBUG_MODE_ENABLED_KEY = "Hohoema_DebugModeEnabled";
        public bool IsDebugModeEnabled
        {
            get
            {
                var enabled = ApplicationData.Current.LocalSettings.Values[DEBUG_MODE_ENABLED_KEY];
                if (enabled != null)
                {
                    return (bool)enabled;
                }
                else
                {
                    return false;
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[DEBUG_MODE_ENABLED_KEY] = value;
                _primaryWindowCoreLayout.IsDebugModeEnabled = value;
            }
        }
        
        const string DEBUG_LOG_LEVEL_KEY = "Hohoema_LogLevel";
        public LogLevel DebugLogLevel
        {
            get
            {
                var enabled = ApplicationData.Current.LocalSettings.Values[DEBUG_LOG_LEVEL_KEY];
                if (enabled != null)
                {
                    return (LogLevel)enabled;
                }
                else
                {
                    return LogLevel.Debug;
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[DEBUG_LOG_LEVEL_KEY] = value;
            }
        }

        bool isFirstCrashe = true;

        private void PrismUnityApplication_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Debug.Write(e.Message);

            e.Handled = true;

            if (e.Exception is OperationCanceledException)
            {
                return;
            }

            if (e.Exception is ObjectDisposedException)
            {
                return;
            }

            if (!isFirstCrashe)
            {
                return;
            }

            isFirstCrashe = false;
            e.Handled = true;

            var logger = Container.Resolve<ILogger>();
            logger.ZLogError(e.Exception.ToString());
        }

        // エラー報告用に画面のスクショを取れるように
        public async Task<Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap> GetApplicationContentImage()
        {
            var rtb = new Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap();
            await rtb.RenderAsync(_primaryWindowCoreLayout);
            return rtb;
        }

        public async Task<Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap> GetApplicationContentImage(int scaledWidth, int scaledHeight)
        {
            var rtb = new Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap();
            await rtb.RenderAsync(_primaryWindowCoreLayout, scaledWidth, scaledHeight);
            return rtb;
        }



        private async Task RegisterDebugToastNotificationBackgroundHandling()
        {
            try
            {
                const string taskName = "ToastBackgroundTask";

                // If background task is already registered, do nothing
                if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
                    return;

                // Otherwise request access
                BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

                // Create the background task
                BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
                {
                    Name = taskName
                };

                // Assign the toast action trigger
                builder.SetTrigger(new ToastNotificationActionTrigger());

                // And register the task
                BackgroundTaskRegistration registration = builder.Register();
            }
            catch { }
        }


#endregion

    }





}
