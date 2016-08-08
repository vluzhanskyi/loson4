using System;
using System.Collections.Generic;

namespace leson4
{
    public class Types
    {
        public class ResultedData
        {
            public ResultedData()
            {

            }
            public ResultedData(string key, string value, List<string> artistList )
            {
                Key = key;
                Value = value;
                ArtistsList = artistList;
            }
            public string Key { set; get; }
            public string Value { set; get; }
            public List<string> ArtistsList { set; get; }
        }

        public class User
        {
            public int Uid { set; get; }
            public int Sex { set; get; }
            public int FollowersCount { set; get; }
            public DateTime Birthday { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public List<Song> AudioList = new List<Song>();


            public class Song
            {
                public string Title { set; get; }
                public string Artist { set; get; }
            }
        }
    }
}
