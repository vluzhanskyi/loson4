using ApiCore;
using MediaBrowser.Controller.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkAPI;
using VkAPI.Wrappers.Friends;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

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
        //FriendsGetParams Params = new FriendsGetParams();
        string Email { set; get; } 
        string Password { set; get; }
       private Dictionary<string, string> LogIn(string email, string password)
        {
            Settings settings = Settings.All;
            int appId = 5555068;
            VkApi api = new VkApi();
            string par3;

           // ApiAuthParams AuthParams = new ApiAuthParams();
            var Luzh = new Dictionary<string, string>();
            api.Authorize(appId, email, password, settings);
            var par = new FriendsGetParams();

            par.Fields = ProfileFields.FriendLists;
            var AccInfo = api.Friends.Get(par);
            
            foreach (var par1 in AccInfo)
            {
                var par2 = par1.FriendLists;
                Luzh.Add(par1.Id.ToString(), par1.LastName);
                if (par2 != null)
                    par3 = par2.ToString();
            }
         
            return Luzh;
        }

        public void CollectLoginData(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public void SaveData()
        {
            var users = new List<User>();

            Dictionary<string, string> friends = LogIn(Email, Password);

            foreach (var item in friends)
            {
                User user = new User(item.Value, item.Key);
                //Add groups here
                users.Add(user);
            }
            
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

