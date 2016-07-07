﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PandyIT.VinylOrganizer.DAL.Model;
using System.Data.SqlClient;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.Core.Repository;
using System.Data.Entity;

namespace PandyIT.VinylOrganizer.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public VinylOrganizerDbContext VinylOrganizerDbContext { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public IVinylOrganizerBusinessContext VinylOrganizerBusinessContext { get; set; }

        public App()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };

            this.VinylOrganizerDbContext = new VinylOrganizerDbContext(builder.ToString(),
                VinylOrganizerSeeder.GetSeeder());
            this.UnitOfWork = new UnitOfWork(this.VinylOrganizerDbContext);
            this.VinylOrganizerBusinessContext = new VinylOrganizerBusinessContext(this.UnitOfWork);

            this.VinylOrganizerDbContext.Locations.Load();

            //this.AddVinyls();
        }



        private void AddVinyls()
        {
            var discogsIds = new[]
            {
                95381,
                737048,
                1004065,
                1337963,
                1625438,
                2362734,
                2465782,
                3111838,
                3263605,
                2837493,
                2766862,
                3366766,
                3395216,
                3781912,
                3403057,
                4463218,
                4471236,
                6205556,
                6196238,
                5817158,
                5637725,
                6003837,
                6149924,
                5750416,
                6363415,
                7741760,
                7679367,
                7737829,
                7855876,
                7458388,
                7238127,
                6678410,
                7829477,
                7663462,
                7873378,
                6839871,
                6946862,
                7537515,
                7154920,
                7828040,
                7796584,
                7802121,
                7124775,
                7507298,
                7597170,
                7230130,
                7618341,
                7384779,
                7573341,
                7235634,
                7771988,
                7741831,
                7490374,
                7361557,
                7458228,
                7780348,
                6601399,
                7418415,
                6609001,
                7379633,
                7618864,
                8062138,
                7750876,
                8028534,
                8130037,
                8031514,
                8133911,
                8030681,
                8100827,
                8183355,
                8012345,
                8120919,
                8253276,
                8329965,
                8285974,
                8036642,
                8195881,
                8271708,
                8200706,
                8153028,
                8224037,
                325623,
                8309467,
                8166659,
                8092716,
                8186293,
                8501806,
                4242198,
                3018053,
                2988525,
                5003156,
                6945937,
                5861692
            };

            discogsIds.ToList().ForEach(VinylOrganizerBusinessContext.AddDiscogsVinyl);
        }

    }
}