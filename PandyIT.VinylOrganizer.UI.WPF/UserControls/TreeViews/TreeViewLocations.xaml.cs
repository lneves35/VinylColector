using GalaSoft.MvvmLight.CommandWpf;
using System;
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
        public ObservableCollection<TreeItemLocation> TreeItemLocations { get; set; }


        public TreeViewLocations()
        {
            InitializeComponent();
            var localLocations = ((App)Application.Current).VinylOrganizerDbContext.Locations.Local;
            var treeItemLocations = localLocations.ToList().Select(l => new TreeItemLocation(l, this));
            TreeItemLocations = new ObservableCollection<TreeItemLocation>(treeItemLocations);
        }

        //public static readonly DependencyProperty SelectedItemProperty =
        //    DependencyProperty.Register("SelectedItem", typeof(TreeItemLocation), typeof(TreeViewLocations));

        //public TreeItemLocation SelectedItem
        //{
        //    get { return (TreeItemLocation)GetValue(SelectedItemProperty); }
        //    set { SetValue(SelectedItemProperty, value); }
        //}

        private ICollectionView root;
        public ICollectionView Root
        {
            get
            {
                if (root == null)
                {
                    ICollectionView rootView = new CollectionViewSource { Source = this.TreeItemLocations }.View;
                    rootView.Filter = (item) =>
                    {
                        var l = item as TreeItemLocation;
                        return l.Location.ParentLocationId == null;
                    };
                    this.root = rootView;
                }
                return root;
            }
        }
        

        //private RelayCommand<RoutedPropertyChangedEventArgs<Object>> selectedItemChangedCommand;
        //public RelayCommand<RoutedPropertyChangedEventArgs<Object>> SelectedItemChangedCommand
        //{
        //    get
        //    {
        //        return selectedItemChangedCommand ??
        //            (selectedItemChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<Object>>(
        //                (e) =>
        //                {
        //                    SelectedItem = (TreeItemLocation)e.NewValue;
        //                })
        //            );
        //    }

        //}

    }
}
