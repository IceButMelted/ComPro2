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
        public bool IsInvincible { get; set; }
        public bool goLeft { get; set; }
        public bool goRight { get; set; }
        public bool jumping { get; set; }

        private string facingDirecttion = "right";


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

        public void MoveLeft()
        {
            Left -= Speed;
        }

        public void MoveRight()
        {
            Left += Speed;
        }

        public void Jump()
        {
            Top -= JumpPower;
        }

        public string GetFacing()
        {
            return facingDirecttion;
        }
        public void SetFacing(string direction)
        {
            facingDirecttion = direction;
        }   
    }
}
