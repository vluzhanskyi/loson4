using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VkAPI;
using VkAPI.Wrappers.Friends;
using System.IO;
using System.Xml.Linq;

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
    public class VKLogic
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
            var result = doc.Root.Elements("uid").Select(e => e.Value).ToArray();
            XDocument doc2 = new XDocument();

            foreach (var friend in result)
            {
                
                XDocument friends = XDocument.Load("http://api.vk.com/method/users.get.xml?user_ids=" + friend + "&fields=first_name,last_name,bdate,followers_count,sex");
              
                foreach(XNode node in friends.Nodes())
                {
                   //save to one file
                }
               
            }

            doc2.Save("Base//base.xml");
            //var result = "https://api.vk.com/method/friends.get.xml?user_ids="+UserId+"&fields=bdate&v=5.53";

        }
     
        class User
        {
           public User(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string FirstName { set; get; }
            public string LastName { set; get; }
            public List<string> Groups { set; get; }
        } 
    }
}

