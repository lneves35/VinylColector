using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.TreeViews
{
    public class TreeItemDrive : TreeItem
    {
        DriveInfo drive_info;

        public TreeItemDrive(DriveInfo d) { drive_info = d; }

        public override string Text
        {
            get { return drive_info.Name; }
        }

        public override IEnumerable<TreeItem> Items
        {
            get
            {
                List<TreeItemFolder> folders = new List<TreeItemFolder>();
                if (!drive_info.IsReady) return folders;
                foreach (DirectoryInfo d in drive_info.RootDirectory.GetDirectories())
                    folders.Add(new TreeItemFolder(d));
                return folders;
            }
        }

        public override string FullPath
        {
            get
            {
                return drive_info.RootDirectory.FullName;
            }
        }

        public override string IconName
        {
            get
            {
                switch (drive_info.DriveType)
                {
                    case DriveType.Fixed:
                        return "HardDriveIcon";
                    case DriveType.CDRom:
                        return "CDRomIcon";
                    case DriveType.Removable:
                        return "USBIcon";
                    case DriveType.Network:
                        return "NetworkIcon";
                    default:
                        return "UnknownDriveIcon";
                }
            }
        }

        public override string Tooltip
        {
            get
            {
                switch (drive_info.DriveType)
                {
                    case DriveType.Fixed:
                        return "Hard Drive";
                    case DriveType.CDRom:
                        return "CD/DVD Drive";
                    case DriveType.Removable:
                        return "USB Drive";
                    case DriveType.Network:
                        return "Network Drive";
                    default:
                        return "Unknown Driven";
                }
            }
        }
    }

}
