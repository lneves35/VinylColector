using System;
using System.Data.SqlClient;
using System.Linq;
using PandyIt.VinylOrganizer.Labels;
using PandyIt.VinylOrganizer.Labels.Entities;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model;
using System.IO;
using System.Threading;
using log4net;
using log4net.Config;
using PandyIT.Core.Integration.Discogs;
using PandyIT.Core.Integration.Youtube;
using PandyIT.VinylOrganizer.BAL.Business.Discogs;
using PandyIT.VinylOrganizer.Services;
using PandyIT.VinylOrganizer.Services.Extractors;
using PandyIT.VinylOrganizer.Services.Harvesters;

namespace PandyIT.VinylOrganizer.ConsoleTests
{
    internal class Program
    {
        private static SqlConnectionStringBuilder sqlConnectionStringBuilder;
        private static ILog log;
        private static IDiscogsAdapter discogsAdapter;
        private static IYoutubeAdapter youtubeAdapter;
        private static IYoutubeHarvester youtubeHarvester;


        private const string FfmpegPath = @"C:\Program Files (x86)\FFmpeg\ffmpeg.exe";

        private static void Bootstrap()
        {
            BootstrapLogger();
            BootstrapConnectionString();
            BootstrapDiscogs();
            BootstrapYoutube();
            youtubeHarvester = new YoutubeHarvester(youtubeAdapter, log);
        }

        private static void BootstrapConnectionString()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };
        }

        private static void BootstrapLogger()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Program));
        }

        private static void BootstrapDiscogs()
        {
            using (var ctx = new VinylOrganizerDbContext(sqlConnectionStringBuilder.ToString(), VinylOrganizerSeeder.GetSeeder()))
            using (var uow = new UnitOfWork(ctx))
            {
                var settingsService = new SettingsService(uow, log);
                var oAuth = settingsService.FetchDiscogsAuthenticationSettings();

                if (oAuth.TokenInformation.Token == null)
                {
                    oAuth = DiscogsAdapter.Authenticate(Console.ReadLine, oAuth.ConsumerInformation);
                    settingsService.AddDiscogsAuthenticationToken(oAuth.TokenInformation);
                }

                discogsAdapter = new DiscogsAdapter(oAuth, log);
            }
        }

        private static void BootstrapYoutube()
        {
            var youtubeConfiguration = new YoutubeServiceConfiguration()
            {
                OutputFolder = new DirectoryInfo(@"C:\YoutubeAudio\output"),
                WorkingFolder = new DirectoryInfo(@"C:\YoutubeAudio\temp"),
                ApplicationName = "Vinyl Organizer",
                FFMpegPath = FfmpegPath,
                GoogleAPIKey = "AIzaSyCb_j_yNGwAuQqKpPgWt-XSsm8eEhmDhYQ"
            };

            youtubeAdapter = new YoutubeAdapter(youtubeConfiguration, log);
        }

        private static void Main(string[] args)
        {
            Bootstrap();            

            using (var ctx = new VinylOrganizerDbContext(sqlConnectionStringBuilder.ToString(), VinylOrganizerSeeder.GetSeeder()))
            using (var uow = new UnitOfWork(ctx))
            {         
                
                var vinylOrganizerService = new VinylOrganizerService(uow, discogsAdapter, log);
                var harvestingService = new HarvestingService(uow, youtubeHarvester, log);

                AddVinyls(vinylOrganizerService);
                PrintLabelsByDiscogsIds(vinylOrganizerService);
                //ConsolePrintTopSimilarities(20);

                //youtubeAdapter.ExtractMp3(new Uri("https://www.youtube.com/watch?v=W8C6Wh_qXFM"));
                //harvestingService.HarvestTracklists(new Uri("https://www.mixesdb.com/w/Category:Manfredas"));
            }
        }

        private static void ConsolePrintTopSimilarities(int limit)
        {
            var duplicateDetectionService = new DuplicateDetectionService(log);
            var similarities = duplicateDetectionService.Run(@"\\LNEVES-MYCLOUD\Public\Music\ORGANIZED\SINGLES\_PLAYLIST");

            var topSimilarities = similarities.OrderBy(s => s.SimilarityFilename).Where(s => s.SimilarityFilename >= 0).Take(limit).ToList();

            topSimilarities.ForEach(s =>
            {
                var text = string.Format("File1: {0} File2: {1} FilenameLev: {2}", s.File1, s.File2, s.SimilarityFilename);
                log.Info(text);
            });
        }

        private static void PrintLabelsByDiscogsIds(VinylOrganizerService businessCtx)
        {
            var labelPage = new LabelPage(5,2,827,1169);
            

            var discogsIdsToPrint = new[]
            {
                12915338,
                11981854
            };

            var i = 0;
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

        private static void AddVinyls(VinylOrganizerService businessCtx)
        {
            var discogsIds = new[]
            {
               // 95381,
               // 737048,
               // 1004065,
               // 1337963,
               // 1625438,
               // 2362734,
               // 2465782,
               // 3111838,
               // 3263605,
               // 2837493,
               // 2766862,
               // 3366766,
               // 3395216,
               // 3781912,
               // 3403057,
               // 4463218,
               // 4471236,
               // 6205556,
               // 6196238,
               // 5817158,
               // 5637725,
               // 6003837,
               // 6149924,
               // 5750416,
               // 6363415,
               // 7741760,
               // 7679367,
               // 7737829,
               // 7855876,
               // 6810566,
               // 7238127,
               // 6678410,
               // 7829477,
               // 7663462,
               // 7873378,
               // 6839871,
               // 6946862,
               // 7537515,
               // 7154920,
               // 7828040,
               // 7796584,
               // 7802121,
               // 7124775,
               // 7507298,
               // 7597170,
               // 7230130,
               // 7314600,
               // 7384779,
               // 7573341,
               // 7235634,
               // 7771988,
               // 7741831,
               // 7490374,
               // 7361557,
               // 7458228,
               // 7780348,
               // 6601399,
               // 7418415,
               // 6609001,
               // 7379633,
               // 7618864,
               // 8062138,
               // 7750876,
               // 8028534,
               // 8130037,
               // 8031514,
               // 8133911,
               // 8030681,
               // 8100827,
               // 9210429,
               // 8012345,
               // 8120919,
               // 8253276,
               // 8329965,
               // 8285974,
               // 8036642,
               // 8617423,
               // 8271708,
               // 8200706,
               // 8153028,
               // 8224037,
               // 325623,
               // 8309467,
               // 8166659,
               // 8092716,
               // 8186293,
               // 8501806,
               // 4242198,
               // 3018053,
               // 2988525,
               // 5003156,
               // 6945937,
               // 5861692,
               // 801310,
               // 6185397,
               // 2938879,
               // 7391071,
               // 8633490,
               // 8604814,
               // 8563459,
               // 8102659,
               // 8105079,
               // 8132765,
               // 3966162,
               // 8169015,
               // 8804780,
               // 8753133,
               // 8204601,
               // 3581905,
               // 6162503,
               // 5723310,
               // 8536301,
               // 8824586,
               // 2392955,
               // 4110065,
               // 8750390,
               // 3470298,
               // 8253709,
               // 2768615,
               // 8573528,
               // 8167935,
               // 7158628,
               // 8376752,
               // 8879709,
               // 8168681,
               // 7078551,
               // 5933508,
               // 4606878,
               // 1943397,
               // 1959141,
               // 8475363,
               // 6198328,
               // 992211,
               // 1162074,
               // 3675247,
               // 6336348,
               // 7609847,
               // 8774166,
               // 8802382,
               // 9548704,
               // 9109288,
               // 8827856,
               // 7644369,
               // 6806463,
               // 4777458,
               // 344198,
               // 8593295,
               // 7136939,
               // 7253119,
               // 4740705,
               // 9299215,
               // 6792747,
               // 8950042,
               // 8426981,
               // 7810393,
               // 7772832,
               // 9001054,
               // 9505509,
               // 8922328,
               // 9086134,
               // 8599831,
               // 7980777,
               // 8921037,
               // 4030069,
               // 8319009,
               // 8888892,
               // 8619476,
               // 8403967,
               // 8387380,
               // 8989473,
               // 8983517,
               // 6292436,
               // 9012825,
               // 8519868,
               // 8503836,
               // 8206206,
               // 3127874,
               // 8071773,
               // 7379294,
               // 6203571,
               // 208803,
               // 796259,
               // 571985,
               // 6379187,
               // 10298217,
               // 9122000,
               // 7737710,
               // 8102914,
               // 9131093,
               // 6393317,
               // 8066978,
               // 742094,
               //3129495,
               //918915,
               //221702,
               //125905,
               //27332,
               //1104945,
               //5988529,
               //4484490,
               //5593595,
               //8053962,
               // 605935,
               // 9288739,
               // 9155278,
               // 9013931,
               // 6755333,
               // 9304357,
               // 6162959,
               // 8763564,
               // 845787,
               // 617743,
               // 6552895,
               // 959848,
               // 648796,
               // 7575662,
               // 9318864,
               // 5204059,
               // 7261405,
               // 9239224,
               // 6440250,
               // 9299432,
               // 9325793,
               // 8714888,


               // 9340038,
               // 9380830,
               // 6782771,
               // 8774433,
               // 7659645,
               // 8240291,
               // 8347782,
               // 7815837,
               // 9512930,
               // 9509710,
               // 9509029,
               // 8723761,
               // 7976836,
               // 1752522,
               // 4064530,
               // 9230648,
               // 8778623, //Kerri Chandler
               // 6584439,
               // 8563649,
               // 8989470,
               // 9680607,
               // 9152893,
               // 9390116,
               //9596296,
               //9707780,
               //9835826,
               //9810577,
               //10368300,
               //5266586,
               //8263705,
               //9875358,
               // 9876494,
               // 9724741,
               // 9830481,
               // 5877431,
               // 7796480,
               // 9846917,
               // 9661301,
               // 9910724,
               // 10488706,
               // 9150143,
               // 285065,
               // 1082542,
               // 3295037,
               // 4895692,
               // 9975547,
               // 9888163,
               // 1127486,
               // 895369,
               // 491458,
               // 896306,
               // 9570574,
               // 6660733,
               // 5721517,
               // 5660407,
               // 7232399,
               // 7263677,
               // 4879973,
               // 5857432,
               // 9336919,
               // 72657,
               // 9151296,
               // 7576111,
               // 1036091,
               // 2757625,
               // 3945093,
               // 4358240,
               // 10201948,
               // 7221898,
               // 1109580,
               // 10177079,
               // 10163275,
               // 10464522,
               // 9062623,
               // 9062740,
               // 5986449,
               // 606649,
               // 10306595,
               // 10336086,
               // 10301306,
               // 10295877,
               // 8386406,
               // 6316290,
               // 2364992,
               // 8402559,
               // 9260973,
               // 4328172,
               // 10182238,
               // 10242315,
               // 5206084,
               // 2995622,
               // 54157,
               // 10672148,
               // 10023295,
               // 11010731,
               // 10566832,
               // 10707971,
               // 9358185,
               // 9233033,
               // 6729678,
               // 8188597,
               // 10126426,
               // 10800414,
               // 10806157,
               // 1141517,
               // 761677,
               // 1370626,
               // 551046,
               // 10721758,
               // 10588866,
               // 10457483,
               // 9692106,
               // 10748750,
               // 10681630,
               // 10801821,
               // 10826439,
               // 11017400,
               // 10576064,
               // 10835418,
               // 85206,
               // 10879560,
               // 10910904,
               // 11073028,
               // 10872633,
               // 11079678
               // 11112707,
               // 10398088,
               // 11025888,
               // 11102391,
               // 11112118,
               // 11145265,
               // 11180848,
               // 11099534,
               // 11148835,

               // 10959237,
               // 11184641,
               // 11283030,
               // 10094549,
               // 11245580,
               // 11138834,
               // 11126482,
               // 9718837,
               // 11154492,
               // 11257548,
               // 11185498,
               // 10607972,
               // 11379530,
               // 11438210,
               // 6699030,
               // 7557449,
               // 11467146,
               // 11512514,
               // 11494385,
               // 4943254,
               // 5480486,
               // 11560777,
               // 11450669,
               // 11542470,

               // 3548854,
               // 9655414,
               // 11659502,
               // 11049957,
               // 11637845,
               // 11627593,
               // 11526450,
               // 11468500,
               // 11568211,

               // 11720137,
               // 11307852,

               // 11713675,
               // 11713584,
               // 11518754,
               // 11696866,
               // 11627247,
               // 4711471,
               // 11769110,

               // 11142340,
               // 920263,
               // 750708,
               // 1003603,
               // 1123617,
               // 894148,
               // 974967
               //11432960,
               //11822954,
               //11895762,
               // 11810704,
               // 11892468,
               // 7263654,
               // 11520026,
               // 11627623,
               // 11895536,
               // 11886948,
               // 11897378,
               // 11895770,
               //11907854,
               //11954236,
               //11779809,
               //39469,
               // 679899,
               // 755372,
               // 677889,
               //11972911,
               // 11968377,
               // 11740322,
               //7458388,
               // 12044061,
               // 12030790,
               // 11991303,
               // 11955706,
               //65130,
               // 7270402,
               // 11857456,
               // 10890370,
               // 11766345,
               // 11845386,
               // 12156376,
               // 12121657,
                //21455,
                //11530855,
                //6271578,
                //8132712,
                //12155002,
                //10305749,
                //10298535,
                //81968,
                //8569023,
                //12040177,
                //12246103,

                //11874884,
                //12480901,

                //229742,
                //104698,
                //313996,
                //1005844,
                //581169,
                //11128791,
                //1084305,
                //652764,

                //12503527,
                //12242457,
                //12490815,
                //12234193,

                //12097296,
                //12295987,
                //12451290,

                //7704301,
                //12573176

                //11265395,
                //12641120,
                //12619591,

                //6973055,
                //12040730,
                //12839960,
                //12804062,
                //8228811,

                //7768374,
                //8006431,
                //12872150,
                //12802097,
                //12894905,

                12915338,
                11981854
            };


            var i = 0;
            discogsIds.ToList()
                .ForEach(id =>
                {
                    i++;
                    Console.WriteLine(i);
                    if (i % 55 == 0)
                    {
                        var secs = 70;
                        Console.WriteLine($"Waiting {secs} seconds");
                        Thread.Sleep(secs * 1000);
                    }

                    businessCtx.AddDiscogsVinyl(id, 1);
                });
        }
    }
}
