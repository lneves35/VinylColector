using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews.Location
{
    /// <summary>
    /// Interaction logic for TreeViewLocations.xaml
    /// </summary>
    public partial class TreeViewLocations : DockPanel
    {
        public TreeViewLocations()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(TreeItemLocation), typeof(TreeViewLocations));

        public TreeItemLocation SelectedItem
        {
            get { return (TreeItemLocation)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }


        private List<TreeItemLocation> root;
        public List<TreeItemLocation> Root
        {
            get
            {
                return root ?? (root = new List<TreeItemLocation>() { new TreeItemLocationVinyl() });
            }
        }

        #endregion Properties

        #region Commands

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

        #endregion Commands
    }
}
