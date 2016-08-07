using System;

using System.Windows.Forms;
using leson4.Properties;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

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
            List<Types.ResultedData> stat = l.CalculateStatistic(l.Deserialize("Persons.xml"));
            
            XmlSerializer serializer = new XmlSerializer(typeof(List<Types.ResultedData>));
            using (FileStream fs = new FileStream("Result.xml", FileMode.OpenOrCreate))
            {              
                    serializer.Serialize(fs, stat);                                
            }
            string curDir = Directory.GetCurrentDirectory().Replace("\\", "/");
            webBrowser1.Url = new Uri(String.Format("file:///{0}/Result.xml", curDir));
            
        }
        public void Auth()
        {
            string appId = "5578372";
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
      
    }

}
