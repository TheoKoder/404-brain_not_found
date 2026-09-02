using System;
using System.Collections.Generic;

namespace loginForm_1
{
    [Serializable]
    public class PlaylistModel
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string CoverImagePath { get; set; }
        public List<Song> Songs { get; set; }

        // Automatically updates track count as songs are added or removed
        public int TrackCount => Songs?.Count ?? 0;

        public PlaylistModel()
        {
            Songs = new List<Song>();
            CreationDate = DateTime.Now;
        }

        public PlaylistModel(string title, string coverImagePath = "")
        {
            Title = title;
            CreationDate = DateTime.Now;
            CoverImagePath = coverImagePath;
            Songs = new List<Song>();
        }
    }
}