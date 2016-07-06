using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.TreeViews
{
    public class TreeItemFolder : TreeItem
    {
        DirectoryInfo directory_info;

        public TreeItemFolder(DirectoryInfo d) { directory_info = d; }

        public override string Text
        {
            get { return directory_info.Name; }
        }

        public override IEnumerable<TreeItem> Items
        {
            get
            {
                List<TreeItem> folders = new List<TreeItem>();
                foreach (DirectoryInfo d in directory_info.GetDirectories())
                    folders.Add(new TreeItemFolder(d));
                return folders;
            }
        }

        public override string FullPath
        {
            get
            {
                return directory_info.FullName;
            }
        }

        public override String IconName
        {
            get
            {
                return "FolderIcon";
            }
        }

        public override string Tooltip
        {
            get
            {
                return "Folder";
            }
        }
    }

}
