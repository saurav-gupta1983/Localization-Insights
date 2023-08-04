namespace Adobe.Localization.Insights.Client.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxLoginID = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.lblAdobeLoginID = new System.Windows.Forms.Label();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btnOffline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxPassword.Location = new System.Drawing.Point(159, 61);
            this.TextBoxPassword.MaxLength = 12;
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(151, 26);
            this.TextBoxPassword.TabIndex = 2;
            // 
            // TextBoxLoginID
            // 
            this.TextBoxLoginID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxLoginID.Location = new System.Drawing.Point(159, 29);
            this.TextBoxLoginID.MaxLength = 50;
            this.TextBoxLoginID.Name = "TextBoxLoginID";
            this.TextBoxLoginID.Size = new System.Drawing.Size(151, 20);
            this.TextBoxLoginID.TabIndex = 1;
            // 
            // LabelPassword
            // 
            this.LabelPassword.Location = new System.Drawing.Point(23, 61);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(96, 23);
            this.LabelPassword.TabIndex = 13;
            this.LabelPassword.Text = "Password";
            this.LabelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdobeLoginID
            // 
            this.lblAdobeLoginID.Location = new System.Drawing.Point(23, 29);
            this.lblAdobeLoginID.Name = "lblAdobeLoginID";
            this.lblAdobeLoginID.Size = new System.Drawing.Size(100, 23);
            this.lblAdobeLoginID.TabIndex = 11;
            this.lblAdobeLoginID.Text = "Adobe LoginID";
            this.lblAdobeLoginID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnlogin
            // 
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Location = new System.Drawing.Point(38, 98);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(120, 23);
            this.btnlogin.TabIndex = 3;
            this.btnlogin.Text = "Log In";
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btnOffline
            // 
            this.btnOffline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffline.Location = new System.Drawing.Point(169, 98);
            this.btnOffline.Name = "btnOffline";
            this.btnOffline.Size = new System.Drawing.Size(120, 23);
            this.btnOffline.TabIndex = 4;
            this.btnOffline.Text = "Work Offline";
            this.btnOffline.Click += new System.EventHandler(this.btnOffline_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 142);
            this.Controls.Add(this.btnOffline);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.TextBoxLoginID);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.lblAdobeLoginID);
            this.Controls.Add(this.btnlogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to Oogway Back-end client";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBoxPassword;
        internal System.Windows.Forms.TextBox TextBoxLoginID;
        internal System.Windows.Forms.Label LabelPassword;
        internal System.Windows.Forms.Label lblAdobeLoginID;
        internal System.Windows.Forms.Button btnlogin;
        internal System.Windows.Forms.Button btnOffline;
    }
}