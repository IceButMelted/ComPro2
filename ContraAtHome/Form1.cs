using System;
using System.Diagnostics;

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

        int factorParallexBG1 = 1;
        int factorParallexBG2 = 3;
        int factorParallexBG3 = 5;

        //Enemy enemy1 = new Soldier();
        //Enemy[] enemies = new Enemy[10];
        //Enemy enm = new Soldier();


        public Form1()
        {
            InitializeComponent();
            SetZLevelBG();

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
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

            ParallexBG();

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
            if (e.KeyCode == Keys.W && jumping == false)
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

        private void SetZLevelBG() {
            BGLv3.SendToBack();
            BGLv2.SendToBack();
            BGLv1.SendToBack();
            SetDefulaToPlayer();
        }

        private void SetDefulaToPlayer() {
            Debug.WriteLine($"BGLV1 x : {BGLv1.Location.X},y : {BGLv1.Location.Y}");
            Debug.WriteLine($"BGLV1 x : {BGLv2.Location.X},y : {BGLv2.Location.Y}");
            Debug.WriteLine($"Player x : {player.Location.X},y : {player.Location.Y}");


            BGlv1_offset = BGLv1.Size.Width / 2;
            BGlv2_offset = BGLv2.Size.Width / 2;
            BGlv3_offset = BGLv3.Size.Width / 2;
            player_offset = player.Size.Width /2;

            BGLv1.Location = new Point(player.Location.X - BGlv1_offset + player_offset, BGLv1.Location.Y);
            BGLv2.Location = new Point(player.Location.X - BGlv2_offset + player_offset, BGLv2.Location.Y);
            BGLv3.Location = new Point(player.Location.X - BGlv3_offset + player_offset, BGLv3.Location.Y);


            Debug.WriteLine($"BGLV1 x : {BGLv1.Location.X},y : {BGLv1.Location.Y}");
            Debug.WriteLine($"BGLV1 x : {BGLv2.Location.X},y : {BGLv2.Location.Y}");
            Debug.WriteLine($"Player x : {player.Location.X},y : {player.Location.Y}");
        }

        private void ParallexBG() {
            if (goRight) {
                BGLv1.Location = new Point(BGLv1.Location.X + factorParallexBG1, BGLv1.Location.Y);
                BGLv2.Location = new Point(BGLv2.Location.X + factorParallexBG2, BGLv2.Location.Y);
                BGLv3.Location = new Point(BGLv3.Location.X + factorParallexBG3, BGLv3.Location.Y);
            }
            if (goLeft)
            {
                BGLv1.Location = new Point(BGLv1.Location.X - factorParallexBG1, BGLv1.Location.Y);
                BGLv2.Location = new Point(BGLv2.Location.X - factorParallexBG2, BGLv2.Location.Y);
                BGLv3.Location = new Point(BGLv3.Location.X - factorParallexBG3, BGLv3.Location.Y);
            }

        }
    }
}
