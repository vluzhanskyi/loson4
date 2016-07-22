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
using VkNet.Enums.Filters;
using VkNet;
using VkNet.Model.RequestParams;

namespace leson4
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
         
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void EmailAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            VKLogic l = new VKLogic();
            l.CollectLoginData(EmailAddress.Text, Password.Text);
            l.SaveData();
        }
    }
}
