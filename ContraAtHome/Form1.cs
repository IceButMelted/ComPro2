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

        //Enemy enemy1 = new Soldier();
        //Enemy[] enemies = new Enemy[10];
        //Enemy enm = new Soldier();
        

        public Form1()
        {
            InitializeComponent();
            
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


    }
}
