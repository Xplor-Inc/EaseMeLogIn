namespace EaseLogMeIn.App
{
    partial class Home
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
            this.components = new System.ComponentModel.Container();
            this.lstAirlines = new System.Windows.Forms.ComboBox();
            this.btnLoginWebsite = new System.Windows.Forms.Button();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAirlines
            // 
            this.lstAirlines.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAirlines.FormattingEnabled = true;
            this.lstAirlines.Location = new System.Drawing.Point(57, 3);
            this.lstAirlines.Name = "lstAirlines";
            this.lstAirlines.Size = new System.Drawing.Size(172, 30);
            this.lstAirlines.TabIndex = 2;
            // 
            // btnLoginWebsite
            // 
            this.btnLoginWebsite.Font = new System.Drawing.Font("Calibri", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginWebsite.ForeColor = System.Drawing.Color.Green;
            this.btnLoginWebsite.Location = new System.Drawing.Point(257, 0);
            this.btnLoginWebsite.Name = "btnLoginWebsite";
            this.btnLoginWebsite.Size = new System.Drawing.Size(115, 34);
            this.btnLoginWebsite.TabIndex = 4;
            this.btnLoginWebsite.Text = "Continue";
            this.btnLoginWebsite.UseVisualStyleBackColor = true;
            this.btnLoginWebsite.Click += new System.EventHandler(this.BtnLoginWebsite_Click);
            // 
            // picLoader
            // 
            this.picLoader.Image = global::EaseLogMeIn.App.Properties.Resources.animation;
            this.picLoader.Location = new System.Drawing.Point(316, 179);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(210, 158);
            this.picLoader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLoader.TabIndex = 5;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::EaseLogMeIn.App.Properties.Resources.home;
            this.btnRefresh.Location = new System.Drawing.Point(3, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 24);
            this.btnRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lstAirlines);
            this.panel1.Controls.Add(this.btnLoginWebsite);
            this.panel1.Location = new System.Drawing.Point(0, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 38);
            this.panel1.TabIndex = 7;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUserName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(874, 7);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(113, 27);
            this.lblUserName.TabIndex = 7;
            this.lblUserName.Text = "UserName";
            this.lblUserName.Click += new System.EventHandler(this.LblUserName_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1514, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picLoader);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.Padding = new System.Windows.Forms.Padding(0, 45, 0, 50);
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox lstAirlines;
        private System.Windows.Forms.Button btnLoginWebsite;
        private System.Windows.Forms.PictureBox picLoader;
        private System.Windows.Forms.PictureBox btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

