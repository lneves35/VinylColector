using System;
using System.Collections.Generic;
using System.IO;

namespace PandyIT.VinylOrganizer.BAL.Business.Youtube
{
    public interface IYoutubeService
    {
        void ExtractMp3(Uri videoSource);
        void ExtractMp3(Uri videoSource, DirectoryInfo outputFolder);
        void ExtractMp3(int discogsId);
        void ExtractMp3(IEnumerable<int> discogsIds);
    }
}