using System;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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
        int screenWidth;
        int screenHeight;

        Dictionary<Enemy, Platform> dictEnemyPlatform = new Dictionary<Enemy, Platform>();

        Player player;

        public Form1()
        {
            InitializeComponent();
            screenWidth = this.ClientSize.Width;
            screenHeight = this.ClientSize.Height;
            SetUpEnemies();
            SetUpPlatform();
            SetUpPlayer();
            ContraToolUtility.DebugCheckTagsAllObject(this);
            SetupBG();
            PairPlatformAndEnemy();
            ContraToolUtility.DebugCheckTagsAllObject(this);
            ContraToolUtility.DebugVisualColorPair(dictEnemyPlatform);
            ContraToolUtility.DebugDict(dictEnemyPlatform);
            
        }

        // Main game timer event handler
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            ContraToolUtility.DebugEnemyBulletLocation(this);
            //Player movement
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

            if (player.goLeft && BorderLeft.Location.X < screenWidth /2 )
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
            //Player movement

            //Check if player is colliding with platforms
            //To keep player on top of the platform
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
            //End of player and platform collision

            EnemyMove();
        }

        // Key down event handler
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.goLeft = true;
                player.SetFacing("left");
            }
            if (e.KeyCode == Keys.D)
            {
                player.goRight = true;
                player.SetFacing("right");
            }
            if (e.KeyCode == Keys.W) { 
                player.SetFacing("up");
            }
            if (e.KeyCode == Keys.K && !player.jumping)
            {
                player.jumping = true;
            }
            if (e.KeyCode == Keys.J) { 
                ShootBullet(player.GetFacing());
            }
        }

        // Key up event handler
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
            if (e.KeyCode == Keys.K) { 
                player.jumping = false; 
                force = -1;
            }
            if (player.jumping)
            {
                player.jumping = false;
            }
        }

        // Player click event handler
        private void player_Click(object sender, EventArgs e)
        {
            // Handle player click event if needed
        }

        //-----------------------------------------------------//

        #region Moving Objects
        //Shoot bullet
        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = player.Left + (player.Width / 2);
            shootBullet.bulletTop = player.Top + (player.Height / 2);
            this.BringToFront();
            shootBullet.MakeBullet(this);
        }   

        // Move game elements based on direction
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

        //Mack Enemy Move
        private void EnemyMove()
        {
            foreach (var pair in dictEnemyPlatform)
            {
                Enemy enemy = pair.Key;
                Platform platform = pair.Value;

                if (enemy.Location.X+enemy.Width > 0 && enemy.Location.X < screenWidth)
                {
                    // Check if the enemy is out of the platform bounds
                    if (enemy.Left < platform.Left || enemy.Left + enemy.Width > platform.Left + platform.Width)
                    {
                        enemy.Speed = -enemy.Speed; // Reverse the enemy's speed
                    }
                    // Move the enemy
                    enemy.EnemyAction(this);
                }
                
            }
        }

        // Parallax background effect
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

        #endregion
        //-----------------------------------------------------//

        #region Setup Methods
        // Setup background elements
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

        // Setup player
        private void SetUpPlayer()
        {
            player = new Player(100, 10, 5, 10, false)
            {
                Size = new Size(60, 75),
                BackColor = Color.FromArgb(255, 255, 121, 123),
                Location = new Point(this.ClientSize.Width / 2, 250),
            };
            this.Controls.Add(player);
        }

        // Setup enemies
        private void SetUpEnemies()
        {
            Debug.WriteLine("\n---------Enemies are setting up--------");
            int numberEnemies = 1;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "enemy")
                {
                    Enemy enemy;
                    int randNum = ContraToolUtility.RandomNumberRange(1, 11);
                    if (randNum < 5)
                    {
                        enemy = new ShootingSoldier(50, 3, 5)
                        {
                            Name = "Enemy" + (numberEnemies++).ToString("D2"),
                            Size = x.Size,
                            Location = x.Location,
                            BackColor = Color.Orange,
                            Tag = x.Tag
                        };
                        enemy.ReplaceTag(0, "ShootingSoldier");
                    }
                    else
                    {
                        enemy = new RunningSoldier(50, 3, 5)
                        {
                            Name = "Enemy" + (numberEnemies++).ToString("D2"),
                            Size = x.Size,
                            Location = x.Location,
                            BackColor = Color.Orange,
                            Tag = x.Tag
                        };
                        enemy.ReplaceTag(0, "RunningSoldier");
                    }
                    this.Controls.Remove(x);
                    this.Controls.Add(enemy);
                    enemy.BringToFront();
                    enemy.DisplayInfo(); //Check enemy details
                }
            }
            Debug.WriteLine("---------Enemies set up--------");
        }

        // Setup platforms
        private void SetUpPlatform()
        {
            Debug.WriteLine("\n---------Platforms are setting up--------");
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
            Debug.WriteLine("---------Platforms set up--------");
        }

        // Form load event handler
        private void Form1_Load(object sender, EventArgs e)
        {
            // Handle form load event if needed
        }

        // Pair each enemy with the nearest platform
        private void PairPlatformAndEnemy()
        {
            Debug.WriteLine("\n----- SetUp Pair -----");
            int pairNumber = 1;
            List<Platform> platforms = new List<Platform>();
            List<Enemy> enemies = new List<Enemy>();

            // Collect all platforms and enemies
            foreach (Control x in this.Controls)
            {
                if (x is Platform platform)
                {
                    platforms.Add(platform);
                }
                else if (x is Enemy enemy)
                {
                    enemies.Add(enemy);
                }
            }

            // Pair each enemy with the nearest platform
            foreach (var enemy in enemies)
            {
                Platform nearestPlatform = null;
                double nearestDistance = double.MaxValue;

                foreach (var platform in platforms)
                {
                    // Calculate the distance with priority to the X-axis
                    double distance = Math.Abs(enemy.Location.X - platform.Location.X) * 2 + Math.Abs(enemy.Location.Y - platform.Location.Y);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestPlatform = platform;
                    }
                }

                if (nearestPlatform != null)
                {
                    enemy.ReplaceTag(1, $"P_E_{pairNumber:D2}");
                    nearestPlatform.ReplaceTag(1, $"P_E_{pairNumber++:D2}");
                    nearestPlatform.ReplaceTag(0,"PlatformEnemy");
                    SetDictEnemyPlatform(enemy, nearestPlatform);
                    Debug.WriteLine($"Paired {enemy.Name} with {nearestPlatform.Name} as {enemy.GetTags(1)}\n");
                }
            }
            Debug.WriteLine("----- End Set UP -----");
        }

        // Set dictionary entry for enemy and platform
        private void SetDictEnemyPlatform(Enemy enemy, Platform plf)
        {
            dictEnemyPlatform.Add(enemy, plf);
        }

        #endregion
        //-----------------------------------------------------//

        


    }
}
