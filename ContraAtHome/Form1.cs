using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ContraAtHome
{
    public partial class Form1 : Form
    {


        bool goLeft;
        bool goRight;
        bool jumping;

        int jumpSpeed;
        int force;
        int playerSpeed = 7;

        int BGlv1_offset;
        int BGlv2_offset;
        int BGlv3_offset;
        int player_offset;

        Player player;

        //Enemy enemy1 = new Soldier();
        //Enemy[] enemies = new Enemy[10];
        //Enemy enm = new Soldier();


        public Form1()
        {
            InitializeComponent();

            SetUpPlayer();

            SetupBG();

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (goLeft == true && player.Left > 150)
            {
                player.Left -= player.Speed;
                //Debug.WriteLine("Player Left : " + player.Left);
                //Debug.WriteLine("Player Location : " + player.Location.X);

            }
            if (goRight == true && player.Left + (player.Width + 150) < this.ClientSize.Width)
            {
                player.Left += player.Speed;
            }

            if (goLeft && BorderLeft.Location.X < 150)
            {
                MoveGameElements("Left");
                ParallexBG(1, 3, 5);
            }

            if (goRight)
            {
                MoveGameElements("Right");
                ParallexBG(1, 3, 5);
            }

            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }


            player.Top += jumpSpeed;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    //check collision between player and each platform 
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 10;
                        player.Top = x.Top - player.Height; // bring player up on the platform 
                    }

                    x.BringToFront(); // bring x to the front layer 
                }
            }
        }

        private void MoveGameElements(string Direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "enemy" || x is PictureBox && (string)x.Tag == "Tag_Border")
                {
                    if (Direction == "Right")
                        x.Left -= playerSpeed;
                    if (Direction == "Left")
                        x.Left += playerSpeed;
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
        }

        private void player_Click(object sender, EventArgs e)
        {

        }

        protected void SetupBG()
        {
            BGLv3.SendToBack();
            BGLv2.SendToBack();
            BGLv1.SendToBack();
            player.BringToFront();

            Debug.WriteLine($"BGLV1 x : {BGLv1.Location.X},y : {BGLv1.Location.Y}");
            Debug.WriteLine($"BGLV1 x : {BGLv2.Location.X},y : {BGLv2.Location.Y}");
            Debug.WriteLine($"Player x : {player.Location.X},y : {player.Location.Y}");


            BGlv1_offset = BGLv1.Size.Width / 2;
            BGlv2_offset = BGLv2.Size.Width / 2;
            BGlv3_offset = BGLv3.Size.Width / 2;
            player_offset = player.Size.Width / 2;

            BGLv1.Location = new Point(player.Location.X - BGlv1_offset + player_offset, BGLv1.Location.Y);
            BGLv2.Location = new Point(player.Location.X - BGlv2_offset + player_offset, BGLv2.Location.Y);
            BGLv3.Location = new Point(player.Location.X - BGlv3_offset + player_offset, BGLv3.Location.Y);


            Debug.WriteLine($"BGLV1 x : {BGLv1.Location.X},y : {BGLv1.Location.Y}");
            Debug.WriteLine($"BGLV1 x : {BGLv2.Location.X},y : {BGLv2.Location.Y}");
            Debug.WriteLine($"Player x : {player.Location.X},y : {player.Location.Y}");
        }

        protected void ParallexBG(int factorParallexBG1, int factorParallexBG2, int factorParallexBG3)
        {
            if (goLeft)
            {
                BGLv1.Location = new Point(BGLv1.Location.X + factorParallexBG1, BGLv1.Location.Y);
                BGLv2.Location = new Point(BGLv2.Location.X + factorParallexBG2, BGLv2.Location.Y);
                BGLv3.Location = new Point(BGLv3.Location.X + factorParallexBG3, BGLv3.Location.Y);
            }
            if (goRight)
            {
                BGLv1.Location = new Point(BGLv1.Location.X - factorParallexBG1, BGLv1.Location.Y);
                BGLv2.Location = new Point(BGLv2.Location.X - factorParallexBG2, BGLv2.Location.Y);
                BGLv3.Location = new Point(BGLv3.Location.X - factorParallexBG3, BGLv3.Location.Y);
            }

        }

        private void SetUpPlayer()
        {
            player = new Player(100, 10, 5, 10, false)
            {
                Size = new Size(50, 50), // Set the size of the player
                BackColor = Color.FromArgb(255, 255, 121, 123), // Set the color of the player for visibility
                Location = new Point(this.ClientSize.Width / 2, 250), // Set the location of the player
            };
            this.Controls.Add(player);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}