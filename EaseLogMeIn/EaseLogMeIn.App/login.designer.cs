namespace EaseLogMeIn.App
{
    partial class Login
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnADLogin = new System.Windows.Forms.Button();
            this.pnlADUsers = new System.Windows.Forms.Panel();
            this.pnlNonADUsers = new System.Windows.Forms.Panel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNonADUser = new System.Windows.Forms.Button();
            this.txtPasswordNonADUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlADUsers.SuspendLayout();
            this.pnlNonADUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(250, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(152, 29);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblUserName.Location = new System.Drawing.Point(181, 22);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(27, 21);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Hi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(143, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // btnADLogin
            // 
            this.btnADLogin.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnADLogin.ForeColor = System.Drawing.Color.Green;
            this.btnADLogin.Location = new System.Drawing.Point(267, 116);
            this.btnADLogin.Name = "btnADLogin";
            this.btnADLogin.Size = new System.Drawing.Size(115, 32);
            this.btnADLogin.TabIndex = 3;
            this.btnADLogin.Text = "Continue";
            this.btnADLogin.UseVisualStyleBackColor = true;
            this.btnADLogin.Click += new System.EventHandler(this.BtnLoginAD_Click);
            // 
            // pnlADUsers
            // 
            this.pnlADUsers.Controls.Add(this.lblUserName);
            this.pnlADUsers.Controls.Add(this.btnADLogin);
            this.pnlADUsers.Controls.Add(this.txtPassword);
            this.pnlADUsers.Controls.Add(this.label2);
            this.pnlADUsers.Location = new System.Drawing.Point(93, 135);
            this.pnlADUsers.Name = "pnlADUsers";
            this.pnlADUsers.Size = new System.Drawing.Size(620, 166);
            this.pnlADUsers.TabIndex = 4;
            // 
            // pnlNonADUsers
            // 
            this.pnlNonADUsers.Controls.Add(this.txtUserName);
            this.pnlNonADUsers.Controls.Add(this.label4);
            this.pnlNonADUsers.Controls.Add(this.btnNonADUser);
            this.pnlNonADUsers.Controls.Add(this.txtPasswordNonADUser);
            this.pnlNonADUsers.Controls.Add(this.label3);
            this.pnlNonADUsers.Location = new System.Drawing.Point(96, 133);
            this.pnlNonADUsers.Name = "pnlNonADUsers";
            this.pnlNonADUsers.Size = new System.Drawing.Size(620, 166);
            this.pnlNonADUsers.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(250, 11);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(152, 29);
            this.txtUserName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(143, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "UserName";
            // 
            // btnNonADUser
            // 
            this.btnNonADUser.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNonADUser.ForeColor = System.Drawing.Color.Green;
            this.btnNonADUser.Location = new System.Drawing.Point(267, 116);
            this.btnNonADUser.Name = "btnNonADUser";
            this.btnNonADUser.Size = new System.Drawing.Size(115, 32);
            this.btnNonADUser.TabIndex = 3;
            this.btnNonADUser.Text = "Continue";
            this.btnNonADUser.UseVisualStyleBackColor = true;
            this.btnNonADUser.Click += new System.EventHandler(this.BtnNonADUser_Click);
            // 
            // txtPasswordNonADUser
            // 
            this.txtPasswordNonADUser.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordNonADUser.Location = new System.Drawing.Point(250, 60);
            this.txtPasswordNonADUser.Name = "txtPasswordNonADUser";
            this.txtPasswordNonADUser.PasswordChar = '*';
            this.txtPasswordNonADUser.Size = new System.Drawing.Size(152, 29);
            this.txtPasswordNonADUser.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(143, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlNonADUsers);
            this.Controls.Add(this.pnlADUsers);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.pnlADUsers.ResumeLayout(false);
            this.pnlADUsers.PerformLayout();
            this.pnlNonADUsers.ResumeLayout(false);
            this.pnlNonADUsers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnADLogin;
        private System.Windows.Forms.Panel pnlADUsers;
        private System.Windows.Forms.Panel pnlNonADUsers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNonADUser;
        private System.Windows.Forms.TextBox txtPasswordNonADUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
    }
}