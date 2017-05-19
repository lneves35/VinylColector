namespace PandyIT.VinylOrganizer.BAL.Business.Youtube
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DiscogsClient.Data.Query;

    public interface IYoutubeHarvester
    {
        void ExtractMp3(Uri videoSource);

        void ExtractMp3(Uri videoSource, DirectoryInfo outputFolder);

        void ExtractMp3(int discogsId, DiscogsEntityType entityType);

        void ExtractMp3FromText(string text);

        void ExtractMp3FromTextLines(IEnumerable<string> lines);

        void ExtractMp3FromTextFile(string filePath);
    }
}