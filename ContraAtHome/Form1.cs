using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media; 
using System.IO;
using System.Windows.Shell;
using System.Windows.Media;
using ColorDrawing = System.Drawing.Color;
using System.Windows.Automation.Text;


namespace ContraAtHome
{
    public partial class Form1 : Form
    {
        #region Variable
        // Constants
        private const int PLAYER_JUMP_FORCE = 10;
        private const int PLAYER_JUMP_SPEED = 15;

        private const int PLATFORM_SPEED = 7;
        private const int SHOOT_COOLDOWN = 20;

        //Counter 
        private int enemyCounter = 0;
        private int tutConter = 0;

        //Scene
        private bool _IsCreatedDeathScene = false;
        private bool _IsCreatedWinSccen = false;
        private bool _IsLockScreen = false;
        private bool _IsShowTutTxt = true;
        //Player 

        // Game state
        private int force;
        private int currentShootCooldown = 0;
        private bool _IsBossStage = false;

        //vairable for cal frame
        private int frameCounter = 0;
        private int enemyAnimationCounter = 0;

        private int frameDeathDuration = 30;
        private int frameDeathCounter = 0;
        private bool deathforward = true;

        //Player State
        private bool _isOnGround = false;
        private bool _isFalling = false;

        //Sprites List Load
        private string lastFacingDirection = Direction.Right;
        //[Player Sprite]
        // 1:Idle // 2:Running // 3:Runnning // 4:Facingup // 5:Jumping // 6:Falling
        private Bitmap[][] playerSpriteRight = new Bitmap[6][];
        private Bitmap[][] playerSpriteLeft = new Bitmap[6][];
        private Bitmap[] playerSpriteDeath = new Bitmap[6];
        //[Enemy Sprite]
        // 1: Running-Rigt 2: Running-Left 3:Shooting-Right 4: Shoothn-Left
        private Bitmap[][] EnemySprite = new Bitmap[4][];
        private Bitmap[][] EnemySpriteDeath = new Bitmap[4][];
        //[Boss]
        //1:normal 2:hurt 3:death
        private Bitmap[][] BossSprite= new Bitmap[3][];

        //Sound 
        private MediaPlayer BossATK_Sound    = new MediaPlayer();
        private MediaPlayer BossDead_Sound   = new MediaPlayer();
        private MediaPlayer BossHit_Sound    = new MediaPlayer();
        private MediaPlayer EnemyDead_Sound  = new MediaPlayer();
        private MediaPlayer GameOver_Sound   = new MediaPlayer();
        private MediaPlayer Gun_Sound        = new MediaPlayer();
        private MediaPlayer Jump_Sound       = new MediaPlayer();
        private MediaPlayer PlayerDead_Sound = new MediaPlayer();
        private MediaPlayer PlayerHit_Sound  = new MediaPlayer();
        private MediaPlayer BGM_Sound        = new MediaPlayer();

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

        public Enemy enemyBoss;
        public GunBoss gunBoss1;
        private bool _IsBossSpawn;
        private bool _IsBossAlive;
        private bool _IsBosHurt = false;
        private bool _BossAction = false;


        #endregion
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

            //update cache
            UpdateCachedCollectionsIfNeeded();

            //initail boss
            CreateBoss();
            CreateGunBoss1();

            //Sprite Loader
            PlayerSpriteLoader();
            EnemySpriteLoader();
            BossSpriteLoader();


            //LoadSound
            SoundLoader();
            BGM_Sound.MediaEnded += (sender, e) =>
            {
                // When the music ends, restart it from the beginning
                BGM_Sound.Position = TimeSpan.Zero;
                BGM_Sound.Play();
            };

            


            //Set Key
            KeyPic.Location = new Point(this.ClientSize.Width/2 - (KeyPic.Width * 4),KeyPic.Location.Y);

            //Text
            txt_Live.Text = "Live : " + (player.Live-1);
            txt_Live.BringToFront();

            // Debug info
            ContraToolUtility.DebugCheckTagsAllObject(this);
            //ContraToolUtility.DebugVisualColorPair(enemyPlatformPairs);
            ContraToolUtility.DebugDict(enemyPlatformPairs);
            Debug.WriteLine($" width :{screenWidth}");
            Debug.WriteLine($" height : {screenHeight}");

            

        }

        // Main game loop
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            if (!BGM_Sound.IsBuffering)
            {
                BGM_Sound.Play();
            }
            if (player._isDeath) {
                AnimationPlayerDeath();
            }
            else if (!player.IsGameOver())
            {
                // Only update collections when needed
                UpdateCachedCollectionsIfNeeded();

                HandlePlayerLogic();
                HandleCollisions();

                //Boss And Enemy
                ProcessEnemyActions();

                // Update shoot cooldown
                if (currentShootCooldown <= SHOOT_COOLDOWN)
                    currentShootCooldown++;

                AnimationPlayerAlive();

                StartBossAction();



            }
            else
            {
                //Freeze everythings and show GameOverScreen 
                //then requested to quit or restart(restartProgram)
                if (!_IsCreatedDeathScene) {
                    CreatDeathScene(this);
                }
                
            }
            if(_IsShowTutTxt == false)
                txt_tut.Visible = false;
            
        }

        public void CreatDeathScene(Form form)
        {
            // Create a panel that will serve as our rectangle
            Panel blueRectangle = new Panel
            {
                // Set the size to match the client area of the form
                Size = form.ClientSize,
                // Set the location to top-left of the client area
                Location = new Point(0, 0),
                // Set background color to blue
                BackgroundImage = new Bitmap("./Sprites/Ui/DeathScreen.png"),
                // Make sure the panel is on top of other controls
                Dock = DockStyle.None,
            };

            // Add the panel to the form
            form.Controls.Add(blueRectangle);

            // Bring the panel to the front so it appears on top of everything
            blueRectangle.BringToFront();

            // Optional: Make the panel resize with the form
            form.Resize += (sender, e) =>
            {
                blueRectangle.Size = form.ClientSize;
            };

            _IsCreatedDeathScene = true;
        }

        private void CreateBoss()
        {
            enemyBoss = new Boss(30,10, "Boss");
            enemyBoss.Location = new Point(BossPicBox.Location.X, BossPicBox.Location.Y);
            enemyBoss.Size = BossPicBox.Size;
            enemyBoss.BackColor = ColorDrawing.White;
            Controls.Add(enemyBoss);
            enemyBoss.BringToFront();
            Controls.Remove(BossPicBox);

        }
        private void CreateGunBoss1() 
        { 
            gunBoss1 = new GunBoss();
            gunBoss1.Location = new Point(BossGun1.Location.X, BossGun1.Location.Y);
            gunBoss1.Size = BossGun1.Size;
            gunBoss1.BackColor = ColorDrawing.Gold;
            Controls.Add(gunBoss1);
            gunBoss1.BringToFront();
            Controls.Remove(BossGun1);
        }

        #region Boss Stage
        private void StartBossAction() {
            if (!_IsBossSpawn && _IsLockScreen)
            {
                BG.BackgroundImage = Properties.Resources.BG2;
                _IsBossSpawn = true;
            }
            if (_IsBossSpawn && enemyBoss.Location.Y < 0)
            {
                PlayeBossAnimation();
                enemyBoss.Top += 1;
            }
            if (_IsBossSpawn && gunBoss1.Location.Y < 0)
            {
                gunBoss1.Top += 1;
            }
            if (enemyBoss.Location.Y >= 0)
            {
                _BossAction = true;
            }
        }

        #endregion

        #region Sound
        private void SoundLoader()
        {
            string resourcePath;
            resourcePath = Path.GetFullPath("./Sounds/SFX/BossAttack" + ".wav");
            BossATK_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/BossDead" + ".wav");
            BossDead_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/BossHit" + ".wav");
            BossHit_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/EnemyDead" + ".wav");
            EnemyDead_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/GameOver" + ".wav");
            GameOver_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/GunSound" + ".wav");
            Gun_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/Jump" + ".wav");
            Jump_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/PlayerDead" + ".wav");
            PlayerDead_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/SFX/PlayerHit" + ".wav");
            PlayerHit_Sound.Open(new System.Uri(resourcePath));
            resourcePath = Path.GetFullPath("./Sounds/BGM/MainTheme.mp3");
            BGM_Sound.Open(new System.Uri(resourcePath));


        }

        private void SFXPlayer(MediaPlayer mdp)
        {
            mdp.Stop();
            mdp.Position = TimeSpan.Zero;
            mdp.Play();
        }
        #endregion

        #region Cache Methods
        private void UpdateCachedCollectionsIfNeeded()
        {
            if (_needsPlayerBulletUpdate || frameCounter % 30 == 0)
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

            if (_needsEnemyBulletUpdate || frameCounter % 30 == 0)
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

            if (!_IsLockScreen)
            {
                if (_needsEnemyUpdate)
                {
                    activeEnemies.Clear();
                    foreach (Control control in Controls)
                    {
                        if (control is Enemy enemy && control.Tag?.ToString() == "enemy" && enemy._IsAlive)
                        {
                            activeEnemies.Add(enemy);
                        }
                    }
                    _needsEnemyUpdate = false;
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
                            tag == "EnemyBullet") ||
                            tag == "key")
                        {
                            movableElements.Add(control);
                        }
                    }
                    _needsMovableElementUpdate = false;
                }
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

            if (!_IsLockScreen)
            {
                if (control is PictureBox && (
                    tag == "platform" ||
                    tag == "enemy" ||
                    tag == "Tag_Border" ||
                    tag == "EnemyBullet"))
                {
                    _needsMovableElementUpdate = true;
                }
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

            if (!_IsLockScreen)
            {
                if (control is PictureBox && (
                    tag == "platform" ||
                    tag == "enemy" ||
                    tag == "Tag_Border" ||
                    tag == "EnemyBullet"))
                {
                    _needsMovableElementUpdate = true;
                }
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
            //Loop to initialize sprite array for death only
            for (int frameIndex = 0; frameIndex < 6; frameIndex++) {
                string framePath = $"./Sprites/Player/Dead/Dead_00{frameIndex}.png";
                playerSpriteDeath[frameIndex] = new Bitmap(framePath);
            }

            // Set the initial player image to the first frame of the idle animation
            // This ensures that when the game starts, the player appears in a neutral state
            player.Image = playerSpriteRight[0][0];
            player.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void BossSpriteLoader()
        {
            BossSprite[0] = new Bitmap[12];
            BossSprite[1] = new Bitmap[6];
            BossSprite[2] = new Bitmap[6];
            //load normal
            for (int frameIndex = 0; frameIndex < 12; frameIndex++)
            {
                string framePath = "";
                if (frameIndex < 10)
                    framePath = $"./Sprites/Boss/Normal/Normal_00{frameIndex}.png";
                else if (frameIndex >= 10)
                    framePath = $"./Sprites/Boss/Normal/Normal_0{frameIndex}.png";
                BossSprite[0][frameIndex] = new Bitmap(framePath);
            }
            //load hurt
            for (int frameIndex = 0; frameIndex < 6; frameIndex++)
            {
                string framePath = $"./Sprites/Boss/Hurt/Hurt_00{frameIndex}.png";
                BossSprite[1][frameIndex] = new Bitmap(framePath);
            }
            //load death
            for (int frameIndex = 0; frameIndex < 6; frameIndex++)
            {
                string framePath = $"./Sprites/Boss/Death/Death_00{frameIndex}.png";
                BossSprite[2][frameIndex] = new Bitmap(framePath);
            }

            enemyBoss.Image = BossSprite[0][0];
            enemyBoss.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void EnemySpriteLoader()
        {
            string[] animationTypes = { "RunningSoldier", "ShootingSoldier" };

            for (int animIndex = 0; animIndex < 2; animIndex++)
            {
                EnemySprite[animIndex * 2] = new Bitmap[6];
                EnemySprite[animIndex * 2 + 1] = new Bitmap[6];

                for (int frameIndex = 0; frameIndex < 6; frameIndex++)
                {
                    string framePath = $"./Sprites/Enemy/{animationTypes[animIndex]}/{animationTypes[animIndex]}_00{frameIndex}.png";
                    EnemySprite[animIndex * 2][frameIndex] = new Bitmap(framePath);

                    EnemySprite[animIndex * 2 + 1][frameIndex] = (Bitmap)EnemySprite[animIndex * 2][frameIndex].Clone();
                    EnemySprite[animIndex * 2 + 1][frameIndex].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }

            //initail Sprite For Enemy
            foreach (var enemy in activeEnemies)
            {
                if (enemy is ShootingSoldier)
                {
                    enemy.Image = EnemySprite[2][0];
                    enemy.SizeMode = PictureBoxSizeMode.StretchImage;
                    //Debug.WriteLine("enemy set shooting image");
                }
                else
                {
                    enemy.Image = EnemySprite[0][0];
                    enemy.SizeMode = PictureBoxSizeMode.StretchImage;
                    //Debug.WriteLine("enemy set running");
                }

            }

            string[] animationDeathTypes = { "MeleeDead", "ShooterDead" };
            //initail Sprite Enemy death
            for (int animIndex = 0; animIndex < 2; animIndex++)
            {
                EnemySpriteDeath[animIndex * 2] = new Bitmap[6];
                EnemySpriteDeath[animIndex * 2 + 1] = new Bitmap[6];

                for (int frameIndex = 0; frameIndex < 6; frameIndex++)
                {
                    string framePath = $"./Sprites/Enemy/Dead/{animationDeathTypes[animIndex]}/{animationDeathTypes[animIndex]}_00{frameIndex}.png";
                    EnemySpriteDeath[animIndex * 2][frameIndex] = new Bitmap(framePath);

                    EnemySpriteDeath[animIndex * 2 + 1][frameIndex] = (Bitmap)EnemySpriteDeath[animIndex * 2][frameIndex].Clone();
                    EnemySpriteDeath[animIndex * 2 + 1][frameIndex].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
        }

        private void AnimationPlayerAlive()
        {
            // Increment and reset frame counter
            if (++frameCounter >= 60)
                frameCounter = 0;

            // Only update animation if it's time to change frames or direction changed
            bool shouldUpdateFrame = (frameCounter % player.GetTickChange() == 0) || lastFacingDirection != player.GetFacing() || keysPressed.Contains(Keys.K) || _isFalling;
            if (!shouldUpdateFrame)
                return;

            // Store direction for easier access
            bool isDirectionRight = (player.GetFacing() == Direction.Right || player.GetFacing() == Direction.UpRight || (player.GetFacing() == Direction.Up && (lastFacingDirection == Direction.Right || lastFacingDirection == Direction.UpRight) ));
            //Debug.WriteLine(isDirectionRight);

            // Determine animation state based on priority
            int animationState;

            if (_isFalling)
            {
                animationState = 5; // Falling animation
            }
            else if (player.jumping)
            {
                animationState = 4; // Jumping animation
            }
            else if (player.up)
            {
                if (player.goLeft || player.goRight)
                    animationState = 2; // Moving up-left or up-right
                else
                    animationState = 3; // Facing up
            }
            else if (player.goLeft || player.goRight)
            {
                animationState = 1; // Running animation
            }
            else
            {
                animationState = 0; // Idle animation
            }

            // Special case for direction inconsistency
            if (player.up && player.goLeft)
            {
                player.Image = playerSpriteLeft[animationState][player.GetCurrentFrame()];
            }
            else if (player.up && player.goRight)
            {
                player.Image = playerSpriteRight[animationState][player.GetCurrentFrame()];
            }
            // Use directional sprites based on facing direction
            else if (isDirectionRight)
            {
                player.Image = playerSpriteRight[animationState][player.GetCurrentFrame()];
                //Debug.WriteLine("right is here");
            }
            else
            {
                player.Image = playerSpriteLeft[animationState][player.GetCurrentFrame()];
                //Debug.WriteLine("left is here");
            }

            // Update frame counter and handle wrapping
            player.SetCurrentFrame((player.GetCurrentFrame() + 1) % (player.GetTickChange() + 1));

            // Update last facing direction
            lastFacingDirection = player.GetFacing();
        }

        private void AnimationPlayerDeath()
        {
            if (deathforward && frameDeathCounter < 6)
            {
                if (frameCounter % 5 == 0)
                {
                    player.Image = playerSpriteDeath[frameDeathCounter];
                    frameDeathCounter++;
                }
                frameCounter++;
                if(frameDeathCounter == 6) deathforward = false;
            }
            else if (!deathforward && frameDeathCounter > 1 && !player.IsGameOver())
            {
                if (frameCounter % 5 == 0)
                {
                    frameDeathCounter--;
                    player.Image = playerSpriteDeath[frameDeathCounter];
                    
                }
                frameCounter++;
            }
            else
            {
                player._isDeath = false;
                deathforward = true;
                //frameDeathCounter = 0;
            }
        }

        private void PlayeBossAnimation() {

            if (!_IsBosHurt)
            {
                if (frameCounter % 10 == 0)
                {
                    enemyBoss.SetCurrentFrame((enemyBoss.GetCurrentFrame() + 1) % 12);
                    enemyBoss.Image = BossSprite[0][enemyBoss.GetCurrentFrame()];
                }
            }
            else {
                if (frameCounter % 3 == 0)
                {
                    enemyBoss.SetCurrentFrame((enemyBoss.GetCurrentFrame() + 1) % 6);
                    enemyBoss.Image = BossSprite[1][enemyBoss.GetCurrentFrame()];
                    if(enemyBoss.GetCurrentFrame() > 4)
                        _IsBosHurt = false;
                }
            }

            if (!enemyBoss._IsAlive && frameCounter % 5 == 0) {
                enemyBoss.SetCurrentFrame((enemyBoss.GetCurrentFrame() + 1) % 6);
                enemyBoss.Image = BossSprite[2][enemyBoss.GetCurrentFrame()];
                if (enemyBoss.GetCurrentFrame() > 5)
                {
                    enemyBoss.SetFinishDeath();
                }
            }
        }

        
        #endregion

        #region Player Movement & Controls


        private void HandlePlayerLogic()
        {
            // Handle player Invincible state
            if (player.IsInvincible)
            {
                if (player.GetInvicibleCounter() < player.GetInvicibleDuration())
                {
                    player.BackColor = ColorDrawing.White; 
                    player.SetInvicibleCounter(player.GetInvicibleCounter() + 1);
                }
                else
                {
                    player.BackColor = ColorDrawing.Transparent; 
                    player.IsInvincible = false;
                    player.SetInvicibleCounter(0);
                }
            }

            //ColletedKey and Eneble Boss Stage
            if (player.Bounds.IntersectsWith(KeyPic.Bounds) && enemyCounter < 1)
            {
                //Look Screen
                _IsLockScreen = true;
                KeyPic.Dispose();
            }

            // Previous ground state (for fall detection)
            bool wasOnGround = _isOnGround; // Fixed pointer syntax issue

            // Reset ground check for this frame
            _isOnGround = false;

            // Platform collision - check if player is on ground
            foreach (Platform platform in playerPlatforms)
            {
                if (player._IsShouldOnPlatform)
                {
                    if (player.Bounds.IntersectsWith(platform.Bounds))
                    {
                        _isOnGround = true;
                        force = PLAYER_JUMP_FORCE;
                        player.Top = platform.Top - player.Height + 1;
                        //keysPressed.Remove(Keys.K);
                        _isFalling = false;
                    }
                }
            }

            // Fall detection
            if (wasOnGround && !_isOnGround && !player.jumping)
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
                    player._IsShouldOnPlatform = true;
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

            // Handle horizontal movement
            if (player.goLeft)
            {
                if (_IsLockScreen || BorderLeft.Location.X > -1)
                {
                    if (player.Left > 0) // Move player if not at left edge
                        player.Left -= player.Speed;
                }
                else if (player.Left + player.Width > screenWidth / 2)
                {
                    player.Left -= player.Speed;
                }
                else if (BorderLeft.Location.X < screenWidth)
                {
                    // Scroll world with parallax
                    MoveGameElements(Direction.Left);
                    //UpdateParallaxBackground(1, 3, 5);
                }
                return;
            }

            if (player.goRight)
            {
                if (_IsLockScreen || BorderRight.Location.X < screenWidth)
                {
                    if (player.Left + player.Width/2 < screenWidth) // Move player if not at right edge
                        player.Left += player.Speed;

                }
                else if (player.Left + player.Width/2 < screenWidth / 2) 
                {
                    player.Left += player.Speed;
                }
                else if (BorderRight.Location.X > screenWidth)
                {
                    // Scroll world with parallax
                    MoveGameElements(Direction.Right);
                    //UpdateParallaxBackground(1, 3, 5);
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
            if (_IsShowTutTxt && keysPressed.Count > 0) {
                _IsShowTutTxt = false;
            }
            if (!player.IsGameOver())
            {
                // Reset movement flags to avoid carryover
                player.goLeft = false;
                player.goRight = false;
                player.up = false;


                // Calculate active movement directions
                bool pressUp = keysPressed.Contains(Keys.W);
                bool pressLeft = keysPressed.Contains(Keys.A);
                bool pressRight = keysPressed.Contains(Keys.D);

                // Jump handling
                if (keysPressed.Contains(Keys.K) && !player.jumping && !_isFalling && _isOnGround)
                {
                    //player Jump Sound
                    SFXPlayer(Jump_Sound);
                    player._IsShouldOnPlatform = false;
                    player.jumping = true;
                }

                // Shoot handling
                if (keysPressed.Contains(Keys.J) && currentShootCooldown > SHOOT_COOLDOWN)
                {
                    //Play player shoot -SOUND-
                    SFXPlayer(Gun_Sound);
                    ShootBullet(player);
                    currentShootCooldown = 0;
                }

                // Handle direction facing based on key combinations
                if (pressUp)
                {
                    if (pressLeft)
                    {
                        player.SetFacing(Direction.UpLeft);
                        player.goLeft = true;
                        player.up = true;
                    }
                    else if (pressRight)
                    {
                        player.SetFacing(Direction.UpRight);
                        player.goRight = true;
                        player.up = true;
                    }
                    else
                    {
                        if (lastFacingDirection == Direction.Right || lastFacingDirection == Direction.UpRight)
                            player.SetFacing(Direction.UpRight);
                        if (lastFacingDirection == Direction.Left || lastFacingDirection == Direction.UpLeft)
                            player.SetFacing(Direction.UpLeft);
                        player.up = true;
                    }
                }
                else if (pressLeft && pressRight)
                {
                    // Neutralize opposing directions
                    // Direction remains unchanged
                }
                else if (pressLeft)
                {
                    player.SetFacing(Direction.Left);
                    player.goLeft = true;
                }
                else if (pressRight)
                {
                    player.SetFacing(Direction.Right);
                    player.goRight = true;
                }
                else
                {
                    if (lastFacingDirection == Direction.UpRight)
                        player.SetFacing(Direction.Right);
                    if (lastFacingDirection == Direction.UpLeft)
                        player.SetFacing(Direction.Left);
                }
            }
            else
            {
                if (keysPressed.Contains(Keys.Enter))
                {
                    Debug.WriteLine("RestartGame");
                    Application.Restart();
                }
                if (keysPressed.Contains(Keys.Q)) 
                {
                    Debug.WriteLine("Quit Game");
                    Application.Exit();
                }
            }


        }

        #endregion

        #region Game Logic
        private void HandleCollisions()
        {
            if (player._isPlayerAlive)
            {
                List<Control> controlsToRemove = new List<Control>();

                // Check player bullet collisions with enemies
                foreach (var bullet in playerBullets)
                {
                    if (!_IsLockScreen) // bossStage
                    {
                        foreach (var enemy in activeEnemies)
                        {
                            if (enemy._IsAlive)
                            {
                                if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                                {
                                    enemy.TakeDamage();
                                    if (enemy.Hp <= 0)
                                    {//Play Die sound -SOUND-
                                        SFXPlayer(EnemyDead_Sound);
                                        
                                    }
                                    else
                                    {//Play Hit sound -SOUND- 
                                        SFXPlayer(PlayerHit_Sound);
                                    }
                                    controlsToRemove.Add(bullet);
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        if (bullet.Bounds.IntersectsWith(enemyBoss.Bounds))
                        {
                            enemyBoss.TakeDamage();
                            _IsBosHurt = true;  
                            if (enemyBoss.Hp <= 0)
                            {//Play Die sound -SOUND-
                                SFXPlayer(EnemyDead_Sound);
                                enemyBoss._IsAlive = false;
                            }
                            else
                            {//Play Hit sound -SOUND- 
                                SFXPlayer(BossHit_Sound);
                            }
                            controlsToRemove.Add(bullet);
                            break;
                        }
                    }
                }
                if (!player.IsInvincible)
                {
                    // Check enemy bullet collisions with player
                    foreach (var bullet in enemyBullets)
                    {
                        if (bullet.Bounds.IntersectsWith(player.Bounds))
                        {
                            //Play Playe Hit sound -SOUND-
                            SFXPlayer(PlayerHit_Sound);
                            player.DecreasLive();
                            player.IsInvincible = true;
                            player._isDeath = true;
                            controlsToRemove.Add(bullet);
                            txt_Live.Text = "Live : " + (player.Live - 1);
                            // Player hit logic here
                        }
                    }
                    
                    if (!_IsLockScreen) // BossStage not use
                    {
                        //check enemy collisions with player
                        foreach (var enmy in activeEnemies)
                        {
                            if (enmy._IsAlive)
                            {
                                if (enmy.Bounds.IntersectsWith(player.Bounds))
                                {
                                    //Play Playe Hit sound -SOUND-
                                    SFXPlayer(PlayerHit_Sound);
                                    player.DecreasLive();
                                    player.IsInvincible = true;
                                    player._isDeath = true;
                                    txt_Live.Text = "Live : " + (player.Live - 1);
                                }
                            }
                        }
                    }
                }
                // Remove objects outside the loop
                foreach (Control control in controlsToRemove)
                {
                    RemoveControlWithCacheUpdate(control);
                }
            }
        }

        // Process enemy actions with optimized collections
        private void ProcessEnemyActions()
        {
            if (enemyCounter > 0)
            {
                // Increment and reset animation counter
                if (++enemyAnimationCounter >= 10)
                    enemyAnimationCounter = 0;

                bool updateAnimation = (enemyAnimationCounter == 0);

                foreach (var enemy in activeEnemies)
                {
                    if (enemy._IsAlive)
                    {
                        if (!enemyPlatformPairs.TryGetValue(enemy, out Platform platform))
                            continue;

                        // Skip if enemy is off-screen
                        if (enemy.Right <= 0 || enemy.Left >= screenWidth)
                            continue;

                        // Common animation update logic
                        if (updateAnimation)
                        {
                            int animationType;

                            if (enemy is ShootingSoldier s)
                            {
                                s.SetFacing(s.Location.X > player.Location.X ? Direction.Left : Direction.Right);
                                animationType = enemy.GetFacing() == Direction.Right ? 2 : 3;
                            }
                            else
                                animationType = enemy.GetFacing() == Direction.Right ? 0 : 1;

                            enemy.SetCurrentFrame((enemy.GetCurrentFrame() + 1) % 6);
                            enemy.Image = EnemySprite[animationType][enemy.GetCurrentFrame()];
                        }

                        // Running soldier specific logic
                        if (enemy is RunningSoldier)
                        {
                            // Check platform bounds
                            if (enemy.Left < platform.Left || enemy.Right > platform.Right)
                                enemy.Speed = -enemy.Speed;
                        }

                        // Shooting soldier specific logic
                        if (enemy is ShootingSoldier shooter && shooter.CanShooting)
                        {
                            // Face the player
                            shooter.SetFacing(shooter.Location.X > player.Location.X ? Direction.Left : Direction.Right);
                            shooter.CanShooting = false;
                            ShootBullet(shooter, shooter.BulletSpeed);
                            //Play Enemy shoot -SOUND-
                            SFXPlayer(Gun_Sound);
                            Gun_Sound.Stop();
                            Gun_Sound.Position = TimeSpan.Zero;
                            Gun_Sound.Play();
                            shooter.ResetShootCooldown();
                        }

                        // Process common enemy actions last
                        enemy.EnemyAction(this);
                    }
                    //is death
                    if (enemy._IsAlive == false && enemy.GetIsFinishDeath() == false)
                    {
                        if (frameCounter % 2 == 0)
                        {

                            if (enemy.GetCurrentFrameDeath() > 5)
                            {
                                enemy.SetFinishDeath();
                                //activeEnemies.Remove(enemy);
                            }
                            else
                            {
                                int a = (enemy is RunningSoldier) ? 0 : 2;
                                int b = enemy.GetFacing() == Direction.Left ? 1 : 0;
                                int spriteIndex = a + b;
                                enemy.Image = EnemySpriteDeath[spriteIndex][enemy.GetCurrentFrameDeath()];
                                enemy.CurrentDeathFrameIncreas();
                            }

                        }
                    }
                    if (enemy._IsAlive == false && enemy.GetIsFinishDeath() == true)
                    {
                        RemoveControlWithCacheUpdate(enemy);
                        enemyCounter--;
                        Debug.WriteLine($"Enemy in scene {enemyCounter}");
                    }
                }
            }
            //boss Action
            else if(_BossAction)
            {
                if (enemyBoss._IsAlive)
                {
                    enemyBoss.EnemyAction(this);
                    gunBoss1.MoveWithPlayer(player);
                    if (frameCounter % gunBoss1.GetFrameDurationBetween() == 0 && gunBoss1.IsBurstShoot())
                    {
                        ShootBullet(gunBoss1, 15, "basic");
                    }
                    else if (frameCounter % 50 == 0)
                    {
                        ShootBullet(gunBoss1, 15, "basic");
                    }
                    PlayeBossAnimation();
                }
                else
                {
                    if (!enemyBoss._IsAlive)
                    {
                        if (frameCounter % 2 == 0)
                        {
                            if (enemyBoss.GetCurrentFrameDeath() > 5)
                            {
                                enemyBoss.SetFinishDeath();
                                //activeEnemies.Remove(enemy);
                            }
                            else
                            {
                                enemyBoss.Image = BossSprite[2][enemyBoss.GetCurrentFrameDeath()];
                                enemyBoss.CurrentDeathFrameIncreas();
                            }
                        }
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
        private void ShootBullet(GunBoss gunBoss, int bulletSpeed, string bulletType = "basic")
        {
            if (gunBoss == null) return;

            ColorDrawing bulletColor = bulletType == "explosive" ? ColorDrawing.Purple : ColorDrawing.Red;

            Bullet bullet = new Bullet("EnemyBullet", bulletSpeed,
                new Point(gunBoss.Left + gunBoss.Width / 2, gunBoss.Top + gunBoss.Height / 2), bulletColor, screenWidth, screenHeight)
            {
                Direction = gunBoss.facing
            };
            bullet.Size = new Size(20, 20);
            AddControlWithCacheUpdate(bullet);
            bullet.BringToFront();
        }

        private void ShootBullet(Enemy shooter, int bulletSpeed, string bulletType = "basic")
        {
            if (shooter == null || !shooter._IsAlive) return;

            ColorDrawing bulletColor = bulletType == "explosive" ? ColorDrawing.Red : ColorDrawing.Purple;

            Bullet bullet = new Bullet("EnemyBullet", bulletSpeed,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), bulletColor, screenWidth, screenHeight)
            {
                Direction = shooter.GetFacing()
            };

            AddControlWithCacheUpdate(bullet);
            bullet.BringToFront();
        }

        private void ShootBullet(Player shooter)
        {
            if (shooter == null) return;

            Bullet bullet = new Bullet("PlayerBullet", 15,
                new Point(shooter.Left + shooter.Width / 2, shooter.Top + shooter.Height / 2), ColorDrawing.Yellow, screenWidth, screenHeight)
            {
                Direction = shooter.GetFacing()
            };

            AddControlWithCacheUpdate(bullet);
            bullet.BringToFront();
            player.BringToFront();
        }

        #endregion

        #region Setup Methods

        private void SetUpPlayer()
        {
            player = new Player(5, 10, 7, 10, false)
            {
                Size = new Size(60, 80),
                BackColor = ColorDrawing.Transparent,
                Location = new Point(ClientSize.Width / 2, 430),
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            Controls.Add(player);
            player.Location = new Point(ClientSize.Width / 2 - player.Width/2, 430);
        }

        private void SetupBackground()
        {
            // Set proper z-order
            BGLv3.SendToBack();
            BGLv2.SendToBack();
            BGLv1.SendToBack();
            BG.SendToBack();
            player.BringToFront();

            //// Calculate offsets
            //bgLayer1Offset = BGLv1.Width / 2;
            //bgLayer2Offset = BGLv2.Width / 2;
            //bgLayer3Offset = BGLv3.Width / 2;
            //playerOffset = player.Width / 2;

            // Position background layers
            BGLv1.Location = new Point(0, 0);
            BGLv2.Location = new Point(0, 0);
            BGLv3.Location = new Point(0, 0);

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
                        BackgroundImage = control.BackgroundImage,
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
            enemyCounter++;
            Enemy enemy;

            // Create appropriate enemy type
            if (ContraToolUtility.RandomNumberRange(1, 11) < 5)
                enemy = new ShootingSoldier(1, 3) { Name = $"Enemy{enemyNumber:D2}" };
            else
                enemy = new RunningSoldier(1, 3) { Name = $"Enemy{enemyNumber:D2}" };

            enemy.Size = control.Size;
            enemy.Location = control.Location;
            enemy.BackColor = ColorDrawing.Transparent;
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
        public const string UpRight = "up-right";
        public const string UpLeft = "up-left";
    }

    public static class PlayerState
    {
        public const string Idle = "idle";
        public const string Running = "running";
        public const string Jumping = "jumping";
        public const string Falling = "falling";
    }
}
