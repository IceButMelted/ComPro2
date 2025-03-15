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
            plf2 = new PictureBox();
            BorderLeft = new PictureBox();
            enemy2 = new PictureBox();
            plf3 = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plf3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // plf
            // 
            plf.BackColor = Color.IndianRed;
            plf.Location = new Point(0, 302);
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
            plf1.Location = new Point(472, 143);
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
            enemy1.Location = new Point(472, 93);
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
            // plf2
            // 
            plf2.BackColor = Color.IndianRed;
            plf2.Location = new Point(851, 198);
            plf2.Margin = new Padding(0);
            plf2.Name = "plf2";
            plf2.Size = new Size(300, 10);
            plf2.TabIndex = 7;
            plf2.TabStop = false;
            plf2.Tag = "platform";
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
            enemy2.Location = new Point(354, 252);
            enemy2.Margin = new Padding(0);
            enemy2.Name = "enemy2";
            enemy2.Size = new Size(40, 50);
            enemy2.TabIndex = 9;
            enemy2.TabStop = false;
            enemy2.Tag = "enemy";
            // 
            // plf3
            // 
            plf3.BackColor = Color.IndianRed;
            plf3.Location = new Point(354, 322);
            plf3.Margin = new Padding(0);
            plf3.Name = "plf3";
            plf3.Size = new Size(100, 10);
            plf3.TabIndex = 10;
            plf3.TabStop = false;
            plf3.Tag = "platform";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.IndianRed;
            pictureBox1.Location = new Point(124, 228);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 74);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "platform";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(784, 561);
            Controls.Add(pictureBox1);
            Controls.Add(plf3);
            Controls.Add(enemy2);
            Controls.Add(BorderLeft);
            Controls.Add(plf2);
            Controls.Add(BGLv3);
            Controls.Add(BGLv2);
            Controls.Add(BGLv1);
            Controls.Add(enemy1);
            Controls.Add(plf1);
            Controls.Add(plf);
            Name = "Form1";
            Text = "Form1";
            //Load += Form1_Load;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)plf).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf1).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BorderLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)plf3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox plf;
        private PictureBox plf1;
        private PictureBox plf2;
        private PictureBox plf3;
        public PictureBox enemy1;
        public PictureBox enemy2;
        private System.Windows.Forms.Timer gameTimer;
        public PictureBox BGLv1;
        public PictureBox BGLv2;
        public PictureBox BGLv3;
        private PictureBox BorderLeft;
        private PictureBox pictureBox1;
    }
}
