using System;
using System.Collections.Generic;
using System.Data.Common;
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
    class Program
    {
        static void Main(string[] args)
        {
            string startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string datalogicFilePath = Path.Combine(startupPath, "Datalogic.sdf");
            string connectionString = string.Format("DataSource={0}", datalogicFilePath);

            var ctx = new VinylOrganizerDbContext(connectionString);
            var uow = new UnitOfWork(ctx);
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
