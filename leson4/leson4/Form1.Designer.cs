namespace leson4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Email_addressName = new System.Windows.Forms.Label();
            this.EmailAddress = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.PasswordName = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 88);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(260, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // Email_addressName
            // 
            this.Email_addressName.AutoSize = true;
            this.Email_addressName.Location = new System.Drawing.Point(12, 3);
            this.Email_addressName.Name = "Email_addressName";
            this.Email_addressName.Size = new System.Drawing.Size(38, 13);
            this.Email_addressName.TabIndex = 1;
            this.Email_addressName.Text = "E-mail:";
            // 
            // EmailAddress
            // 
            this.EmailAddress.Location = new System.Drawing.Point(74, 3);
            this.EmailAddress.Name = "EmailAddress";
            this.EmailAddress.Size = new System.Drawing.Size(198, 20);
            this.EmailAddress.TabIndex = 2;
            this.EmailAddress.TextChanged += new System.EventHandler(this.EmailAddress_TextChanged);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(74, 30);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(198, 20);
            this.Password.TabIndex = 3;
            // 
            // PasswordName
            // 
            this.PasswordName.AutoSize = true;
            this.PasswordName.Location = new System.Drawing.Point(12, 36);
            this.PasswordName.Name = "PasswordName";
            this.PasswordName.Size = new System.Drawing.Size(56, 13);
            this.PasswordName.TabIndex = 4;
            this.PasswordName.Text = "Password:";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(196, 57);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordName);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.EmailAddress);
            this.Controls.Add(this.Email_addressName);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label Email_addressName;
        private System.Windows.Forms.TextBox EmailAddress;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label PasswordName;
        private System.Windows.Forms.Button LoginButton;
    }
}

