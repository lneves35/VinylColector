using System.Windows;
using PandyIT.VinylOrganizer.DAL.Model;
using System.Data.SqlClient;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.Core.Repository;
using System.Data.Entity;
using log4net;
using log4net.Config;

namespace PandyIT.VinylOrganizer.UI.WPF
{
    public partial class App : Application
    {
        public VinylOrganizerDbContext VinylOrganizerDbContext { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public IVinylOrganizerService VinylOrganizerService { get; set; }

        public ILog log { get; set; }

        public App()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };

            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(App));

            this.VinylOrganizerDbContext = new VinylOrganizerDbContext(builder.ToString(),
                VinylOrganizerSeeder.GetSeeder());
            this.UnitOfWork = new UnitOfWork(this.VinylOrganizerDbContext);
            //this.VinylOrganizerService = new VinylOrganizerService(this.UnitOfWork, new DiscogsAdapter("qXdqQlDKAhFpWYTcTgVIDmvehYahJBvAqZvNbiHF", null));

            this.VinylOrganizerDbContext.Locations.Load();
        }
    }
}
