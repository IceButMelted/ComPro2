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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            BorderRight = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            plf2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf2).BeginInit();
            SuspendLayout();
            // 
            // plf
            // 
            plf.BackColor = Color.IndianRed;
            plf.Location = new Point(0, 532);
            plf.Margin = new Padding(0);
            plf.Name = "plf";
            plf.Size = new Size(1300, 20);
            plf.TabIndex = 0;
            plf.TabStop = false;
            plf.Tag = "platform";
            // 
            // plf1
            // 
            plf1.BackColor = Color.IndianRed;
            plf1.Location = new Point(585, 205);
            plf1.Margin = new Padding(0);
            plf1.Name = "plf1";
            plf1.Size = new Size(300, 20);
            plf1.TabIndex = 1;
            plf1.TabStop = false;
            plf1.Tag = "platform";
            // 
            // enemy1
            // 
            enemy1.BackColor = Color.Red;
            enemy1.Location = new Point(713, 155);
            enemy1.Margin = new Padding(0);
            enemy1.Name = "enemy1";
            enemy1.Size = new Size(40, 50);
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
            BGLv1.BackColor = Color.DarkGreen;
            BGLv1.Location = new Point(-6, 29);
            BGLv1.Name = "BGLv1";
            BGLv1.Size = new Size(400, 100);
            BGLv1.TabIndex = 4;
            BGLv1.TabStop = false;
            // 
            // BGLv2
            // 
            BGLv2.BackColor = Color.Chartreuse;
            BGLv2.Location = new Point(43, 81);
            BGLv2.Name = "BGLv2";
            BGLv2.Size = new Size(300, 100);
            BGLv2.TabIndex = 5;
            BGLv2.TabStop = false;
            // 
            // BGLv3
            // 
            BGLv3.BackColor = Color.DarkSeaGreen;
            BGLv3.Location = new Point(43, 125);
            BGLv3.Name = "BGLv3";
            BGLv3.Size = new Size(200, 100);
            BGLv3.TabIndex = 6;
            BGLv3.TabStop = false;
            // 
            // BorderLeft
            // 
            BorderLeft.BackColor = Color.DarkSlateGray;
            BorderLeft.Location = new Point(0, 315);
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
            enemy2.Location = new Point(597, 330);
            enemy2.Margin = new Padding(0);
            enemy2.Name = "enemy2";
            enemy2.Size = new Size(40, 50);
            enemy2.TabIndex = 9;
            enemy2.TabStop = false;
            enemy2.Tag = "enemy";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.IndianRed;
            pictureBox1.Location = new Point(346, 125);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "platform";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.IndianRed;
            pictureBox2.Location = new Point(501, 380);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(300, 20);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "platform";
            // 
            // BorderRight
            // 
            BorderRight.BackColor = Color.DarkSlateGray;
            BorderRight.Location = new Point(1270, 315);
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
            pictureBox4.Location = new Point(943, 424);
            pictureBox4.Margin = new Padding(0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(300, 20);
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            pictureBox4.Tag = "platform";
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.IndianRed;
            pictureBox5.Location = new Point(706, 532);
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
            pictureBox6.Location = new Point(845, 482);
            pictureBox6.Margin = new Padding(0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(40, 50);
            pictureBox6.TabIndex = 17;
            pictureBox6.TabStop = false;
            pictureBox6.Tag = "enemy";
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.IndianRed;
            pictureBox7.Location = new Point(990, 424);
            pictureBox7.Margin = new Padding(0);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(253, 10);
            pictureBox7.TabIndex = 18;
            pictureBox7.TabStop = false;
            pictureBox7.Tag = "platform";
            // 
            // pictureBox8
            // 
            pictureBox8.BackColor = Color.Red;
            pictureBox8.Location = new Point(990, 374);
            pictureBox8.Margin = new Padding(0);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(40, 50);
            pictureBox8.TabIndex = 19;
            pictureBox8.TabStop = false;
            pictureBox8.Tag = "enemy";
            // 
            // plf2
            // 
            plf2.BackColor = Color.IndianRed;
            plf2.Location = new Point(819, 282);
            plf2.Margin = new Padding(0);
            plf2.Name = "plf2";
            plf2.Size = new Size(300, 20);
            plf2.TabIndex = 7;
            plf2.TabStop = false;
            plf2.Tag = "platform";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.LightBlue;
            ClientSize = new Size(780, 557);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox4);
            Controls.Add(BorderRight);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(enemy2);
            Controls.Add(BorderLeft);
            Controls.Add(plf2);
            Controls.Add(BGLv3);
            Controls.Add(BGLv2);
            Controls.Add(BGLv1);
            Controls.Add(enemy1);
            Controls.Add(plf1);
            Controls.Add(plf);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BorderRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf2).EndInit();
            ResumeLayout(false);
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
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox BorderRight;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        public PictureBox pictureBox6;
        private PictureBox pictureBox7;
        public PictureBox pictureBox8;
        private PictureBox plf2;
    }
}
