using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.UI.WPF.UserControls.TreeViews
{
    public class TreeItemLocationIconResolver
    {
        private static TreeItemLocationIconResolver instance;

        public static TreeItemLocationIconResolver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TreeItemLocationIconResolver();
                }
                return instance;
            }
        }

        private TreeItemLocationIconResolver() {}

        public string GetResourceName(Location location)
        {
            var type = location.GetType().ToString();

            if (location.ParentLocationId == null)
                return "DatabaseIcon";
            else
                return "BlueFolderIcon";
        }
    }
}
