using GalaSoft.MvvmLight.CommandWpf;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews
{
    /// <summary>
    /// Interaction logic for TreeViewLocations.xaml
    /// </summary>
    public partial class TreeViewLocations : DockPanel
    {
        public TreeViewLocations()
        {
            InitializeComponent();
            var localLocations = ((App)Application.Current).VinylOrganizerDbContext.Locations.Local;

            ObservableCollection<TreeItemLocation> treeItemLocations = new ObservableCollection<TreeItemLocation>();
            localLocations.ToList().ForEach(l => treeItemLocations.Add(new TreeItemLocation(l, treeItemLocations)));

            var rootView = CollectionViewSource.GetDefaultView(treeItemLocations);
            rootView.Filter = (item) =>
            {
                var l = item as TreeItemLocation;
                return l.Location.ParentLocationId == null;
            };
            this.Root = rootView;
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(TreeItemLocation), typeof(TreeViewLocations));

        public TreeItemLocation SelectedItem
        {
            get { return (TreeItemLocation)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ICollectionView Root { get; set; }

        private RelayCommand<RoutedPropertyChangedEventArgs<Object>> selectedItemChangedCommand;
        public RelayCommand<RoutedPropertyChangedEventArgs<Object>> SelectedItemChangedCommand
        {
            get
            {
                return selectedItemChangedCommand ??
                    (selectedItemChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<Object>>(
                        (e) =>
                        {
                            SelectedItem = (TreeItemLocation)e.NewValue;
                        })
                    );
            }

        }

    }
}
