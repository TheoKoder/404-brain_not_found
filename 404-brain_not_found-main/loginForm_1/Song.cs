using System;

namespace loginForm_1
{
    [Serializable]
    public class Song
    {
        // Required song attributes
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string FilePath { get; set; } // Path to audio file on system

        public Song() { }

        public Song(string name, string artist, string album, string genre, string filePath = "")
        {
            Name = name;
            Artist = artist;
            Album = album;
            Genre = genre;
            FilePath = filePath;
        }

        public override string ToString()
        {
            return $"{Name} - {Artist} ({Album})";
        }
    }
}