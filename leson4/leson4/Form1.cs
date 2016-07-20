using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiCore;

namespace leson4
{
    public partial class Form1 : Form
    {
        private ApiCore.SessionInfo sessionInfo;
        private ApiCore.ApiManager manager;
        private bool isLoggedIn = false;
        private List<Friend> friendList;
        private FriendsFactory friendFactory;
        private Regex userIdCheck;

        public Form1()
        {
            InitializeComponent();
        }

        private void Reauth()
        {
            if (!this.isLoggedIn)
            {
                SessionManager sm = new SessionManager(5555068, Convert.ToInt32(ApiPerms.Audio | ApiPerms.ExtendedMessages | ApiPerms.ExtendedWall | ApiPerms.Friends | ApiPerms.Offers | ApiPerms.Photos | ApiPerms.Questions | ApiPerms.SendNotify | ApiPerms.SidebarLink | ApiPerms.UserNotes | ApiPerms.UserStatus | ApiPerms.Video | ApiPerms.WallPublisher | ApiPerms.Wiki));
                this.sessionInfo = sm.GetSession(SessionAuthType.WithBrowser);
                if (this.sessionInfo != null)
                {
                    this.isLoggedIn = true;
                }
            }

            if (this.isLoggedIn)
            {
                manager = new ApiManager(this.sessionInfo);
                manager.Timeout = 10000;
                this.listBox1.Enabled = true;
                this.friendFactory = new FriendsFactory(this.manager);
                this.userIdCheck = new Regex("([\\d])+$");
                this.GetFriendList();
            }
        }

        //Загрузка списка друзей и запись данных в виджет listBox
        private void GetFriendList()
        {
            try
            {
                this.listBox1.Items.Clear();
                this.friendList = this.friendFactory.Get(FriendNameCase.Nominative, null, null, null, new string[] { "uid", "first_name", "nickname", "last_name" });
                for (int i = 0; i < this.friendList.Count; i++)
                {
                    this.listBox1.Items.Add(this.friendList[i]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading friend list:\n" + e.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Вызов функций
            this.Reauth();
            this.GetFriendList();
        }
    }
}
