using System;

using System.Windows.Forms;
using leson4.Properties;
using System.Collections.Generic;

namespace leson4
{
    public partial class Form1 : Form
    {
        private string Token { get; set; }
        private string UserId { get; set; }
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
            VkLogic l = new VkLogic();
            l.CollectLoginData(Token, UserId);
            l.CollectData("Persons.xml");

            MessageBox.Show(Resources.Form1_LoginButton_Click_DataBase_created_);
            GetDataButton.IsAccessible = true;
        }
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            VkLogic l = new VkLogic();
            Dictionary<string, string> stat = l.CalculateStatistic(l.Deserialize("Persons.xml"));
            int i = 0;
            
            foreach (var item in stat)
            {
                //richTextBox1.Text.Insert(i, );

                MessageBox.Show(item.Key + item.Value);
                i++;
            }
        }
        public void Auth()
        {
            string appId = "5563664";
            string Scope = "friends,audio,pages,wall,stats";
            string url = "https://oauth.vk.com/authorize?client_id=" + appId + "&display=popup&redirect_uri=http://api.vkontakte.ru/blank.html&scope=" + Scope + "&response_type=token&v=5.53&state=123456";
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string uri = webBrowser1.Url.ToString();
            Token = uri.Split('=')[1].Split('&')[0];
            UserId = uri.Split('=')[3].Split('&')[0];
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
