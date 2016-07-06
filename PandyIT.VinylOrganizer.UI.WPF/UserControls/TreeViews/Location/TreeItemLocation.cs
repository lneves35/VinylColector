using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews.Location
{
    public class TreeItemLocation
    {
        private static TreeItemLocation dummy = new TreeItemLocation();

        public TreeItemLocation()
        {
            Children = new ObservableCollection<TreeItemLocation>();
            Children.Add(dummy);
        }

        public virtual string Text { get { return "dummy"; } }

        public virtual IEnumerable<TreeItemLocation> Items { get; set; }

        private bool isExpanded;
        public bool IsExpanded
        {
            set
            {
                isExpanded = value;
                if (isExpanded == true && Children.Remove(dummy) && Items!=null)
                {
                    foreach (TreeItemLocation t in Items)
                        Children.Add(t);
                }
            }
            get { return isExpanded; }
        }

        public ObservableCollection<TreeItemLocation> Children
        {
            get;
            set;
        }
    }
}
