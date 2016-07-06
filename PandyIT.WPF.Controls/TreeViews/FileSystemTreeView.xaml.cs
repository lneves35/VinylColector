using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Controls.TreeViews
{
    /// <summary>
    /// Interaction logic for FileSystemPanel.xaml
    /// </summary>
    public partial class FileSystemTreeView : DockPanel
    {
        public FileSystemTreeView()
        {
            InitializeComponent();
        }

        #region Properties

        #region SelectedItem

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(TreeItem), typeof(FileSystemTreeView));

        public TreeItem SelectedItem
        {
            get { return (TreeItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion SelectedItem

        private List<TreeItem> root;
        public List<TreeItem> Root
        {
            get
            {
                return root ?? (root = new List<TreeItem>() { new TreeItemMyComputer() });
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
                            SelectedItem = (TreeItem)e.NewValue;
                        })
                    );
            }

        }

        #endregion Commands

    }    
    
}
