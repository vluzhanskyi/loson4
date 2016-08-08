using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace leson4
{
    public class VkLogic
    {
        private string Token { set; get; }
        private string UserId { set; get; }

        public void CollectLoginData(string token, string userId)
        {
            Token = token;
            UserId = userId;
        }

        public void CollectData(string dbFileName)
        {
            var users = new List<Types.User>();

            var doc = XDocument.Load("http://api.vk.com/method/friends.get.xml?user_id=" + UserId);
            if (doc.Root == null) return;
            var result = doc.Root.Elements("uid").Select(e => e.Value).ToArray();

            foreach (var friend in result)
            {
                var f = XDocument.Load("http://api.vk.com/method/users.get.xml?user_ids=" + friend + "&fields=first_name,last_name,bdate,followers_count,sex");
                var user = new Types.User();

                if (f.Root != null && f.Root.Element("user") != null)
                {
                    var sex = 0;
                    var followersCount = 0;
                    var uid = 0;
                    var bDate = DateTime.MinValue;

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

                        if (xElement1 != null)
                            DateTime.TryParse(xElement.Value, out bDate);
                        if (bDate.Year < 1900)
                            bDate = new DateTime(1990, 6, 1, 7, 47, 0);
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
                    user.Birthday = bDate;
                    user.Sex = sex;
                    user.FollowersCount = followersCount;


                    var music = XDocument.Load("https://api.vk.com/method/audio.get.xml?need_user=0&access_token=" + Token + "&owner_id=" + friend);
                    foreach (var sound in music.Root.Elements("audio"))
                    {
                        var title = sound.Element("title");
                        var artist = sound.Element("artist");
                        if (title == null || artist == null) continue;
                        var track = new Types.User.Song
                        {
                            Artist = artist.Value,
                            Title = title.Value
                        };
                        user.AudioList.Add(track);
                    }
                }
                users.Add(user);
            }
            Serialize(users, dbFileName);
        }

        private static void Serialize(List<Types.User> users, string dbFileName)
        {
            var ser = new XmlSerializer(typeof(List<Types.User>));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            try
            {
                using (var fs = new FileStream(dbFileName, FileMode.OpenOrCreate))
                {
                    ser.Serialize(fs, users, namespaces);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public List<Types.ResultedData> CalculateStatistic(List<Types.User> friends)
        {
            if (friends ==null || friends.Count == 0) return null;
            var result = new List<Types.ResultedData>();
            
            // var Result = new Dictionary<string, string>();

            double? averageAge =
    (from friend in friends select friend.Birthday.Year).Average();
            var res = DateTime.Now.Year - averageAge.Value;
            int res0;
            int.TryParse(res.ToString(CultureInfo.CurrentCulture), out res0);
            var data0 = new Types.ResultedData("AvarageAge: ", 
                Math.Ceiling(DateTime.Now.Year - averageAge.Value).ToString(CultureInfo.CurrentCulture), null);
            result.Add(data0);
            //Result.Add("AvarageAge: ", Math.Ceiling(DateTime.Now.Year - averageAge.Value).ToString());

            double? averageFollowersCount =
    (from friend in friends select friend.FollowersCount).Average();
            //Result.Add("AvarageFollowersCount: ", Math.Ceiling(averageFollowersCount.Value).ToString());
            var data1 = new Types.ResultedData("AvarageFollowersCount: ",
                Math.Ceiling(averageFollowersCount.Value).ToString(CultureInfo.CurrentCulture), null);
            result.Add(data1);

            double? averageSex =
   (from friend in friends select friend.Sex).Average();

            //Result.Add("AvarageSex: ", Math.Ceiling(averageSex.Value).ToString());
            var data2 = new Types.ResultedData("AvarageSex: ", null, null)
            {
                Key = "AvarageSex: "
            };
            var sex = Math.Ceiling(averageSex.Value);
            data2.Value = Math.Abs(sex - 2) < 0 ? "Mans" : "Womans";
            result.Add(data2);

            //var temp = GetTopFiveSongs(friends);
            //Result.Add("Popular Artist: ", temp[0]);
            var data3 = new Types.ResultedData("Top 5 Artists: ", null,
                GetTopFiveSongs(friends));
            result.Add(data3);

            return result;
        }

        private static List<string> GetTopFiveSongs(IEnumerable<Types.User> friends)
        {
            var songs = friends.SelectMany(friend => friend.AudioList).ToList();

            var topFiveSongs = songs.GroupBy(q => q.Artist)
                                             .OrderByDescending(gp => gp.Count())
                                             .Take(5)
                                             .Select(g => g.Key).ToList();

            return topFiveSongs;
        }

        public List<Types.User> Deserialize(string dbfilename)
        {
            var serializer = new XmlSerializer(typeof(List<Types.User>));
            List<Types.User> users = null;
            var fs = new FileStream(dbfilename, FileMode.Open);
            var reader = XmlReader.Create(fs);

            try
            {              
                users = (List<Types.User>)serializer.Deserialize(reader);
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return users;  
        }
    }
}
