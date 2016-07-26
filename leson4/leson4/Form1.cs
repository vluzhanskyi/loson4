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

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            GetDataButton.IsAccessible = false;
            VKLogic l = new VKLogic();
            l.CollectLoginData(Token, UserId);
            l.SaveData();
            MessageBox.Show("DataBase created!");
            GetDataButton.IsAccessible = true;
        }

        public void Auth()
        {
            string appId = "5563664";
            string Scope = "2";
            string url = "https://oauth.vk.com/authorize?client_id=" + appId + "&display=popup&redirect_uri=http://api.vkontakte.ru/blank.html&scope=" + Scope + "&response_type=token&v=5.53&state=123456";
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string uri = webBrowser1.Url.ToString();
            Token = uri.Split('=')[1].Split('&')[0];
            UserId = uri.Split('=')[3].Split('&')[0];


        }
    }
}
