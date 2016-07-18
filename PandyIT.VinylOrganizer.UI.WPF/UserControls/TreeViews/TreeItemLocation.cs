using System.Collections.ObjectModel;
using System.ComponentModel;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using System.Windows.Data;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews
{
    public class TreeItemLocation
    {
        private TreeViewLocations treeViewLocations;
        
        public Location Location { get; set; }

        public TreeItemLocation(Location location, TreeViewLocations treeViewLocations)
        {
            this.Location = location;
            this.treeViewLocations = treeViewLocations;
        }

        public bool IsExpanded { get; set; }

        private ICollectionView children;

        public ICollectionView Children
        {
            get
            {
                if (children == null)
                {
                    ICollectionView childrenView = new CollectionViewSource { Source = this.treeViewLocations.TreeItemLocations }.View;

                    childrenView.Filter += (item) =>
                    {
                        var l = item as TreeItemLocation;
                        return l.Location.ParentLocationId == this.Location.LocationId;
                    };
                    this.children = childrenView;
                }
                return children;
            }
        }

        public string IconName
        {
            get
            {
                return TreeItemLocationIconResolver.Instance.GetResourceName(this.Location);
            }
        }

    }
}
