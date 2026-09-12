using System;
using System.Collections.Generic;

namespace loginForm_1
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<PlaylistModel> Playlists { get; set; }

        public User()
        {
            Playlists = new List<PlaylistModel>();
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Playlists = new List<PlaylistModel>();
        }
    }
}
