using System;
using System.Diagnostics;
using System.IO;
using log4net;

namespace PandyIT.Core.Media
{
    public class FFmpegAdapter : IFFmpegAdapter
    {
        private readonly FileInfo ffmpegFileInfo;
        private ILog log;

        public FFmpegAdapter(string path, ILog log) : this (new FileInfo(path), log)
        {
        }

        public FFmpegAdapter(FileInfo ffmpegFileInfo, ILog log)
        {
            if (ffmpegFileInfo == null)
            {
                throw new NotImplementedException(nameof(ffmpegFileInfo));
            }

            if (!ffmpegFileInfo.Exists)
            {
                throw new FileNotFoundException(ffmpegFileInfo.FullName);
            }

            this.ffmpegFileInfo = ffmpegFileInfo;
            this.log = log;
        }

        public void ExtractMp3(FileInfo source, FileInfo target)
        {
            this.log.Info(string.Format("Extracting MP3 from file {0} -> {1}", source, target));

            var args = string.Format("-i \"{0}\" -vn \"{1}\"", source.FullName, target.FullName);

            var proc = new Process
            {
                StartInfo =
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = ffmpegFileInfo.FullName,
                    Arguments = args,
                    UseShellExecute = false,
                    //CreateNoWindow = true
                }
            };

            proc.Start();

            //string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
        }
    }
}
