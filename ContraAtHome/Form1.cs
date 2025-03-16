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

        private int frameCounter = 0;

        //Player State
        private bool _isOnGround = false;
        private bool _isFalling = false;

        //Sprites List Load
        private string lastFacingDirection = Direction.Right;
        // 1:Idle // 2:Running // 3:Runnning // 4:Facing // 5:Jumping // 6:Falling
        private Bitmap[][] playerSpriteRight = new Bitmap[6][];
        private Bitmap[][] playerSpriteLeft = new Bitmap[6][];
        // 1: Running-Rigt 2: Running-Left 3:Shooting-Right 4: Shoothn-Left
        private Bitmap[][] EnemySprite = new Bitmap[4][];


        // Background parallax
        private int bgLayer1Offset;
        private int bgLayer2Offset;
        private int bgLayer3Offset;
        private int playerOffset;
        private readonly int screenWidth;
        private readonly int screenHeight;

        //flags to track when collections need updating
        private bool _needsPlayerBulletUpdate = true;
        private bool _needsEnemyUpdate = true;
        private bool _needsEnemyBulletUpdate = true;
        private bool _needsMovableElementUpdate = true;

        // Game objects
        private readonly Dictionary<Enemy, Platform> enemyPlatformPairs = new Dictionary<Enemy, Platform>();
        private List<Platform> playerPlatforms = new List<Platform>();
        private Player player;

        //List for better performance
        private List<PictureBox> playerBullets = new List<PictureBox>();
        private List<Enemy> activeEnemies = new List<Enemy>();
        private List<PictureBox> enemyBullets = new List<PictureBox>();
        private List<Control> movableElements = new List<Control>();

        // Key press tracking
        private HashSet<Keys> keysPressed = new HashSet<Keys>();

        public Form1()
        {
            InitializeComponent();
            screenWidth = ClientSize.Width;
            screenHeight = ClientSize.Height;

            // Initialize game
            SetUpGameObjects();
            ContraToolUtility.DebugLocationPlatform(playerPlatforms);
            SetUpPlayer();
            SetupBackground();

            //Sprite Loader
            PlayerSpriteLoader();

            // Debug info
            ContraToolUtility.DebugCheckTagsAllObject(this);
            ContraToolUtility.DebugVisualColorPair(enemyPlatformPairs);
            ContraToolUtility.DebugDict(enemyPlatformPairs);
            Debug.WriteLine($" width :{screenWidth}");
            Debug.WriteLine($" height : {screenHeight}");



        }

        // Main game loop
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            // Only update collections when needed
            UpdateCachedCollectionsIfNeeded();

            HandlePlayerMovement();
            HandleCollisions();
            ProcessEnemyActions();

            // Update shoot cooldown
            if (currentShootCooldown <= SHOOT_COOLDOWN)
                currentShootCooldown++;

            TestAnimationPlayer(0);
        }

        #region Cache Methods
        private void UpdateCachedCollectionsIfNeeded()
        {
            if (_needsPlayerBulletUpdate)
            {
                playerBullets.Clear();
                foreach (Control control in Controls)
                {
                    if (control is PictureBox pictureBox && control.Tag?.ToString() == "PlayerBullet")
                    {
                        playerBullets.Add(pictureBox);
                    }
                }
                _needsPlayerBulletUpdate = false;
            }

            if (_needsEnemyUpdate)
            {
                activeEnemies.Clear();
                foreach (Control control in Controls)
                {
                    if (control is Enemy enemy && control.Tag?.ToString() == "enemy" && enemy.IsAlive)
                    {
                        activeEnemies.Add(enemy);
                    }
                }
                _needsEnemyUpdate = false;
            }

            if (_needsEnemyBulletUpdate)
            {
                enemyBullets.Clear();
                foreach (Control control in Controls)
                {
                    if (control is PictureBox pictureBox && control.Tag?.ToString() == "EnemyBullet")
                    {
                        enemyBullets.Add(pictureBox);
                    }
                }
                _needsEnemyBulletUpdate = false;
            }

            if (_needsMovableElementUpdate)
            {
                movableElements.Clear();
                foreach (Control control in Controls)
                {
                    string tag = control.Tag?.ToString();
                    if (control is PictureBox && (
                        tag == "platform" ||
                        tag == "enemy" ||
                        tag == "Tag_Border" ||
                        tag == "EnemyBullet"))
                    {
                        movableElements.Add(control);
                    }
                }
                _needsMovableElementUpdate = false;
            }
        }

        private void AddControlWithCacheUpdate(Control control)
        {
            Controls.Add(control);

            // Invalidate relevant cache based on control type
            string tag = control.Tag?.ToString();
            if (control is PictureBox && tag == "PlayerBullet")
                _needsPlayerBulletUpdate = true;
            else if (control is Enemy && tag == "enemy")
                _needsEnemyUpdate = true;
            else if (control is PictureBox && tag == "EnemyBullet")
                _needsEnemyBulletUpdate = true;

            if (control is PictureBox && (
                tag == "platform" ||
                tag == "enemy" ||
                tag == "Tag_Border" ||
                tag == "EnemyBullet"))
            {
                _needsMovableElementUpdate = true;
            }
        }

        private void RemoveControlWithCacheUpdate(Control control)
        {
            Controls.Remove(control);

            // Invalidate relevant cache based on control type
            string tag = control.Tag?.ToString();
            if (control is PictureBox && tag == "PlayerBullet")
                _needsPlayerBulletUpdate = true;
            else if (control is Enemy && tag == "enemy")
            {
                _needsEnemyUpdate = true;
                enemyPlatformPairs.Remove(control as Enemy);
            }
            else if (control is PictureBox && tag == "EnemyBullet")
                _needsEnemyBulletUpdate = true;

            if (control is PictureBox && (
                tag == "platform" ||
                tag == "enemy" ||
                tag == "Tag_Border" ||
                tag == "EnemyBullet"))
            {
                _needsMovableElementUpdate = true;
            }

            control.Dispose();
        }
        #endregion

        #region Animation

        private void PlayerSpriteLoader()
        {
            // 1:Idle // 2:Running // 3:Running Face Up // 4:Facing Up // 5:Jumping // 6:Falling

            // Array holding different animation states for the player
            string[] animationTypes = { "Idle", "Running", "RunFaceUp", "FaceUp", "Jumping", "Falling" };

            // Loop to initialize sprite arrays for both left and right directions
            for (int animIndex = 0; animIndex < animationTypes.Length; animIndex++)
            {
                playerSpriteRight[animIndex] = new Bitmap[6];
                playerSpriteLeft[animIndex] = new Bitmap[6];

                for (int frameIndex = 0; frameIndex < 6; frameIndex++)
                {
                    string framePath = $"./Sprites/Player/{animationTypes[animIndex]}/{animationTypes[animIndex]}_00{frameIndex}.png";

                    playerSpriteRight[animIndex][frameIndex] = new Bitmap(framePath);
                    playerSpriteLeft[animIndex][frameIndex] = (Bitmap)playerSpriteRight[animIndex][frameIndex].Clone();
                    playerSpriteLeft[animIndex][frameIndex].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }

            // Set the initial player image to the first frame of the idle animation
            // This ensures that when the game starts, the player appears in a neutral state
            player.Image = playerSpriteRight[0][0];
            player.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void EnemySpriteLoader()
        {
            string[] animationType = { "RunningSoldier", "ShootingSoldier" };

        }

        private void playerSpriteLoaderCache (){}

        private void TestAnimationPlayer(int state)
        {
            frameCounter += 1;
            if (frameCounter > 59)
            {
                frameCounter = 0;
            }
            if ((frameCounter % player.GetTickChange() == 0) || lastFacingDirection != player.GetFacing())
            {
                lastFacingDirection = player.GetFacing();
                if (player.GetCurrentFrame() > 5)
                {
                    player.ResetFrame();
                }

                // Create a new copy of the original image
                Bitmap originalImage = playerSpriteRight[state][player.GetCurrentFrame()];
                Bitmap flippedImage = new Bitmap(originalImage);

                // Only flip if facing left
                if (player.GetFacing() == Direction.Left)
                {
                    flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                // Set the potentially flipped copy
                player.Image = flippedImage;
                player.SetCurrentFrame(player.GetCurrentFrame() + 1);
            }
        }
        private void AnimationTimerEvent()
        {
            // Player animation
            if (player.GetState() == PlayerState.Idle)
            {
                player.Image = playerSpriteRight[0][0];
            }
            else if (player.GetState() == PlayerState.Running)
            {
                player.Image = playerSpriteRight[1][player.GetCurrentFrame()];
                player.Image.RotateFlip(player.GetFacing() == Direction.Left ? RotateFlipType.RotateNoneFlipX : RotateFlipType.RotateNoneFlipNone);
                player.SetCurrentFrame(player.GetCurrentFrame() + 1);
                if (player.GetCurrentFrame() > 3)
                {
                    player.ResetFrame();
                }
            }
            else if (player.GetState() == PlayerState.Jumping)
            {
                player.Image = playerSpriteRight[2][0];
            }
            else if (player.GetState() == PlayerState.Falling)
            {
                player.Image = playerSpriteRight[3][0];
            }
        }

        #endregion

        #region Player Movement & Controls

        private void HandlePlayerMovement()
        {
            //Previous ground state (for fall detection)
            bool _wasOnGround = _isOnGround;

            // Reset ground check for this frame
            _isOnGround = false;

            // Platform collision - check if player is on ground
            foreach (Platform platform in playerPlatforms)
            {
                if (player.Bounds.IntersectsWith(platform.Bounds))
                {
                    _isOnGround = true;
                    force = PLAYER_JUMP_FORCE;
                    player.Top = platform.Top - player.Height + 1;
                    _isFalling = false;
                }

            }

            // Fall detection
            if (_wasOnGround && !_isOnGround && !player.jumping)
            {
                _isFalling = true;
            }

            // Handle jumping physics
            if (player.jumping)
            {
                force -= 1;

                // End jump if force is depleted
                if (force < 0)
                {
                    player.jumping = false;
                    _isFalling = true;
                }
            }

            // Apply vertical movement only when not on ground or jumping
            if (!_isOnGround || player.jumping)
            {
                int jumpSpeed = player.jumping ? -PLAYER_JUMP_SPEED : PLAYER_JUMP_SPEED;
                player.Top += jumpSpeed;
            }

            if (_isFalling && _isOnGround)
            {
                _isFalling = false;
            }

            // Horizontal movement
            if (player.goLeft)
            {
                if (player.Left > screenWidth)
                    player.Left -= player.Speed;
                else if (BorderLeft.Location.X < screenWidth / 3)
                {
                    MoveGameElements(Direction.Left);
                    UpdateParallaxBackground(1, 3, 5);
                }
                return;
            }

            if (player.goRight)
            {
                if (player.Right > screenWidth)
                    player.Left += player.Speed;
                else if (BorderRight.Location.X > screenWidth - screenWidth / 3)
                {
                    MoveGameElements(Direction.Right);
                    UpdateParallaxBackground(1, 3, 5);
                }
                return;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            keysPressed.Add(e.KeyCode);
            HandleKeyPresses();
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
            HandleKeyPresses();
        }

        private void HandleKeyPresses()
        {
            if (keysPressed.Contains(Keys.K) && !player.jumping && !_isFalling && _isOnGround)
            {
                //Debug.WriteLine("Jumping");
                player.jumping = true;
            }

            if (keysPressed.Contains(Keys.J) && currentShootCooldown > SHOOT_COOLDOWN)
            {
                ShootBullet(player);
                currentShootCooldown = 0;
            }

            if (keysPressed.Contains(Keys.W))
            {
                player.SetFacing(Direction.Up);
                //Debug.WriteLine("Facing up");
            }

            player.goLeft = keysPressed.Contains(Keys.A);
            player.goRight = keysPressed.Contains(Keys.D);

            //go up and left or right But Facing up Multi key press
            if (keysPressed.Contains(Keys.W) && keysPressed.Contains(Keys.A))
            {
                player.SetFacing(Direction.Up);
                //Debug.WriteLine("Facing UP");
                return;
            }
            else if (keysPressed.Contains(Keys.W) && keysPressed.Contains(Keys.D))
            {
                player.SetFacing(Direction.Up);
                //Debug.WriteLine("Facing UP");
                return;
            }

            //Go Left or Right single key press
            if (player.goLeft && player.goRight)
            {
                player.goLeft = player.goRight = false;
                return;
            }
            if (player.goLeft)
            {
                player.SetFacing(Direction.Left);
                //Debug.WriteLine("Facing left");
                return;
            }
            else if (player.goRight)
            {
                player.SetFacing(Direction.Right);
                //Debug.WriteLine("Facing right");
                return;
            }


        }

        #endregion

        #region Game Logic

        private void HandleCollisions()
        {
            List<Control> controlsToRemove = new List<Control>();

            // Check player bullet collisions with enemies
            foreach (var bullet in playerBullets)
            {
                foreach (var enemy in activeEnemies)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        enemy.TakeDamage();
                        if (!enemy.IsAlive)
                        {
                            controlsToRemove.Add(enemy);
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
                RemoveControlWithCacheUpdate(control);
            }
        }

        // Process enemy actions with optimized collections
        private void ProcessEnemyActions()
        {
            foreach (var enemy in activeEnemies)
            {
                if (enemyPlatformPairs.TryGetValue(enemy, out Platform platform))
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
        }

        #endregion

        #region Game Elements
        // Move game elements with optimized collection
        private void MoveGameElements(string direction)
        {
            int moveAmount = direction == Direction.Right ? -PLATFORM_SPEED : PLATFORM_SPEED;

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

        // Modified ShootBullet method to use the new AddControlWithCacheUpdate
        private void ShootBullet(Enemy shooter, int bulletSpeed, string bulletType = "basic")
        {
            if (shooter == null || !shooter.IsAlive) return;

            Color bulletColor = bulletType == "explosive" ? Color.Red : Color.Yellow;

            Bullet bullet = new Bullet("EnemyBullet", bulletSpeed,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), bulletColor, screenWidth, screenHeight)
            {
                Direction = shooter.GetFacing()
            };

            bullet.BackColor = Color.Purple;

            AddControlWithCacheUpdate(bullet);
            bullet.BringToFront();
        }

        private void ShootBullet(Player shooter)
        {
            if (shooter == null) return;

            Bullet bullet = new Bullet("PlayerBullet", 10,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), Color.Yellow, screenWidth, screenHeight)
            {
                Direction = shooter.GetFacing()
            };

            AddControlWithCacheUpdate(bullet);
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
                Location = new Point(ClientSize.Width / 2, 430),
                BackgroundImageLayout = ImageLayout.Stretch,
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

            ContraToolUtility.DebugLocationEnemy(enemies);

            // Pair enemies with platforms
            PairEnemiesWithPlatforms(enemies, platforms, pairNumber);
            Debug.WriteLine("---------Game objects setup complete--------");
        }

        private void PairEnemiesWithPlatforms(List<Enemy> enemies, List<Platform> platforms, int startPairNumber)
        {
            int pairNumber = startPairNumber;

            foreach (var enemy in enemies)
            {
                // Calculate enemy position using Location.X and Location.Y
                int enemyCenterX = enemy.Location.X + (enemy.Width / 2);
                int enemyBottom = enemy.Location.Y + enemy.Height;

                // Find platforms that are below the enemy (without LINQ)
                Platform selectedPlatform = null;
                int closestVerticalDistance = int.MaxValue;
                int closestHorizontalDistance = int.MaxValue;
                bool foundPlatformBelow = false;

                // First pass: find platforms below the enemy
                foreach (Platform platform in platforms)
                {
                    if (platform.Location.Y >= enemyBottom)
                    {
                        int platformCenterX = platform.Location.X + (platform.Width / 2);
                        int verticalDistance = platform.Location.Y - enemyBottom;
                        int horizontalDistance = Math.Abs(platformCenterX - enemyCenterX);

                        // Check if this platform is better than the current best
                        bool isBetterPlatform = false;

                        // If we found no platform yet, this one is better
                        if (selectedPlatform == null)
                        {
                            isBetterPlatform = true;
                        }
                        // If this platform is closer vertically, it's better
                        else if (verticalDistance < closestVerticalDistance)
                        {
                            isBetterPlatform = true;
                        }
                        // If vertical distance is the same, compare horizontal distance
                        else if (verticalDistance == closestVerticalDistance &&
                                 horizontalDistance < closestHorizontalDistance)
                        {
                            isBetterPlatform = true;
                        }

                        if (isBetterPlatform)
                        {
                            selectedPlatform = platform;
                            closestVerticalDistance = verticalDistance;
                            closestHorizontalDistance = horizontalDistance;
                            foundPlatformBelow = true;
                        }
                    }
                }

                // If no platform is below, find the nearest platform overall (fallback)
                if (!foundPlatformBelow)
                {
                    closestVerticalDistance = int.MaxValue;
                    closestHorizontalDistance = int.MaxValue;

                    foreach (Platform platform in platforms)
                    {
                        int platformCenterX = platform.Location.X + (platform.Width / 2);
                        int verticalDistance = Math.Abs(platform.Location.Y - enemyBottom);
                        int horizontalDistance = Math.Abs(platformCenterX - enemyCenterX);

                        // Check if this platform is better than the current best
                        bool isBetterPlatform = false;

                        // If we found no platform yet, this one is better
                        if (selectedPlatform == null)
                        {
                            isBetterPlatform = true;
                        }
                        // If this platform is closer vertically, it's better
                        else if (verticalDistance < closestVerticalDistance)
                        {
                            isBetterPlatform = true;
                        }
                        // If vertical distance is the same, compare horizontal distance
                        else if (verticalDistance == closestVerticalDistance &&
                                 horizontalDistance < closestHorizontalDistance)
                        {
                            isBetterPlatform = true;
                        }

                        if (isBetterPlatform)
                        {
                            selectedPlatform = platform;
                            closestVerticalDistance = verticalDistance;
                            closestHorizontalDistance = horizontalDistance;
                        }
                    }

                    Debug.WriteLine($"No platform directly below enemy {enemy.Name}, using nearest available platform: {selectedPlatform?.Name}");
                }

                if (selectedPlatform != null)
                {
                    string tagId = $"P_E_{pairNumber++:D2}";
                    enemy.ReplaceTag(1, tagId);
                    selectedPlatform.ReplaceTag(1, tagId);
                    selectedPlatform.ReplaceTag(0, "PlatformEnemy");
                    enemyPlatformPairs[enemy] = selectedPlatform;

                    int platformCenterX = selectedPlatform.Location.X + (selectedPlatform.Width / 2);
                    Debug.WriteLine($"Paired {enemy.Name} with {selectedPlatform.Name} - " +
                        $"X distance: {Math.Abs(platformCenterX - enemyCenterX)}, " +
                        $"Y distance: {Math.Abs(selectedPlatform.Location.Y - enemyBottom)}, " +
                        $"Y relation: {(selectedPlatform.Location.Y >= enemyBottom ? "below" : "above")} enemy");
                }
            }
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

    public static class PlayerState
    {
        public const string Idle = "idle";
        public const string Running = "running";
        public const string Jumping = "jumping";
        public const string Falling = "falling";
    }
}
