using System.IO;

namespace PandyIT.Core.Media
{
    public interface IFFmpegAdapter
    {
        void ExtractMp3(FileInfo source, FileInfo target);
    }
}