using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.TreeViews
{
    public class TreeItem : ViewModelBase
    {
        private static TreeItem dummy = new TreeItem();

        public TreeItem()
        {
            Children = new ObservableCollection<TreeItem>();
            Children.Add(dummy);
        }


        public virtual string Text { get { return "dummy"; } }

        public virtual String FullPath
        {
            get { return "Dummy FullPath"; }
        }

        public virtual String IconName
        {
            get { return "UnknownDriveIcon"; }
        }

        public virtual IEnumerable<TreeItem> Items { get; set; }

        public virtual String Tooltip 
        {
            get { return "Unknown"; }
        } 

        private bool isExpanded;
        public bool IsExpanded
        {
            set
            {
                isExpanded = value;
                if (isExpanded == true && Children.Remove(dummy))
                {
                    foreach (TreeItem t in Items)
                        Children.Add(t);
                }
            }
            get { return isExpanded; }
        }

        public ObservableCollection<TreeItem> Children
        {
            get;
            set;
        }

        
    }

}
