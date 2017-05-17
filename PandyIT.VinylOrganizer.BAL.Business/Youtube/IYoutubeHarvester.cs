using System;
using System.IO;
using DiscogsClient.Data.Query;

namespace PandyIT.VinylOrganizer.BAL.Business.Youtube
{
    public interface IYoutubeHarvester
    {
        void ExtractMp3(Uri videoSource);

        void ExtractMp3(Uri videoSource, DirectoryInfo outputFolder);

        void ExtractMp3(int discogsId, DiscogsEntityType entityType);

        void ExtractMp3FromTextLines(string text);

        void ExtractMp3FromTextFile(string filePath);
    }
}