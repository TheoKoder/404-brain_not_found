using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization; // Added for JSON attributes

namespace loginForm_1
{
    public class PlaylistModel
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string CoverImagePath { get; set; }

        // Ignore raw System.Drawing.Image during JSON export to prevent crashes
        [JsonIgnore]
        public Image CoverImage { get; set; }

        public List<Song> Songs { get; set; } = new List<Song>();

        // Automatically updates track count as songs are added or removed
        public int TrackCount => Songs?.Count ?? 0;

        public PlaylistModel()
        {
            Songs = new List<Song>();
            CreationDate = DateTime.Now;
        }

        public PlaylistModel(string title) : this(title, null, "")
        {
        }

        public PlaylistModel(string title, Image coverImage, string coverImagePath = "")
        {
            Title = title;
            CreationDate = DateTime.Now;
            CoverImagePath = coverImagePath;
            CoverImage = coverImage;
            Songs = new List<Song>();
        }
    }
}