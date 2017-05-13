﻿using System;
using System.Data.SqlClient;
using System.Linq;
using PandyIt.VinylOrganizer.Labels;
using PandyIt.VinylOrganizer.Labels.Entities;
using PandyIT.Core.Repository;
using PandyIT.VinylOrganizer.BAL.Business;
using PandyIT.VinylOrganizer.DAL.Model;
using System.IO;
using DiscogsNet.Api;
using log4net;
using log4net.Config;
using PandyIT.Core.Media;
using PandyIT.VinylOrganizer.BAL.Business.Youtube;

namespace PandyIT.VinylOrganizer.ConsoleTests
{
    internal class Program
    {
        private const string FfmpegPath = @"C:\Program Files (x86)\FFmpeg\ffmpeg.exe";


        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(typeof(Program));

            var builder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "teste",
                DataSource = "(local)",
                IntegratedSecurity = true
            };

            var youtubeConfiguration = new YoutubeServiceConfiguration()
            {
                OutputFolder = new DirectoryInfo(@"C:\YoutubeAudio\output"),
                WorkingFolder = new DirectoryInfo(@"C:\YoutubeAudio\temp")
            };

            using (var ctx = new VinylOrganizerDbContext(builder.ToString(), VinylOrganizerSeeder.GetSeeder()))
            using (var uow = new UnitOfWork(ctx))
            {
                var discogs = new Discogs3("wTIlBQlrElaTrepxOBIw");
                var vinylOrganizerService = new VinylOrganizerService(uow, discogs);
                var youtubeService = new YoutubeService(
                    youtubeConfiguration, 
                    log, 
                    uow, 
                    new YoutubeDownloader(log, (a, ev) => Print(string.Format("\r{0}% Complete", (int)ev.ProgressPercentage))),
                    new FFmpegAdapter(FfmpegPath, log),
                    discogs);

                DownloadYoutubeAudio(youtubeService);
                //AddVinyls(vinylOrganizerService);
                //PrintLabelsByDiscogsIds(vinylOrganizerService);
                //PrintLabelsByName(businessCtx);
            }
        }

        private static void Print(string msg)
        {
            var oldLeft = Console.CursorLeft;
            var oldTop = Console.CursorTop;

            Console.Write(msg);

            Console.SetCursorPosition(oldLeft, oldTop);
        }

        private static void DownloadYoutubeAudio(YoutubeService youtubeService)
        {
            var discogsIdsToExtractAudio = new[]
            {
                1148256,
7154920,
480618,
2385650,
2750823,
3419668,
8564504,
156194,
639592,
1425260,
1817982,
6096889,
51292,
1155299,
897760,
4840284,
3873447,
4169243,
7860663,
7125000,
1687806,
990726,
7124824,
9069532,
9017573,
982779,
2700795,
3672380,
2210898,
6618486,
712582,
317007,
5460139,
8069632,
1531916,
2378887,
2675732,
8021945,
7443685,
2701287,
6323552,
426414,
2978285,
4234502,
8188597,
7617793,
844005,
3704568,
3400324,
759199,
2209030,
5001742,
2935002,
4357448,
6875989,
3966843,
8396543,
8394688,
4369372,
4052990,
968240,
1525918,
1700118,
982447,
4128086,
3004366,
8352255,
8564171,
193966,
5070787,
8360328,
66193,
4594164,
8636578,
962953,
1609815,
7641737,
3346259,
3309238,
7333798,
2531910,
2213882,
6075054,
6614355,
1125084,
183259,
91661,
1648940,
9367160,
2387965,
5536257,
76854,
1770046,
1632474,
6878272,
6949031,
3466253,
4855277,
6702329,
7644369,
3280889,
2465782,
2756,
8356271,
1910199,
3834073,
4000462,
3854644,
1382374,
6182536,
5489728,
4224246,
2123085,
2347901,
7270402,
2198704,
5201649,
2852266,
6489627,
1104945,
6050286,
6980917,
7129760,
7791076,
6951100,
939010,
59034,
2584241,
4206836,
6964755,
1345797,
2362734,
3030542,
182711,
3888076,
6977521,
1576404,
4138539,
65437,
3311393,
6910488,
3436560,
442396,
4350801,
1228791,
43040,
411196,
4412756,
1369100,
4031443,
1337672,
3793579,
4967398,
7605159,
8329834,
5067004,
8302598,
6592586,
4792223,
138202,
6275599,
137383,
4901478,
8673141,
1658502,
3603024,
3758075,
6973055,
7314622,
2260743,
5090979,
6572553,
2699193,
9145390,
1315169,
5437983,
5020527,
3559531,
2318737,
4254186,
1616728,
7025938,
1493829,
941394,
5540250,
2686848,
4992407,
6680559,
3710968,
4122046,
2241359,
2172257,
3801310,
5213984,
4431768,
9548704,
511669,
7058857,
2461319,
167131,
7678015,
6120589,
831696,
1828985,
5676076,
3516344,
7453373,
2203078,
3129042,
5986449,
7207999,
2928438,
60607,
7549666,
5954975,
7630076,
3733008

            };

            youtubeService.ExtractMp3(discogsIdsToExtractAudio);
        }

        private static void PrintLabelsByDiscogsIds(VinylOrganizerService businessCtx)
        {
            var labelPage = new LabelPage(5,2,827,1169);
            

            var discogsIdsToPrint = new[]
            {
                3945093,
                4358240,
                10201948,
                7221898,
                1109580
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


        private static void PrintLabelsByName(VinylOrganizerService businessCtx)
        {
            var labelPage = new LabelPage(5, 2, 827, 1169);

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

        private static void AddVinyls(VinylOrganizerService businessCtx)
        {
            var discogsIds = new []
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
               // 8536301,
               // 3966162,
               // 8169015,
               // 8804780,
               // 8753133,
               // 8204601,
               // 3581905,
               // 6162503,
               //5723310,
               // 8824586, //2016-0037 Playgroup ‎– Previously Unreleased
               // 2392955,
               // 4110065,
               // 8750390,
               // 3470298,
               // 8253709,
               // 2962241,
               // 8573528,
               // 8167935,
               // 7548627,
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
               // 8822802,
               // 9109288,
               // 8827856,
               // 8685547,
               // 6806463,
               // 4777458,
               // 344198,
               // 8593295,
               // 7136939,
               // 7253119,
               // 4740705,
               // 8194051,
               // 6792747,
               // 8950042,
               // 8426981,
               // 7810393,
               // 7772832,

               // 9001054,
               //9505509,
               // 8922328,
               // 9086134,
               // 8599831,
               // 7980777,
               // 8921037,
               // 4030069,
               // 8575138,
               // 8888892,

               // 8619476,
               // 8403967,
               // 8957456,
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
               // 8557092,
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
               //9304357,
               // 6162959,
               // 8763564,
               //845787,
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
               //9797252,
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
               // 9150143,
               // 285065,
               // 1082542,
               // 3295037,
               // 9986087,
               // 9975547,
               // 9888163,
               // 1127486,
               // 895369,
               // 491458,
               // 896306,
               // 9570574,
               // 6660733,
               // 5721517,
               // 5660407
               //7232399,
               // 7263677,
               // 4879973,
               // 5857432,
               // 9336919
               //72657,
               // 9151296,
               // 7576111,
                //1036091,
                //2757625,
                3945093,
                4358240,
                10201948,
                7221898,
                1109580
            };

            discogsIds.ToList().ForEach(id => businessCtx.AddDiscogsVinyl(id, 1));
        }
    }
}
