namespace PandyIT.VinylOrganizer.Services.Extractors
{
    using System;
    using System.Collections.Generic;
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public interface ITrackListExtractor
    {
        IEnumerable<MusicTrack> GetTrackList(Uri url);

        bool CanExtract(Uri url);
    }
}