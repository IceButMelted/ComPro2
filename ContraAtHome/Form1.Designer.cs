namespace ContraAtHome
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            plf = new PictureBox();
            plf1 = new PictureBox();
            enemy1 = new PictureBox();
            gameTimer = new System.Windows.Forms.Timer(components);
            BGLv1 = new PictureBox();
            BGLv2 = new PictureBox();
            BGLv3 = new PictureBox();
            BorderLeft = new PictureBox();
            enemy2 = new PictureBox();
            pictureBox2 = new PictureBox();
            BorderRight = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox8 = new PictureBox();
            plf2 = new PictureBox();
            BG = new PictureBox();
            KeyPic = new PictureBox();
            pictureBox3 = new PictureBox();
            BossPicBox = new PictureBox();
            BossGun1 = new PictureBox();
            txt_Live = new Label();
            txt_tut = new Label();
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KeyPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossGun1).BeginInit();
            SuspendLayout();
            // 
            // plf
            // 
            plf.BackColor = Color.IndianRed;
            plf.BackgroundImage = Properties.Resources.Platform1;
            plf.Location = new Point(0, 515);
            plf.Margin = new Padding(0);
            plf.Name = "plf";
            plf.Size = new Size(2400, 33);
            plf.TabIndex = 0;
            plf.TabStop = false;
            plf.Tag = "platform";
            // 
            // plf1
            // 
            plf1.BackColor = Color.IndianRed;
            plf1.BackgroundImage = Properties.Resources.Platform1;
            plf1.Location = new Point(947, 197);
            plf1.Margin = new Padding(0);
            plf1.Name = "plf1";
            plf1.Size = new Size(291, 33);
            plf1.TabIndex = 1;
            plf1.TabStop = false;
            plf1.Tag = "platform";
            // 
            // enemy1
            // 
            enemy1.BackColor = Color.Red;
            enemy1.Location = new Point(947, 117);
            enemy1.Margin = new Padding(0);
            enemy1.Name = "enemy1";
            enemy1.Size = new Size(60, 80);
            enemy1.TabIndex = 3;
            enemy1.TabStop = false;
            enemy1.Tag = "enemy";
            // 
            // gameTimer
            // 
            gameTimer.Enabled = true;
            gameTimer.Interval = 60;
            gameTimer.Tick += MainGameTimerEvent;
            // 
            // BGLv1
            // 
            BGLv1.BackColor = Color.DimGray;
            BGLv1.BackgroundImage = Properties.Resources.BG_Parallax_002;
            BGLv1.Location = new Point(33, 0);
            BGLv1.Name = "BGLv1";
            BGLv1.Size = new Size(30, 30);
            BGLv1.TabIndex = 4;
            BGLv1.TabStop = false;
            // 
            // BGLv2
            // 
            BGLv2.BackColor = Color.DarkGray;
            BGLv2.BackgroundImage = Properties.Resources.BG_Parallax_001;
            BGLv2.Location = new Point(79, 41);
            BGLv2.Name = "BGLv2";
            BGLv2.Size = new Size(30, 30);
            BGLv2.TabIndex = 5;
            BGLv2.TabStop = false;
            // 
            // BGLv3
            // 
            BGLv3.BackColor = Color.Gainsboro;
            BGLv3.BackgroundImage = Properties.Resources.BG_Parallax_000;
            BGLv3.BackgroundImageLayout = ImageLayout.None;
            BGLv3.Location = new Point(143, 77);
            BGLv3.Name = "BGLv3";
            BGLv3.Size = new Size(30, 30);
            BGLv3.TabIndex = 6;
            BGLv3.TabStop = false;
            // 
            // BorderLeft
            // 
            BorderLeft.BackColor = Color.DarkSlateGray;
            BorderLeft.Location = new Point(0, 316);
            BorderLeft.Margin = new Padding(0);
            BorderLeft.Name = "BorderLeft";
            BorderLeft.Size = new Size(30, 30);
            BorderLeft.TabIndex = 8;
            BorderLeft.TabStop = false;
            BorderLeft.Tag = "Tag_Border";
            // 
            // enemy2
            // 
            enemy2.BackColor = Color.Red;
            enemy2.Location = new Point(1442, 243);
            enemy2.Margin = new Padding(0);
            enemy2.Name = "enemy2";
            enemy2.Size = new Size(60, 80);
            enemy2.TabIndex = 9;
            enemy2.TabStop = false;
            enemy2.Tag = "enemy";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.IndianRed;
            pictureBox2.BackgroundImage = Properties.Resources.Platform2;
            pictureBox2.Location = new Point(0, 356);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(241, 33);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "platform";
            // 
            // BorderRight
            // 
            BorderRight.BackColor = Color.DarkSlateGray;
            BorderRight.Location = new Point(1800, 315);
            BorderRight.Margin = new Padding(0);
            BorderRight.Name = "BorderRight";
            BorderRight.Size = new Size(30, 30);
            BorderRight.TabIndex = 14;
            BorderRight.TabStop = false;
            BorderRight.Tag = "Tag_Border";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.IndianRed;
            pictureBox4.BackgroundImage = Properties.Resources.Platform2;
            pictureBox4.Location = new Point(947, 356);
            pictureBox4.Margin = new Padding(0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(291, 33);
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            pictureBox4.Tag = "platform";
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.IndianRed;
            pictureBox5.Location = new Point(706, 515);
            pictureBox5.Margin = new Padding(0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(311, 10);
            pictureBox5.TabIndex = 16;
            pictureBox5.TabStop = false;
            pictureBox5.Tag = "platform";
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Red;
            pictureBox6.Location = new Point(851, 435);
            pictureBox6.Margin = new Padding(0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(60, 80);
            pictureBox6.TabIndex = 17;
            pictureBox6.TabStop = false;
            pictureBox6.Tag = "enemy";
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Red;
            pictureBox8.Location = new Point(993, 276);
            pictureBox8.Margin = new Padding(0);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(60, 80);
            pictureBox8.TabIndex = 19;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "enemy";
            // 
            // plf2
            // 
            plf2.BackColor = Color.IndianRed;
            plf2.BackgroundImage = Properties.Resources.Platform4;
            plf2.Location = new Point(1374, 323);
            plf2.Margin = new Padding(0);
            plf2.Name = "plf2";
            plf2.Size = new Size(300, 33);
            plf2.TabIndex = 7;
            plf2.TabStop = false;
            plf2.Tag = "platform";
            // 
            // BG
            // 
            BG.BackgroundImage = Properties.Resources.BG;
            BG.Location = new Point(0, 0);
            BG.Name = "BG";
            BG.Size = new Size(800, 600);
            BG.TabIndex = 20;
            BG.TabStop = false;
            // 
            // KeyPic
            // 
            KeyPic.BackColor = Color.Aquamarine;
            KeyPic.Location = new Point(396, 429);
            KeyPic.Margin = new Padding(0);
            KeyPic.Name = "KeyPic";
            KeyPic.Size = new Size(50, 50);
            KeyPic.TabIndex = 21;
            KeyPic.TabStop = false;
            KeyPic.Tag = "key";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.IndianRed;
            pictureBox3.BackgroundImage = Properties.Resources.Platform3;
            pictureBox3.Location = new Point(559, 356);
            pictureBox3.Margin = new Padding(0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(256, 33);
            pictureBox3.TabIndex = 22;
            pictureBox3.TabStop = false;
            pictureBox3.Tag = "platform";
            // 
            // BossPicBox
            // 
            BossPicBox.BackColor = Color.Aqua;
            BossPicBox.Location = new Point(313, -81);
            BossPicBox.Name = "BossPicBox";
            BossPicBox.Size = new Size(191, 93);
            BossPicBox.TabIndex = 23;
            BossPicBox.TabStop = false;
            // 
            // BossGun1
            // 
            BossGun1.BackColor = Color.DarkOrchid;
            BossGun1.Location = new Point(653, -38);
            BossGun1.Name = "BossGun1";
            BossGun1.Size = new Size(30, 50);
            BossGun1.TabIndex = 24;
            BossGun1.TabStop = false;
            // 
            // txt_Live
            // 
            txt_Live.AutoSize = true;
            txt_Live.BackColor = Color.Black;
            txt_Live.Font = new Font("Unispace", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_Live.ForeColor = Color.SpringGreen;
            txt_Live.Location = new Point(0, 533);
            txt_Live.Name = "txt_Live";
            txt_Live.Size = new Size(116, 25);
            txt_Live.TabIndex = 25;
            txt_Live.Text = "Live : 5";
            // 
            // txt_tut
            // 
            txt_tut.AutoSize = true;
            txt_tut.BackColor = Color.Black;
            txt_tut.Font = new Font("Unispace", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_tut.ForeColor = Color.SpringGreen;
            txt_tut.Location = new Point(79, 110);
            txt_tut.Name = "txt_tut";
            txt_tut.Size = new Size(662, 50);
            txt_tut.TabIndex = 26;
            txt_tut.Text = "'A' : move left | 'D' : move right | 'W' : Face up\r\n'J' : shoot | 'K' : jump";
            txt_tut.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(780, 557);
            Controls.Add(txt_tut);
            Controls.Add(txt_Live);
            Controls.Add(BossGun1);
            Controls.Add(BossPicBox);
            Controls.Add(pictureBox3);
            Controls.Add(KeyPic);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox4);
            Controls.Add(BorderRight);
            Controls.Add(pictureBox2);
            Controls.Add(enemy2);
            Controls.Add(BorderLeft);
            Controls.Add(plf2);
            Controls.Add(enemy1);
            Controls.Add(plf1);
            Controls.Add(plf);
            Controls.Add(BGLv3);
            Controls.Add(BGLv2);
            Controls.Add(BGLv1);
            Controls.Add(BG);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)plf).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf1).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).EndInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BorderRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BG).EndInit();
            ((System.ComponentModel.ISupportInitialize)KeyPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)BossPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)BossGun1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox plf;
        private PictureBox plf1;
        public PictureBox enemy1;
        public PictureBox enemy2;
        private System.Windows.Forms.Timer gameTimer;
        public PictureBox BGLv1;
        public PictureBox BGLv2;
        public PictureBox BGLv3;
        private PictureBox BorderLeft;
        private PictureBox pictureBox2;
        private PictureBox BorderRight;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        public PictureBox pictureBox6;
        public PictureBox pictureBox8;
        private PictureBox plf2;
        private PictureBox BG;
        private PictureBox KeyPic;
        private PictureBox pictureBox3;
        private PictureBox BossPicBox;
        private PictureBox BossGun1;
        private Label txt_Live;
        private Label txt_tut;
    }
}
