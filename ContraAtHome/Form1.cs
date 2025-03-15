using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ContraAtHome
{
    public partial class Form1 : Form
    {
        // Constants
        private const int PLAYER_JUMP_FORCE = 10;
        private const int PLAYER_JUMP_SPEED = 10;

        private const int PLATFORM_SPEED = 7;
        private const int SHOOT_COOLDOWN = 10;

        // Game state
        private int force;
        private int currentShootCooldown = 0;

        // Background parallax
        private int bgLayer1Offset;
        private int bgLayer2Offset;
        private int bgLayer3Offset;
        private int playerOffset;
        private readonly int screenWidth;
        private readonly int screenHeight;

        // Game objects
        private readonly Dictionary<Enemy, Platform> enemyPlatformPairs = new Dictionary<Enemy, Platform>();
        private List<Platform> playerPlatforms = new List<Platform>();
        private Player player;

        public Form1()
        {
            InitializeComponent();
            screenWidth = ClientSize.Width;
            screenHeight = ClientSize.Height;

            // Initialize game
            SetUpGameObjects();
            SetUpPlayer();
            SetupBackground();

            // Debug info
            ContraToolUtility.DebugCheckTagsAllObject(this);
            ContraToolUtility.DebugVisualColorPair(enemyPlatformPairs);
            ContraToolUtility.DebugDict(enemyPlatformPairs);
        }

        // Main game loop
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            // Vertical movement
            if (player.jumping)
                force -= 1;
            int jumpSpeed = player.jumping ? -PLAYER_JUMP_SPEED : PLAYER_JUMP_SPEED;

            player.Top += jumpSpeed;
            HandlePlayerMovement();
            HandleCollisions();
            ProcessEnemyActions();

            // Update shoot cooldown
            if (currentShootCooldown <= SHOOT_COOLDOWN)
                currentShootCooldown++;
        }

        #region Player Movement & Controls

        private void HandlePlayerMovement()
        {
            

            // Platform collision
            foreach (Platform platform in playerPlatforms)
            {
                if (player.Bounds.IntersectsWith(platform.Bounds) && !player.jumping)
                {
                    force = PLAYER_JUMP_FORCE;
                    player.Top = platform.Top - player.Height;
                }
                
            }

            // Handle jumping
            if (player.jumping && force < 0)
            {
                player.jumping = false;
            }

            // Horizontal movement
            if (player.goLeft)
            {
                if (player.Left > screenWidth / 2)
                    player.Left -= player.Speed;
                else if (BorderLeft.Location.X < screenWidth / 2)
                {
                    MoveGameElements(Direction.Left);
                    UpdateParallaxBackground(1, 3, 5);
                }
            }

            if (player.goRight)
            {
                MoveGameElements(Direction.Right);
                UpdateParallaxBackground(1, 3, 5);
            }
            
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    player.goLeft = true;
                    player.SetFacing(Direction.Left);
                    break;
                case Keys.D:
                    player.goRight = true;
                    player.SetFacing(Direction.Right);
                    break;
                case Keys.W:
                    player.SetFacing(Direction.Up);
                    break;
                case Keys.K:
                    if (!player.jumping)
                        player.jumping = true;
                    break;
                case Keys.J:
                    if (currentShootCooldown > SHOOT_COOLDOWN)
                    {
                        ShootBullet(player);
                        currentShootCooldown = 0;
                    }
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    player.goLeft = false;
                    break;
                case Keys.D:
                    player.goRight = false;
                    break;
                case Keys.K:
                    player.jumping = false;
                    force = -1;
                    break;
            }
        }

        #endregion

        #region Game Logic

        private void HandleCollisions()
        {
            List<Control> controlsToRemove = new List<Control>();

            // Use LINQ to efficiently process collisions
            var playerBullets = Controls.OfType<PictureBox>()
                .Where(x => x.Tag?.ToString() == "PlayerBullet");

            var enemies = Controls.OfType<PictureBox>()
                .Where(x => x.Tag?.ToString() == "enemy");

            var enemyBullets = Controls.OfType<PictureBox>()
                .Where(x => x.Tag?.ToString() == "EnemyBullet");

            // Check player bullet collisions with enemies
            foreach (var bullet in playerBullets)
            {
                foreach (var enemy in enemies)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        if (enemy is Enemy targetEnemy)
                        {
                            targetEnemy.TakeDamage();
                            if (!targetEnemy.IsAlive)
                            {
                                controlsToRemove.Add(enemy);
                            }
                        }
                        controlsToRemove.Add(bullet);
                        break;
                    }
                }
            }

            // Check enemy bullet collisions with player
            foreach (var bullet in enemyBullets)
            {
                if (bullet.Bounds.IntersectsWith(player.Bounds))
                {
                    controlsToRemove.Add(bullet);
                    // Player hit logic here
                }
            }

            // Remove objects outside the loop
            foreach (Control control in controlsToRemove)
            {
                Controls.Remove(control);
                if (control is Enemy enemy)
                {
                    enemyPlatformPairs.Remove(enemy);
                }
                control.Dispose();
            }
        }

        private void ProcessEnemyActions()
        {
            foreach (var (enemy, platform) in enemyPlatformPairs)
            {
                // Skip if enemy is off-screen
                if (enemy.Right <= 0 || enemy.Left >= screenWidth)
                    continue;

                // Check platform bounds
                if (enemy.Left < platform.Left || enemy.Right > platform.Right)
                    enemy.Speed = -enemy.Speed;

                // Process enemy actions
                enemy.EnemyAction(this);

                // Handle shooting enemies
                if (enemy is ShootingSoldier shooter && shooter.CanShooting)
                {
                    // Face the player
                    shooter.SetFacing(shooter.Location.X > player.Location.X ? Direction.Left : Direction.Right);
                    shooter.CanShooting = false;
                    ShootBullet(shooter, shooter.BulletSpeed);
                    shooter.ResetShootCooldown();
                }
            }
        }

        #endregion

        #region Game Elements

        private void MoveGameElements(string direction)
        {
            int moveAmount = direction == Direction.Right ? -PLATFORM_SPEED : PLATFORM_SPEED;

            // Use LINQ to get all movable elements
            var movableElements = Controls.OfType<Control>()
                .Where(x => x is PictureBox &&
                           (x.Tag?.ToString() == "platform" ||
                            x.Tag?.ToString() == "enemy" ||
                            x.Tag?.ToString() == "Tag_Border" ||
                            x.Tag?.ToString() == "EnemyBullet"));

            foreach (var element in movableElements)
            {
                element.Left += moveAmount;
            }
        }

        private void UpdateParallaxBackground(int layer1Factor, int layer2Factor, int layer3Factor)
        {
            int directionMultiplier = player.goRight ? -1 : 1;

            if (player.goLeft || player.goRight)
            {
                BGLv1.Left += directionMultiplier * layer1Factor;
                BGLv2.Left += directionMultiplier * layer2Factor;
                BGLv3.Left += directionMultiplier * layer3Factor;
            }
        }

        private void ShootBullet(Enemy shooter, int bulletSpeed, string bulletType = "basic")
        {
            if (shooter == null || !shooter.IsAlive) return;

            Color bulletColor = bulletType == "explosive" ? Color.Red : Color.Yellow;

            Bullet bullet = new Bullet("EnemyBullet", bulletSpeed,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), bulletColor)
            {
                Direction = shooter.GetFacing()
            };

            Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void ShootBullet(Player shooter)
        {
            if (shooter == null) return;

            Bullet bullet = new Bullet("PlayerBullet", 10,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), Color.Yellow)
            {
                Direction = shooter.GetFacing()
            };

            Controls.Add(bullet);
            bullet.BringToFront();
        }

        #endregion

        #region Setup Methods

        private void SetUpPlayer()
        {
            player = new Player(100, 10, 7, 10, false)
            {
                Size = new Size(60, 75),
                BackColor = Color.FromArgb(255, 255, 121, 123),
                Location = new Point(ClientSize.Width / 2, 250),
            };
            Controls.Add(player);
        }

        private void SetupBackground()
        {
            // Set proper z-order
            BGLv3.SendToBack();
            BGLv2.SendToBack();
            BGLv1.SendToBack();
            player.BringToFront();

            // Calculate offsets
            bgLayer1Offset = BGLv1.Width / 2;
            bgLayer2Offset = BGLv2.Width / 2;
            bgLayer3Offset = BGLv3.Width / 2;
            playerOffset = player.Width / 2;

            // Position background layers
            BGLv1.Location = new Point(player.Left - bgLayer1Offset + playerOffset, BGLv1.Top);
            BGLv2.Location = new Point(player.Left - bgLayer2Offset + playerOffset, BGLv2.Top);
            BGLv3.Location = new Point(player.Left - bgLayer3Offset + playerOffset, BGLv3.Top);
        }

        private void SetUpGameObjects()
        {
            Debug.WriteLine("\n---------Setting up game objects--------");

            List<Enemy> enemies = new List<Enemy>();
            List<Platform> platforms = new List<Platform>();

            playerPlatforms.Clear();

            int enemyCount = 1;
            int platformCount = 1;
            int pairNumber = 1;

            // Replace PictureBoxes with game objects
            foreach (Control control in Controls.OfType<PictureBox>().ToList())
            {
                string tag = control.Tag?.ToString();

                if (tag == "enemy")
                {
                    // Create appropriate enemy type
                    Enemy enemy = CreateEnemy(control, enemyCount++);
                    enemies.Add(enemy);
                }
                else if (tag == "platform")
                {
                    // Create platform
                    Platform platform = new Platform()
                    {
                        Name = $"Platform{platformCount++:D2}",
                        Size = control.Size,
                        Location = control.Location,
                        BackColor = Color.Brown,
                        Tag = "platform"
                    };

                    Controls.Remove(control);
                    Controls.Add(platform);
                    platform.BringToFront();
                    platforms.Add(platform);
                    playerPlatforms.Add(platform);
                }
            }

            // Pair enemies with platforms
            PairEnemiesWithPlatforms(enemies, platforms, pairNumber);
            Debug.WriteLine("---------Game objects setup complete--------");
        }

        private Enemy CreateEnemy(Control control, int enemyNumber)
        {
            Enemy enemy;

            // Create appropriate enemy type
            if (ContraToolUtility.RandomNumberRange(1, 11) < 5)
                enemy = new ShootingSoldier(3, 3) { Name = $"Enemy{enemyNumber:D2}" };
            else
                enemy = new RunningSoldier(5, 3) { Name = $"Enemy{enemyNumber:D2}" };

            enemy.Size = control.Size;
            enemy.Location = control.Location;
            enemy.BackColor = Color.Orange;
            enemy.Tag = "enemy";

            Controls.Remove(control);
            Controls.Add(enemy);
            enemy.BringToFront();

            return enemy;
        }

        private void PairEnemiesWithPlatforms(List<Enemy> enemies, List<Platform> platforms, int startPairNumber)
        {
            int pairNumber = startPairNumber;

            foreach (var enemy in enemies)
            {
                // Find nearest platform
                Platform nearestPlatform = platforms
                    .OrderBy(p => Math.Abs(enemy.Left - p.Left) * 2 + Math.Abs(enemy.Top - p.Top))
                    .FirstOrDefault();

                if (nearestPlatform != null)
                {
                    string tagId = $"P_E_{pairNumber++:D2}";

                    enemy.ReplaceTag(1, tagId);
                    nearestPlatform.ReplaceTag(1, tagId);
                    nearestPlatform.ReplaceTag(0, "PlatformEnemy");
                    enemyPlatformPairs[enemy] = nearestPlatform;

                    Debug.WriteLine($"Paired {enemy.Name} with {nearestPlatform.Name}");
                }
            }
        }

        #endregion
    }

    // Static class for direction constants
    public static class Direction
    {
        public const string Left = "left";
        public const string Right = "right";
        public const string Up = "up";
        public const string Down = "down";
    }
}