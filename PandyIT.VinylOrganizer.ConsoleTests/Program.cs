using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using PandyIt.VinylOrganizer.Labels;
using PandyIt.VinylOrganizer.Labels.Entities;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model;
using System.Diagnostics;

namespace PandyIT.VinylOrganizer.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };

            using (var ctx = new VinylOrganizerDbContext(builder.ToString(), VinylOrganizerSeeder.GetSeeder()))
            using (var uow = new UnitOfWork(ctx))
            {
                var businessCtx = new VinylOrganizerBusinessContext(uow);

                AddVinyls(businessCtx);
                PrintLabelsByDiscogsIds(businessCtx);
                //PrintLabelsByName(businessCtx);
            }
        }

        private static void PrintLabelsByDiscogsIds(VinylOrganizerBusinessContext businessCtx)
        {
            LabelPage labelPage = new LabelPage(5,2,827,1169);
            

            var discogsIdsToPrint = new[]
            {
                8132765,
                9109288,
                9210429,
                8066978
            };

            int i = 0;
            foreach (var discogsId in discogsIdsToPrint)
            {
                var column = i % 2;
                var row = i / 2;

                var vinyl = businessCtx.FetchVinylByDiscogsId(discogsId);
                labelPage.AddLabel(row, column, vinyl);
                i++;
            }

            var labelPages = new LabelPage[] { labelPage };
            var printer = new LabelPrinter(labelPages);
            printer.Print();
        }


        private static void PrintLabelsByName(VinylOrganizerBusinessContext businessCtx)
        {
            LabelPage labelPage = new LabelPage(5, 2, 827, 1169);

            var discogsIdsToPrint = new[]
            {
                "#2015-0010",
                "#2015-0011",
                "#2015-0012",
                "#2015-0014",
                "#2015-0015",
                "#2015-0016",
                "#2015-0017",
                "#2015-0018",
                "#2015-0019",
                "#2015-0020",
            };

            int i = 0;
            foreach (var discogsId in discogsIdsToPrint)
            {
                var column = i % 2;
                var row = i / 2;

                var vinyl = businessCtx.FetchVinylByName(discogsId);
                labelPage.AddLabel(row, column, vinyl);
                i++;
            }

            var labelPages = new LabelPage[] { labelPage };
            var printer = new LabelPrinter(labelPages);
            printer.Print();
        }

        private static void AddVinyls(VinylOrganizerBusinessContext businessCtx)
        {
            var discogsIds = new []
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
                9210429,
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
                5861692,
                801310,
                6185397,
                2938879,
                7391071,
                8633490,
                8604814,
                8563459,
                8102659,
                8105079,
                8132765,
                8536301,
                3966162,
                8169015,
                8804780,
                8753133,
                8204601,
                3581905,
                6162503,
                8824586, //2016-0037 Playgroup ‎– Previously Unreleased
                2392955,
                4110065,
                8750390,
                6106087,
                8253709,
                2962241,
                8573528,
                8167935,
                7548627,
                8376752,
                8879709,
                8168681,
                7078551,
                5933508,
                4606878,
                1943397,
                1959141,
                8475363,
                6198328,
                992211,
                1162074,
                3675247,
                6336348,
                7609847,
                8774166,
                8802382,
                8822802,
                9109288,
                8827856,
                8685547,
                6806463,
                4777458,
                344198,
                8593295,
                7136939,
                7253119,
                4740705,
                8194051,
                6792747,
                8950042,
                8426981,
                7810393,
                7772832,

                9001054,
                8862937,
                8922328,
                9086134,
                8599831,
                7980777,
                8921037,
                4030069,
                8575138,
                8888892,

                8619476,
                8403967,
                8957456,
                8989473,
                8983517,
                6292436,
                9012825,
                8519868,
                8503836,
                8206206,
                3127874,
                8071773,
                7379294,
                6203571,
                208803,
                796259,
                571985,
                6379187,
                8557092,
                9122000,
                7737710,
                8102914,
                9131093,
                6393317,
                8066978
            };

            discogsIds.ToList().ForEach(id => businessCtx.AddDiscogsVinyl(id, 1));
        }
    }
}
