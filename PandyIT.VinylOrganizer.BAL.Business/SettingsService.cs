using System.Linq;
using log4net;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.Common;
using PandyIT.VinylOrganizer.DAL.Model.Entities;
using RestSharpHelper.OAuth1;

namespace PandyIT.VinylOrganizer.BAL.Business
{
    public class SettingsService : BaseService, ISettingsService
    {
        private readonly ILog log;

        public SettingsService(IUnitOfWork recordCaseUnitOfWork, ILog log)
            : base(recordCaseUnitOfWork)
        {
            this.log = log;
        }

        public void AddSetting(Setting setting)
        {
            this.unitOfWork.GetRepository<Setting>().Add(setting);
        }

        public Setting FetchSetting(string key)
        {
            return this.unitOfWork.GetRepository<Setting>()
                .Find(item => item.Key == key)
                .Single();
        }

        public bool SettingExists(string key)
        {
            return this.unitOfWork.GetRepository<Setting>()
                .Find(item => item.Key == key)
                .Any();
        }

        public OAuthCompleteInformation FetchDiscogsAuthenticationSettings()
        {
            var settings = this.unitOfWork.GetRepository<Setting>()
                .Find(item => item.Key.StartsWith("DISCOGS"));

            var consumerKey = settings.Single(item => item.Key == Constants.DISCOGS_CONSUMER_KEY).Value;
            var consumerKeySecret = settings.Single(item => item.Key == Constants.DISCOGS_CONSUMER_SECRET).Value;

            var token = settings.SingleOrDefault(item => item.Key == Constants.DISCOGS_TOKEN)?.Value;
            var tokenSecret = settings.SingleOrDefault(item => item.Key == Constants.DISCOGS_TOKEN_SECRET)?.Value;

            var oAuthConsumerInformation = new OAuthConsumerInformation(consumerKey, consumerKeySecret);
            var oAuthTokenInformation = new OAuthTokenInformation(token, tokenSecret);

            return new OAuthCompleteInformation(oAuthConsumerInformation, oAuthTokenInformation);
        }

        public void AddDiscogsAuthenticationToken(OAuthTokenInformation tokenInformation)
        {
            this.AddSetting(new Setting()
            {
                Key = "DISCOGS_TOKEN",
                Value = tokenInformation.Token
            });
            this.AddSetting(new Setting()
            {
                Key = "DISCOGS_TOKEN_SECRET",
                Value = tokenInformation.TokenSecret
            });
        }
    }
}
