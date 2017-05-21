namespace PandyIT.VinylOrganizer.Services.Harvesters
{
    using PandyIT.VinylOrganizer.DAL.Model.Entities;

    public interface IYoutubeHarvester
    {
        void HarvestMusicTrack(HarvestedMusicTrack musicTrack);
    }
}