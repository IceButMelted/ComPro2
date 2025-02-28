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
            ((System.ComponentModel.ISupportInitialize)plf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemyOne).BeginInit();
            SuspendLayout();
            // 
            // plf
            // 
            plf.BackColor = Color.IndianRed;
            plf.Location = new Point(9, 302);
            plf.Margin = new Padding(0);
            plf.Name = "plf";
            plf.Size = new Size(760, 50);
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
            player.Location = new Point(9, 252);
            player.Margin = new Padding(0);
            player.Name = "player";
            player.Size = new Size(40, 50);
            player.TabIndex = 2;
            player.TabStop = false;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(784, 361);
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
            ResumeLayout(false);
        }

        #endregion

        private PictureBox plf;
        private PictureBox pictureBox1;
        public PictureBox enemyOne;
        private System.Windows.Forms.Timer gameTimer;
        public PictureBox player;
    }
}
