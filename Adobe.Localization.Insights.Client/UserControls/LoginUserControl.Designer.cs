namespace Adobe.Localization.Insights.Client.UserControls
{
    partial class LoginUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblAdobeUserID = new System.Windows.Forms.Label();
            this.btnlogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Location = new System.Drawing.Point(157, 56);
            this.TxtPassword.MaxLength = 12;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(136, 20);
            this.TxtPassword.TabIndex = 5;
            // 
            // TxtUsername
            // 
            this.TxtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUsername.Location = new System.Drawing.Point(157, 24);
            this.TxtUsername.MaxLength = 50;
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(136, 20);
            this.TxtUsername.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(21, 56);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(96, 23);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Password";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdobeUserID
            // 
            this.lblAdobeUserID.Location = new System.Drawing.Point(21, 24);
            this.lblAdobeUserID.Name = "lblAdobeUserID";
            this.lblAdobeUserID.Size = new System.Drawing.Size(100, 23);
            this.lblAdobeUserID.TabIndex = 6;
            this.lblAdobeUserID.Text = "AdobeUserID";
            this.lblAdobeUserID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnlogin
            // 
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Location = new System.Drawing.Point(93, 96);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(75, 23);
            this.btnlogin.TabIndex = 7;
            this.btnlogin.Text = "Log In";
            // 
            // LoginUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtUsername);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblAdobeUserID);
            this.Controls.Add(this.btnlogin);
            this.Name = "LoginUserControl";
            this.Size = new System.Drawing.Size(328, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TxtPassword;
        internal System.Windows.Forms.TextBox TxtUsername;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblAdobeUserID;
        internal System.Windows.Forms.Button btnlogin;
    }
}
