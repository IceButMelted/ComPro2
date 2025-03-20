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
            BorderRight = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox8 = new PictureBox();
            plf2 = new PictureBox();
            KeyPic = new PictureBox();
            BossPicBox = new PictureBox();
            BossGun1 = new PictureBox();
            txt_Live = new Label();
            BG = new PictureBox();
            txt_Objective = new Label();
            TitleUi = new PictureBox();
            pictureBox9 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox12 = new PictureBox();
            pictureBox13 = new PictureBox();
            pictureBox14 = new PictureBox();
            pictureBox15 = new PictureBox();
            pictureBox16 = new PictureBox();
            pictureBox18 = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KeyPic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossPicBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BossGun1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TitleUi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            BorderLeft.Visible = false;
            // 
            // enemy2
            // 
            enemy2.BackColor = Color.Red;
            enemy2.Location = new Point(1370, 117);
            enemy2.Margin = new Padding(0);
            enemy2.Name = "enemy2";
            enemy2.Size = new Size(60, 80);
            enemy2.TabIndex = 9;
            enemy2.TabStop = false;
            enemy2.Tag = "enemy";
            // 
            // BorderRight
            // 
            BorderRight.BackColor = Color.DarkSlateGray;
            BorderRight.Location = new Point(2370, 315);
            BorderRight.Margin = new Padding(0);
            BorderRight.Name = "BorderRight";
            BorderRight.Size = new Size(30, 30);
            BorderRight.TabIndex = 14;
            BorderRight.TabStop = false;
            BorderRight.Tag = "Tag_Border";
            BorderRight.Visible = false;
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
            pictureBox5.Location = new Point(1207, 515);
            pictureBox5.Margin = new Padding(0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(244, 10);
            pictureBox5.TabIndex = 16;
            pictureBox5.TabStop = false;
            pictureBox5.Tag = "platform";
            // 
            // pictureBox6
            // 
            pictureBox6.BackColor = Color.Red;
            pictureBox6.Location = new Point(1261, 435);
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
            plf2.Location = new Point(1370, 197);
            plf2.Margin = new Padding(0);
            plf2.Name = "plf2";
            plf2.Size = new Size(229, 33);
            plf2.TabIndex = 7;
            plf2.TabStop = false;
            plf2.Tag = "platform";
            // 
            // KeyPic
            // 
            KeyPic.BackColor = Color.Transparent;
            KeyPic.Image = Properties.Resources.letter;
            KeyPic.Location = new Point(200, 440);
            KeyPic.Margin = new Padding(0);
            KeyPic.Name = "KeyPic";
            KeyPic.Size = new Size(64, 48);
            KeyPic.SizeMode = PictureBoxSizeMode.StretchImage;
            KeyPic.TabIndex = 21;
            KeyPic.TabStop = false;
            KeyPic.Tag = "key";
            // 
            // BossPicBox
            // 
            BossPicBox.BackColor = Color.Transparent;
            BossPicBox.Location = new Point(339, -112);
            BossPicBox.Name = "BossPicBox";
            BossPicBox.Size = new Size(180, 120);
            BossPicBox.TabIndex = 23;
            BossPicBox.TabStop = false;
            // 
            // BossGun1
            // 
            BossGun1.BackColor = Color.Transparent;
            BossGun1.BackgroundImage = Properties.Resources.TopTurret;
            BossGun1.BackgroundImageLayout = ImageLayout.Stretch;
            BossGun1.Location = new Point(652, -120);
            BossGun1.Name = "BossGun1";
            BossGun1.Size = new Size(64, 128);
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
            // BG
            // 
            BG.BackgroundImage = Properties.Resources.BG;
            BG.Location = new Point(0, -42);
            BG.Margin = new Padding(0);
            BG.Name = "BG";
            BG.Size = new Size(800, 600);
            BG.TabIndex = 27;
            BG.TabStop = false;
            // 
            // txt_Objective
            // 
            txt_Objective.AutoSize = true;
            txt_Objective.BackColor = Color.Black;
            txt_Objective.Font = new Font("Unispace", 15.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txt_Objective.ForeColor = Color.SpringGreen;
            txt_Objective.ImageAlign = ContentAlignment.MiddleRight;
            txt_Objective.Location = new Point(492, 533);
            txt_Objective.Name = "txt_Objective";
            txt_Objective.Size = new Size(272, 25);
            txt_Objective.TabIndex = 29;
            txt_Objective.Text = "Glitches Remain : 00";
            // 
            // TitleUi
            // 
            TitleUi.BackColor = Color.RosyBrown;
            TitleUi.BackgroundImage = Properties.Resources.MainMenuScreen;
            TitleUi.Location = new Point(0, 0);
            TitleUi.Name = "TitleUi";
            TitleUi.Size = new Size(800, 600);
            TitleUi.TabIndex = 30;
            TitleUi.TabStop = false;
            TitleUi.Visible = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackColor = Color.IndianRed;
            pictureBox9.BackgroundImage = Properties.Resources.Platform2;
            pictureBox9.Location = new Point(2100, 355);
            pictureBox9.Margin = new Padding(0);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(291, 33);
            pictureBox9.TabIndex = 32;
            pictureBox9.TabStop = false;
            pictureBox9.Tag = "platform";
            // 
            // pictureBox10
            // 
            pictureBox10.BackColor = Color.IndianRed;
            pictureBox10.BackgroundImage = Properties.Resources.Platform3;
            pictureBox10.Location = new Point(2100, 197);
            pictureBox10.Margin = new Padding(0);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(291, 33);
            pictureBox10.TabIndex = 31;
            pictureBox10.TabStop = false;
            pictureBox10.Tag = "platform";
            // 
            // pictureBox11
            // 
            pictureBox11.BackColor = Color.IndianRed;
            pictureBox11.BackgroundImage = Properties.Resources.Platform1;
            pictureBox11.Location = new Point(1712, 197);
            pictureBox11.Margin = new Padding(0);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(291, 33);
            pictureBox11.TabIndex = 33;
            pictureBox11.TabStop = false;
            pictureBox11.Tag = "platform";
            // 
            // pictureBox12
            // 
            pictureBox12.BackColor = Color.Red;
            pictureBox12.Location = new Point(2214, 117);
            pictureBox12.Margin = new Padding(0);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(60, 80);
            pictureBox12.TabIndex = 34;
            pictureBox12.TabStop = false;
            pictureBox12.Tag = "enemy";
            // 
            // pictureBox13
            // 
            pictureBox13.BackColor = Color.Red;
            pictureBox13.Location = new Point(2331, 275);
            pictureBox13.Margin = new Padding(0);
            pictureBox13.Name = "pictureBox13";
            pictureBox13.Size = new Size(60, 80);
            pictureBox13.TabIndex = 35;
            pictureBox13.TabStop = false;
            pictureBox13.Tag = "enemy";
            // 
            // pictureBox14
            // 
            pictureBox14.BackColor = Color.Red;
            pictureBox14.Location = new Point(1829, 117);
            pictureBox14.Margin = new Padding(0);
            pictureBox14.Name = "pictureBox14";
            pictureBox14.Size = new Size(60, 80);
            pictureBox14.TabIndex = 36;
            pictureBox14.TabStop = false;
            pictureBox14.Tag = "enemy";
            // 
            // pictureBox15
            // 
            pictureBox15.BackColor = Color.IndianRed;
            pictureBox15.Location = new Point(1997, 515);
            pictureBox15.Margin = new Padding(0);
            pictureBox15.Name = "pictureBox15";
            pictureBox15.Size = new Size(394, 10);
            pictureBox15.TabIndex = 37;
            pictureBox15.TabStop = false;
            pictureBox15.Tag = "platform";
            // 
            // pictureBox16
            // 
            pictureBox16.BackColor = Color.Red;
            pictureBox16.Location = new Point(1997, 435);
            pictureBox16.Margin = new Padding(0);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(60, 80);
            pictureBox16.TabIndex = 38;
            pictureBox16.TabStop = false;
            pictureBox16.Tag = "enemy";
            // 
            // pictureBox18
            // 
            pictureBox18.BackColor = Color.Red;
            pictureBox18.Location = new Point(1557, 276);
            pictureBox18.Margin = new Padding(0);
            pictureBox18.Name = "pictureBox18";
            pictureBox18.Size = new Size(60, 80);
            pictureBox18.TabIndex = 40;
            pictureBox18.TabStop = false;
            pictureBox18.Tag = "enemy";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.IndianRed;
            pictureBox1.BackgroundImage = Properties.Resources.Platform2;
            pictureBox1.Location = new Point(1517, 356);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 33);
            pictureBox1.TabIndex = 41;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "platform";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            AutoScrollMinSize = new Size(2400, 0);
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(800, 557);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox18);
            Controls.Add(pictureBox16);
            Controls.Add(pictureBox15);
            Controls.Add(pictureBox14);
            Controls.Add(pictureBox13);
            Controls.Add(pictureBox12);
            Controls.Add(pictureBox11);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox10);
            Controls.Add(txt_Objective);
            Controls.Add(txt_Live);
            Controls.Add(BossGun1);
            Controls.Add(BossPicBox);
            Controls.Add(KeyPic);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox4);
            Controls.Add(BorderRight);
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
            Controls.Add(TitleUi);
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
            ((System.ComponentModel.ISupportInitialize)BorderRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf2).EndInit();
            ((System.ComponentModel.ISupportInitialize)KeyPic).EndInit();
            ((System.ComponentModel.ISupportInitialize)BossPicBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)BossGun1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BG).EndInit();
            ((System.ComponentModel.ISupportInitialize)TitleUi).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox13).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox14).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox15).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private PictureBox BorderRight;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        public PictureBox pictureBox6;
        public PictureBox pictureBox8;
        private PictureBox plf2;
        private PictureBox KeyPic;
        private PictureBox BossPicBox;
        private PictureBox BossGun1;
        private Label txt_Live;
        private PictureBox BG;
        private Label txt_Objective;
        private PictureBox TitleUi;
        private PictureBox pictureBox9;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        public PictureBox pictureBox12;
        public PictureBox pictureBox13;
        public PictureBox pictureBox14;
        private PictureBox pictureBox15;
        public PictureBox pictureBox16;
        public PictureBox pictureBox18;
        private PictureBox pictureBox1;
    }
}
