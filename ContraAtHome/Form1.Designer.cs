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
            pictureBox1 = new PictureBox();
            player = new PictureBox();
            enemyOne = new PictureBox();
            gameTimer = new System.Windows.Forms.Timer(components);
            BGLv1 = new PictureBox();
            BGLv2 = new PictureBox();
            BGLv3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyOne).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // plf
            // 
            plf.BackColor = Color.IndianRed;
            plf.Location = new Point(-6, 302);
            plf.Margin = new Padding(0);
            plf.Name = "plf";
            plf.Size = new Size(1303, 50);
            plf.TabIndex = 0;
            plf.TabStop = false;
            plf.Tag = "platform";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.IndianRed;
            pictureBox1.Location = new Point(472, 143);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(300, 50);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Tag = "platform";
            // 
            // player
            // 
            player.BackColor = Color.Green;
            player.Location = new Point(264, 252);
            player.Margin = new Padding(0);
            player.Name = "player";
            player.Size = new Size(40, 50);
            player.TabIndex = 2;
            player.TabStop = false;
            player.Click += player_Click;
            // 
            // enemyOne
            // 
            enemyOne.BackColor = Color.Red;
            enemyOne.Location = new Point(472, 93);
            enemyOne.Margin = new Padding(0);
            enemyOne.Name = "enemyOne";
            enemyOne.Size = new Size(40, 50);
            enemyOne.TabIndex = 3;
            enemyOne.TabStop = false;
            enemyOne.Tag = "enemy";
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
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.IndianRed;
            pictureBox2.Location = new Point(851, 198);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(300, 50);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Tag = "platform";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.DarkSlateGray;
            pictureBox3.Location = new Point(139, 252);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(33, 36);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(584, 361);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(BGLv3);
            Controls.Add(BGLv2);
            Controls.Add(BGLv1);
            Controls.Add(enemyOne);
            Controls.Add(player);
            Controls.Add(pictureBox1);
            Controls.Add(plf);
            Name = "Form1";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)plf).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemyOne).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv1).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv2).EndInit();
            ((System.ComponentModel.ISupportInitialize)BGLv3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox plf;
        private PictureBox pictureBox1;
        public PictureBox enemyOne;
        private System.Windows.Forms.Timer gameTimer;
        public PictureBox player;
        public PictureBox BGLv1;
        public PictureBox BGLv2;
        public PictureBox BGLv3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}
