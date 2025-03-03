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

        Dictionary<Enemy, Platform> dictEnemyPlatform = new Dictionary<Enemy, Platform>();

        Player player;

        public Form1()
        {
            InitializeComponent();
            SetUpEnemies();
            SetUpPlatform();
            SetUpPlayer();
            DebugCheckTags();
            SetupBG();
            PairPlatformAndEnemy();
            DebugCheckTags();
            DebugVisualColorPair();
            DebugDict();
        }

        // Main game timer event handler
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

            EnemyMove();
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

        // Key down event handler
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

        // Setup player
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

        // Setup enemies
        private void SetUpEnemies()
        {
            Debug.WriteLine("\n---------Enemies are setting up--------");
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
                    };
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
                    SetDictEnemyPlatform(enemy, nearestPlatform);
                    Debug.WriteLine($"Paired {enemy.Name} with {nearestPlatform.Name} as {enemy.GetTags(1)}\n");
                }
            }
            Debug.WriteLine("----- End Set UP -----");
        }

        // Debug check tags
        private void DebugCheckTags()
        {
            Debug.WriteLine("\n---------Checking Tags--------");
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "platform" || (string)x.Tag == "enemy"))
                {
                    Debug.WriteLine($"Name: {x.Name}");
                    Debug.WriteLine($"PictureBox Tag: {x.Tag}");
                }

                if (x is Tags tag)
                {
                    tag.DisplayTags();
                }
            }
            Debug.WriteLine("---------Tags Checked--------");
        }

        // Generate a random color
        private Color GetRandomColor()
        {
            Random random = new Random();
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            return Color.FromArgb(r, g, b);
        }

        // Debug visual color pair
        private void DebugVisualColorPair()
        {
            Debug.WriteLine("\n----- Visual Pair -----");
            Dictionary<string, Color> tagColorMap = new Dictionary<string, Color>();

            foreach (var pair in dictEnemyPlatform)
            {
                Enemy enemy = pair.Key;
                Platform platform = pair.Value;

                string tag = enemy.GetTags(1); // Get the tag at position 2 (index 1)
                if (!string.IsNullOrEmpty(tag) && tag != "DefaultTag2")
                {
                    if (!tagColorMap.ContainsKey(tag))
                    {
                        tagColorMap[tag] = GetRandomColor();
                    }
                    enemy.BackColor = tagColorMap[tag];
                    platform.BackColor = tagColorMap[tag];
                }
            }

            // Debug output to verify the color assignment
            foreach (var entry in tagColorMap)
            {
                Debug.WriteLine($"Tag: {entry.Key}, Color: {entry.Value}");
            }
            Debug.WriteLine("----- End Visual Pair -----");
        }


        // Set dictionary entry for enemy and platform
        private void SetDictEnemyPlatform(Enemy enemy, Platform plf)
        {
            dictEnemyPlatform.Add(enemy, plf);
        }

        // Debug dictionary entries
        private void DebugDict()
        {
            Debug.WriteLine("\n----- Debugging Dict -----");
            foreach (KeyValuePair<Enemy, Platform> P_E_p in dictEnemyPlatform)
            {
                Debug.WriteLine($"Key = {P_E_p.Key}, Value = {P_E_p.Value}");
                Debug.WriteLine($"Enemy : {P_E_p.Key.Name}, Platform : {P_E_p.Value.Name}");
                Debug.WriteLine($"Tags ene : {P_E_p.Key.GetTags(1)}, Tag plf : {P_E_p.Value.GetTags(1)}\n");
            }
            Debug.WriteLine("----- End Debugging Dict -----");
        }

        private void EnemyMove()
        {
            foreach (var pair in dictEnemyPlatform)
            {
                Enemy enemy = pair.Key;
                Platform platform = pair.Value;

                // Check if the enemy is out of the platform bounds
                if (enemy.Left < platform.Left || enemy.Left + enemy.Width > platform.Left + platform.Width)
                {
                    enemy.Speed = -enemy.Speed; // Reverse the enemy's speed
                }

                // Move the enemy
                enemy.Left += enemy.Speed;
            }
        }


    }
}
