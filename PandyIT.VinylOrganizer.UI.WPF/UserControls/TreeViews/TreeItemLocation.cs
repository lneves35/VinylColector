using System.Collections.ObjectModel;
using System.ComponentModel;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using System.Windows.Data;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews
{
    public class TreeItemLocation
    {
        public Location Location { get; set; }

        public TreeItemLocation(Location location, ObservableCollection<TreeItemLocation> treeItemLocations)
        {
            this.Location = location;

            var childrenView = CollectionViewSource.GetDefaultView(treeItemLocations);
            childrenView.Filter = (item) =>
            {
                var l = item as TreeItemLocation;
                return l.Location.ParentLocationId == this.Location.LocationId;
            };
            this.Children = childrenView;
        }

        public bool IsExpanded { get; set; }

        public ICollectionView Children { get; }
        
    }
}
