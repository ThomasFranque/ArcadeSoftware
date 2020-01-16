using System.IO;

namespace ExternalSystemGames
{
    public struct GameInfo
    {
        public FileInfo ImageFile { get; }
        public FileInfo ExeFile { get; }
        public string Name { get; }
        public string Path { get; }
        public string Description { get; }


        public GameInfo(string name, string path, string description, FileInfo exe, FileInfo imgFile)
        {
            Name = name;
            Path = path;
            Description = description;
            ExeFile = exe;
            ImageFile = imgFile;
        }

        public override string ToString()
        {
            return $"{Name}\t{Path}";
        }
    }
}
