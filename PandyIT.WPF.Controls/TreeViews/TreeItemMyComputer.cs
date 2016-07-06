using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.TreeViews
{
    public class TreeItemMyComputer : TreeItem
    {
        public TreeItemMyComputer() { }

        public override string Text
        {
            get { return Properties.Resources.TREE_ITEM_MYCOMPUTER; }
        }

        private ObservableCollection<TreeItemDrive> drives;
        public ObservableCollection<TreeItemDrive> Drives
        {
            get
            {
                if (drives == null)
                {

                }
                return drives;
            }
        }

        public override IEnumerable<TreeItem> Items
        {
            get
            {
                List<TreeItemDrive> drives = new List<TreeItemDrive>();
                drives = new List<TreeItemDrive>();
                foreach (DriveInfo d in DriveInfo.GetDrives())
                    drives.Add(new TreeItemDrive(d));
                return drives;
            }
        }

        public override string FullPath
        {
            get
            {
                return Text;
            }
        }

        public override string IconName
        {
            get
            {
                return "ComputerIcon";
            }
        }

        public override string Tooltip
        {
            get
            {
                return "My Computer";
            }
        }
    }        
}
