namespace PandyIT.VinylOrganizer.Services.Extractors
{
    using System;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public interface ITrackListExtractor
    {
        HarvestedTrackList GetTrackList(Uri url);

        bool CanExtract(Uri url);
    }
}