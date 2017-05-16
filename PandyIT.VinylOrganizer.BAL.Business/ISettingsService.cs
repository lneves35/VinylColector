using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public interface ISettingsService
    {
        void AddSetting(Setting setting);
        Setting FetchSetting(string key);

        bool SettingExists(string key);
    }
}