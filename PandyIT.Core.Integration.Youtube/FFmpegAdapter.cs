namespace PandyIT.Core.Integration.Youtube
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using log4net;

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
            this.log.Info(string.Format("Extracting MP3: {0}", target.Name));

            var args = string.Format("-i \"{0}\" -vn \"{1}\" -y", source.FullName, target.FullName);

            var proc = new Process
            {
                StartInfo =
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    FileName = ffmpegFileInfo.FullName,
                    Arguments = args,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
        }
    }
}
