using System;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ContraAtHome
{
    public partial class Form1 : Form
    {
        int jumpSpeed;
        int force;
        int PlatformSpeed = 7;

        int BGlv1_offset;
        int BGlv2_offset;
        int BGlv3_offset;
        int player_offset;

        Player player;

        public Form1()
        {
            InitializeComponent();
            SetUpEnemies();
            SetUpPlatform();
            SetUpPlayer();
            SetupBG();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            if (player.jumping && force < 0)
            {
                player.jumping = false;
            }

            if (player.goLeft && player.Left > 300)
            {
                player.Left -= player.Speed;
            }
            if (player.goLeft && player.Left + (player.Width + 300) < this.ClientSize.Width)
            {
                player.Left += player.Speed;
            }

            if (player.goLeft && BorderLeft.Location.X < 300)
            {
                MoveGameElements("Left");
                ParallexBG(1, 3, 5);
            }

            if (player.goRight)
            {
                MoveGameElements("Right");
                ParallexBG(1, 3, 5);
            }

            if (player.jumping)
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
                    if (player.Bounds.IntersectsWith(x.Bounds) && !player.jumping)
                    {
                        force = 10;
                        player.Top = x.Top - player.Height;
                    }

                    x.BringToFront();
                }
            }
        }

        public void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "platform" || (string)x.Tag == "enemy" || (string)x.Tag == "Tag_Border"))
                {
                    if (direction == "Right")
                        x.Left -= PlatformSpeed;
                    if (direction == "Left")
                        x.Left += PlatformSpeed;
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.goLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                player.goRight = true;
            }
            if (e.KeyCode == Keys.Space && !player.jumping)
            {
                player.jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                player.goRight = false;
            }
            if (player.jumping)
            {
                player.jumping = false;
            }
        }

        private void player_Click(object sender, EventArgs e)
        {
            // Handle player click event if needed
        }

        private void MovingEnemy() { 
            
        }

        protected void SetupBG()
        {
            BGLv3.SendToBack();
            BGLv2.SendToBack();
            BGLv1.SendToBack();
            player.BringToFront();

            BGlv1_offset = BGLv1.Size.Width / 2;
            BGlv2_offset = BGLv2.Size.Width / 2;
            BGlv3_offset = BGLv3.Size.Width / 2;
            player_offset = player.Size.Width / 2;

            BGLv1.Location = new Point(player.Location.X - BGlv1_offset + player_offset, BGLv1.Location.Y);
            BGLv2.Location = new Point(player.Location.X - BGlv2_offset + player_offset, BGLv2.Location.Y);
            BGLv3.Location = new Point(player.Location.X - BGlv3_offset + player_offset, BGLv3.Location.Y);
        }

        public void ParallexBG(int factorParallexBG1, int factorParallexBG2, int factorParallexBG3)
        {
            if (player.goLeft)
            {
                BGLv1.Location = new Point(BGLv1.Location.X + factorParallexBG1, BGLv1.Location.Y);
                BGLv2.Location = new Point(BGLv2.Location.X + factorParallexBG2, BGLv2.Location.Y);
                BGLv3.Location = new Point(BGLv3.Location.X + factorParallexBG3, BGLv3.Location.Y);
            }
            if (player.goRight)
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
                Size = new Size(50, 50),
                BackColor = Color.FromArgb(255, 255, 121, 123),
                Location = new Point(this.ClientSize.Width / 2, 250),
            };
            this.Controls.Add(player);
        }

        private void SetUpEnemies()
        {
            int numberEnemies = 1;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "enemy")
                {
                    Enemy enemy = new Soldier(50, 3, 5)
                    {
                        Name = "Enemy" + (numberEnemies++).ToString("D2"),
                        Size = x.Size,
                        Location = x.Location,
                        BackColor = Color.Orange,
                        Tag = x.Tag
                        //Tag = new List<string> { x.Tag.ToString(), "Tag_Border" }
                    };
                    enemy.DisplayTags();
                    this.Controls.Remove(x);
                    this.Controls.Add(enemy);
                    enemy.BringToFront();
                    enemy.DisplayInfo(); //Check enemy details
                }
            }
        }

        private void SetUpPlatform()
        {
            int numberPlatforms = 1;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    Platform platform = new Platform()
                    {
                        Name = "Platform" + (numberPlatforms++).ToString("D2"),
                        Size = x.Size,
                        Location = x.Location,
                        BackColor = Color.Brown,
                        Tag = x.Tag
                    };
                    this.Controls.Remove(x);
                    this.Controls.Add(platform);
                    platform.DisplayPlatformInfo();
                    platform.BringToFront();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Handle form load event if needed
        }
    }
}
