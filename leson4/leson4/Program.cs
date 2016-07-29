using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VkAPI;
using VkAPI.Wrappers.Friends;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace leson4
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());      
        }
    }   
    public class VkLogic
    {
        string Token { set; get; } 
        string UserId { set; get; }

        public void CollectLoginData(string token, string userId)
        {
            Token = token;
            UserId = userId;
        }

        public void SaveData()
        {
            var users = new List<User>();
            string response = String.Empty;

            XDocument doc = XDocument.Load("http://api.vk.com/method/friends.get.xml?user_id=" + UserId);
            if (doc.Root != null)
            {
                var result = doc.Root.Elements("uid").Select(e => e.Value).ToArray();

                foreach (var friend in result)
                {                  
                   // XmlTextReader friendXml = new XmlTextReader("http://api.vk.com/method/users.get.xml?user_ids=" + friend + "&fields=first_name,last_name,bdate,followers_count,sex");
                    XDocument f = XDocument.Load("http://api.vk.com/method/users.get.xml?user_ids=" + friend + "&fields=first_name,last_name,bdate,followers_count,sex");
                     var user = new User();

                     if (f.Root != null && f.Root.Element("user") != null)
                    {
                        int sex = 0;
                        int followersCount = 0;
                        int uid = 0;
                        long bdate = 0;

                        var o3 = f.Root.Element("user");
                        if (o3 != null)
                        {
                            var element3 = o3.Element("uid");
                            if (element3 != null) 
                                int.TryParse(element3.Value, out uid);
                        }
                        user.Uid = uid;
                        
                        var o2 = f.Root.Element("user");
                        if (o2 != null)
                        {
                            var xElement3 = o2.Element("sex");
                            if (xElement3 != null) int.TryParse(xElement3.Value, out sex);
                        }
                        var xElement = f.Root.Element("user");
                        if (xElement != null)
                        {
                            var element = xElement.Element("followers_count");
                            if (element != null)
                                int.TryParse(element.Value, out followersCount);
                        }
                        var o = f.Root.Element("user");
                        if (o != null)
                        {
                            var xElement1 = o.Element("bdate");
                            if (xElement1 != null) long.TryParse(xElement1.Value, out bdate);
                        }
                        var element1 = f.Root.Element("user");
                        if (element1 != null)
                        {
                            var o1 = element1.Element("first_name");
                            if (o1 != null)
                                user.FirstName = o1.Value;
                        }
                        var xElement2 = f.Root.Element("user");
                        if (xElement2 != null)
                        {
                            var element2 = xElement2.Element("last_name");
                            if (element2 != null)
                                user.LastName = element2.Value;
                        }
                        user.Sex = sex;
                        user.FollowersCount = followersCount;
                        user.Birthday = bdate;

                        XDocument music = XDocument.Load("https://api.vk.com/method/audio.get.xml?need_user=0&access_token=" + Token + "&owner_id=" + friend);
                        foreach (var sound in music.Root.Elements("audio"))
                        {
                            var title = sound.Element("title");
                            var artist = sound.Element("artist");
                           if (title != null && artist != null)
                            {
                                user.AudioList.Add(new User.Song(title.Value, artist.Value));
                            }
                        }
                    }
                   
                    users.Add(user);                
                }
            }
            XmlSerializer writer = new XmlSerializer(typeof(User));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            
            namespaces.Add(string.Empty, string.Empty);
            
            using (FileStream fs = new FileStream("Persons.xml", FileMode.OpenOrCreate))
            {               
                foreach (var user in users)
                    writer.Serialize(fs, user, namespaces);
            }
           
        }
     
        public class User
        {
            public int Uid { set; get; }
            public int Sex { set; get; }
            public int FollowersCount { set; get; }
            public long Birthday { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public List<Song> AudioList = new List<Song>();
            

            public class Song
            {
                public Song(string tittle, string artist)
                {
                    Title = tittle;
                    Artist = artist;
                }

                public string Title { set; get; }
                public string Artist { set; get; }
            }
        } 
    }
}

