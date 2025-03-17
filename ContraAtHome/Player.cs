using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ContraAtHome
{
    public class Player : PictureBox
    {
        // Properties
        public int Hp { get; set; }
        public int JumpPower { get; set; }
        public int Speed { get; set; }
        public int Dmg { get; set; }
        
        public bool up { get; set; }
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public bool jumping { get; set; }

        private string facingDirecttion = "right";

        //state And Animation
        private string state = "idle";
        private int tickChange = 5;
        private int currentFrame = 0;

        private int invicibleDuration = 20; // 05 sec at 60 fps
        private int invicibleCounter = 0;
        public bool IsInvincible { get; set; }


        // Constructor
        public Player(int hp, int jumpPower, int speed, int dmg, bool isInvincible)
        {
            Hp = hp;
            JumpPower = jumpPower;
            Speed = speed;
            Dmg = dmg;
            IsInvincible = isInvincible;
        }

        // Method to display player details
        public void DisplayInfo()
        {
            Debug.WriteLine($"HP: {Hp}, Jump Power: {JumpPower}, Speed: {Speed}, Damage: {Dmg}, Is Invincible: {IsInvincible}");
        }

        public void DecreasHP() {
            Hp--;
        }

        public string GetFacing()
        {
            return facingDirecttion;
        }
        public void SetFacing(string direction)
        {
            facingDirecttion = direction;
        }   

        public string GetState()
        {
            return state;
        }
        public void SetState(string newState)
        {
            state = newState;
        }
        public void ResetFrame()
        {
            currentFrame = 0;
        }
        public int GetCurrentFrame()
        {
            return currentFrame;
        }
        public void SetCurrentFrame(int frame)
        {
            currentFrame = frame;
        }

        public int GetTickChange()
        {
            return tickChange;
        }

        public int GetInvicibleDuration()
        {
            return invicibleDuration;
        }

        public int GetInvicibleCounter() { 
            return invicibleCounter;
        }

        public void SetInvicibleCounter(int counter) {
            invicibleCounter = counter;  
        }

    }
}
