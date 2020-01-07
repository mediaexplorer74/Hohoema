﻿using NicoPlayerHohoema.Models.Provider;
using System;
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
using Prism.Ioc;
using NicoPlayerHohoema.Repository.NicoVideo;
using System.Windows.Input;
using NicoPlayerHohoema.Views.Flyouts;
using NicoPlayerHohoema.Interfaces;
using NicoPlayerHohoema.UseCase.Playlist;
using System.Threading.Tasks;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace NicoPlayerHohoema.Views.Pages.VideoListPage
{
    public sealed partial class VideoItemsListView : UserControl
    {


        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(VideoItemsListView), new PropertyMetadata(null));




        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(VideoItemsListView), new PropertyMetadata(null));


        public FlyoutBase ItemContextFlyout
        {
            get { return (FlyoutBase)GetValue(ItemContextFlyoutProperty); }
            set { SetValue(ItemContextFlyoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContextFlyoutProperty =
            DependencyProperty.Register("ItemContextFlyout", typeof(FlyoutBase), typeof(VideoItemsListView), new PropertyMetadata(null));





        public ICommand ItemCommand
        {
            get { return (ICommand)GetValue(ItemCommandProperty); }
            set { SetValue(ItemCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCommandProperty =
            DependencyProperty.Register("ItemCommand", typeof(ICommand), typeof(VideoItemsListView), new PropertyMetadata(null));




        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(VideoItemsListView), new PropertyMetadata(null));





        public bool IsUpdateSourceVideoItem
        {
            get { return (bool)GetValue(IsUpdateSourceVideoItemProperty); }
            set { SetValue(IsUpdateSourceVideoItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsUpdateSourceVideoItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUpdateSourceVideoItemProperty =
            DependencyProperty.Register("IsUpdateSourceVideoItem", typeof(bool), typeof(VideoItemsListView), new PropertyMetadata(true));


        private readonly VideoInfoRepository _videoInfoRepository;
        private readonly VideoItemsSelectionContext _selectionContext;

        public VideoItemsListView()
        {
            this.InitializeComponent();

            // Selection
            _selectionContext = App.Current.Container.Resolve<VideoItemsSelectionContext>();

            // Update Video Item
            _videoInfoRepository = App.Current.Container.Resolve<Repository.NicoVideo.VideoInfoRepository>();
            
            Loaded += VideoItemsListView_Loaded;
            Unloaded += VideoItemsListView_Unloaded;
        }

        private void VideoItemsListView_Loaded(object sender, RoutedEventArgs e)
        {
            ItemsList.IsItemClickEnabled = true;
            ItemsList.ItemClick += ItemsList_ItemClick;

            // Selection
            _selectionContext.RequestSelectAll += _selectionContext_RequestSelectAll;
            _selectionContext.SelectionStarted += _selectionContext_SelectionStarted;
            ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            
            // Context Flyout
            ItemsList.ContextRequested += ItemsList_ContextRequested;
            
            // Update Video Item
            ItemsList.ContainerContentChanging += ItemsList_ContainerContentChanging;
        }

        private void ItemsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemCommand = ItemCommand;
            if (itemCommand == null) { return; }

            if (ItemsList.SelectionMode == ListViewSelectionMode.None 
                || ItemsList.SelectionMode == ListViewSelectionMode.Single)
            {
                if (itemCommand.CanExecute(e.ClickedItem))
                {
                    itemCommand.Execute(e.ClickedItem);
                }
            }
        }

        private void VideoItemsListView_Unloaded(object sender, RoutedEventArgs e)
        {
            // Selection
            _selectionContext.RequestSelectAll -= _selectionContext_RequestSelectAll;
            _selectionContext.SelectionStarted -= _selectionContext_SelectionStarted;
            ItemsList.SelectionChanged -= ItemsList_SelectionChanged;

            // Context Flyout
            ItemsList.ContextRequested -= ItemsList_ContextRequested;

            // Update Video Item
            ItemsList.ContainerContentChanging -= ItemsList_ContainerContentChanging;
            _updatedItemsId.Clear();
        }

        #region Selection

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectionContext.IsSelectionEnabled)
            {
                if (e.RemovedItems?.Any() ?? false)
                {
                    foreach (var removedItem in e.RemovedItems)
                    {
                        if (removedItem is IVideoContent item)
                        {
                            _selectionContext.SelectionItems.Remove(item);
                        }
                    }
                }
                if (e.AddedItems?.Any() ?? false)
                {
                    foreach (var addedItem in e.AddedItems)
                    {
                        if (addedItem is IVideoContent item)
                        {
                            if (!_selectionContext.SelectionItems.Contains(item))
                            {
                                _selectionContext.SelectionItems.Add(item);
                            }
                        }
                    }
                }
            }
        }

        private async void _selectionContext_SelectionStarted(object sender, RequestSelectionStartEventArgs e)
        {
            await Task.Delay(50);

            var item = ItemsList.Items.FirstOrDefault(x => x == e.FirstSelectedItem);
            var container = ItemsList.ContainerFromItem(item);
            var index = ItemsList.IndexFromContainer(container);
            ItemsList.SelectRange(new ItemIndexRange(index, 1));
        }

        private void _selectionContext_RequestSelectAll(object sender, RequestSelectAllEventArgs e)
        {
            if (ItemsList.SelectedItems.Count == ItemsList.Items.Count)
            {
                foreach (var range in ItemsList.SelectedRanges.ToList())
                {
                    ItemsList.DeselectRange(range);
                }
            }
            else
            {
                ItemsList.SelectAll();
            }
        }

        #endregion



        #region Context Flyout

        private void ItemsList_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
        {
            var list = sender as ListViewBase;

            var itemFlyout = ItemContextFlyout;
            if (itemFlyout == null) { return; }

            if (itemFlyout is VideoItemFlyout videoItemFlyout)
            {
                if (list.SelectedItems.Count > 0)
                {
                    videoItemFlyout.VideoItems = list.SelectedItems.Cast<IVideoContent>().ToList();
                }
                else
                {
                    videoItemFlyout.VideoItems = null;
                }
            }

            if (sender == args.OriginalSource) { return; }

            // コントローラ操作時にSelectorItem.DataContext == null になる
            // MenuFlyoutItemのCommand等が解決できるようにDataContextを予め埋める
            if (args.OriginalSource is SelectorItem selectorItem)
            {
                selectorItem.DataContext = selectorItem.Content;
            }

            itemFlyout.ShowAt(args.OriginalSource as FrameworkElement);
            args.Handled = true;
        }


        #endregion


        #region Update Video Item



        HashSet<string> _updatedItemsId = new HashSet<string>();

        private void ItemsList_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var videoContent = args.Item as Interfaces.IVideoContentWritable;
            //if (videoContent != null && !_updatedItemsId.Contains(videoContent.Id))
            if (videoContent != null)
            {
                _updatedItemsId.Add(videoContent.Id);
                _ = _videoInfoRepository.UpdateAsync(videoContent);

                System.Diagnostics.Debug.WriteLine("updated : " + videoContent.Id);
            }
        }


        #endregion
    }
}
