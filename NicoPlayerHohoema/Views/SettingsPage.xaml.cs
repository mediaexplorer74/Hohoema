﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace NicoPlayerHohoema.Views
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class SettingsPage : Page
	{
		public SettingsPage()
		{
			this.InitializeComponent();
		}
	}


	public class SettingContentTemplateSelector : DataTemplateSelector
	{
		public DataTemplate RankingTemplate { get; set; }
		public DataTemplate NGTemplate { get; set; }
		public DataTemplate PlayerTemplate { get; set; }
		public DataTemplate CacheTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			if (item is ViewModels.VideoListSettingsPageContentViewModel)
			{
				return RankingTemplate;
			}
			else if (item is ViewModels.CommentSettingsPageContentViewModel)
			{
				return NGTemplate;
			}
			else if (item is ViewModels.VideoPlaySettingsPageContentViewModel)
			{
				return PlayerTemplate;
			}
			else if (item is ViewModels.CacheSettingsPageContentViewModel)
			{
				return CacheTemplate;
			}

			return base.SelectTemplateCore(item, container);
		}
	}
}