using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model;
using PandyIT.VinylOrganizer.DAL.Model.Entities;

namespace PandyIT.VinylOrganizer.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };           

            var ctx = new VinylOrganizerDbContext(builder.ToString());

            using (var uow = new UnitOfWork(ctx))
            {
                var businessCtx = new VinylOrganizerBusinessContext(uow);

                var music = new MusicTrack()
                {
                    Artist = "ola",
                    Title = "mundo"
                };

                businessCtx.AddMusicTrack(music);
            }
        }
    }
}
