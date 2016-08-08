using System;
using System.Windows.Forms;
using leson4.Properties;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace leson4
{
    public partial class Form1 : Form
    {
        private string Token { get; set; }
        private string UserId { get; set; }
        public static int FriendCount { get; set; }

        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Auth();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            GetDataButton.IsAccessible = false;
            var l = new VkLogic();
            l.CollectLoginData(Token, UserId);
            progressBar1.Maximum = FriendCount;
            progressBar1.Step = FriendCount / 2;
            l.CollectData("Persons.xml");
            MessageBox.Show(Resources.Form1_LoginButton_Click_DataBase_created_);
            GetDataButton.IsAccessible = true;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            var l = new VkLogic();
            progressBar1.Maximum = FriendCount;
            progressBar1.Step = FriendCount / 2;
            var stat = l.CalculateStatistic(l.Deserialize("Persons.xml"));
            
            var serializer = new XmlSerializer(typeof(List<Types.ResultedData>));
            using (var fs = new FileStream("Result.xml", FileMode.OpenOrCreate))
            {              
                    serializer.Serialize(fs, stat);                                
            }
            var curDir = Directory.GetCurrentDirectory().Replace("\\", "/");
            webBrowser1.Url = new Uri(string.Format("file:///{0}/Result.xml", curDir));
            progressBar1.Value = progressBar1.Maximum;
            
        }

        public void Auth()
        {
            const string appId = "5578372";
            const string scope = "friends,audio,pages,wall,stats";
            var url = "https://oauth.vk.com/authorize?client_id=" + appId + "&display=popup&redirect_uri=http://api.vkontakte.ru/blank.html&scope=" + scope + "&response_type=token&v=5.53&state=123456";
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted_1(object sender, EventArgs e)
        {
            var uri = webBrowser1.Url.ToString();

            if (!uri.Contains("token")) 
                return;
            Token = uri.Split('=')[1].Split('&')[0];
            UserId = uri.Split('=')[3].Split('&')[0];
        }

        public static void IncreaseProgressValue()
        {
            if (progressBar1.Value != progressBar1.Maximum)
                progressBar1.Value += 1;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }              
      
    }

}
