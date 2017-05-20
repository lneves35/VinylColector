namespace PandyIT.Core.Integration.Youtube
{
    using System.IO;

    public interface IFFmpegAdapter
    {
        void ExtractMp3(FileInfo source, FileInfo target);
    }
}