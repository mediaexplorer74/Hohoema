﻿using I18NPortable;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Hohoema.Presentation.Views.Controls.VideoList
{
    public sealed partial class VideoListItemControl : UserControl
    {
        static public string LocalizedText_PostAt_Short = "VideoPostAt_Short".Translate();
        static public string LocalizedText_ViewCount_Short = "ViewCount_Short".Translate();
        static public string LocalizedText_CommentCount_Short = "CommentCount_Short".Translate();
        static public string LocalizedText_MylistCount_Short = "MylistCount_Short".Translate();

        public VideoListItemControl()
        {
            this.InitializeComponent();            
        }

        public bool IsThumbnailUseCache
        {
            get { return (bool)GetValue(IsThumbnailUseCacheProperty); }
            set { SetValue(IsThumbnailUseCacheProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsThumbnailUseCache.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsThumbnailUseCacheProperty =
            DependencyProperty.Register("IsThumbnailUseCache", typeof(bool), typeof(VideoListItemControl), new PropertyMetadata(true));


        #region NG Video Owner


        public bool IsRevealHiddenVideo
        {
            get { return (bool)GetValue(IsRevealHiddenVideoProperty); }
            set { SetValue(IsRevealHiddenVideoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRevealHiddenVideo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRevealHiddenVideoProperty =
            DependencyProperty.Register("IsRevealHiddenVideo", typeof(bool), typeof(VideoListItemControl), new PropertyMetadata(false));


        private void HiddenVideoOnceRevealButton_Click(object sender, RoutedEventArgs e)
        {
            IsRevealHiddenVideo = true;
        }

        private void ExitRevealButton_Click(object sender, RoutedEventArgs e)
        {
            IsRevealHiddenVideo = false;
        }

        #endregion

    }
}