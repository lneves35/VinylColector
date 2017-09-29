using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using PandyIT.Core.FileSystem;
using PandyIT.Core.Text;

namespace PandyIT.VinylOrganizer.Services
{
    public class DuplicateDetectionService
    {
        private readonly ILog log;

        public DuplicateDetectionService(ILog log)
        {
            this.log = log;
        }

        public IEnumerable<Similarity> Run(string path)
        {
            return this.Run(new [] { path });
        }

        public IEnumerable<Similarity> Run(IEnumerable<string> paths)
        {
            this.log.Info("Getting files...");

            var musicFiles = paths.SelectMany(p => FileUtils.GetFiles(p, "*.aiff|*.aif|*.mp3", SearchOption.AllDirectories)).ToArray();
            this.log.Info("Total files: " + musicFiles.Length);

            var similarities = GetCombinations(musicFiles);
            comb = similarities.Count;
            this.log.Info("Total combinations: " + similarities.Count);

            similarities.ForEach(RunFilenameLevenshtein);

            return similarities;
        }

        private static List<Similarity> GetCombinations(IReadOnlyList<string> musicFiles)
        {
            var similarities = new List<Similarity>();
            for (var i = 0; i < musicFiles.Count; i++)
            {
                for (var j = i + 1; j < musicFiles.Count; j++)
                {
                    var file1 = musicFiles[i];
                    var file2 = musicFiles[j];

                    similarities.Add(new Similarity() {File1 = file1, File2 = file2});
                }
            }
            return similarities;
        }

        private int lev = 0;
        private int comb = 0;
        private void RunFilenameLevenshtein(Similarity similarity)
        {
            lev++;
            this.log.Info(lev + "/" + comb);

            var file1 = similarity.File1;
            var file2 = similarity.File2;
            int result;

            if ((file1 + file2).ToLower().Contains("untitled"))
            {
                result = -1;
            }
            else
            {
                result = TextUtils.Levenshtein(file1, file2, true);
            }
            
            similarity.SimilarityFilename = result;
            
        }

        public class Similarity
        {
            public string File1 { get; set; }

            public string File2 { get; set; }

            public int SimilarityFilename { get; set; }
        }
    }
}
