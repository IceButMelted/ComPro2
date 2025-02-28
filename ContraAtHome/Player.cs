using System;

namespace ContraAtHome
{
    public class Player
    {
        // Properties
        public int Hp { get; set; }
        public int JumpPower { get; set; }
        public int Speed { get; set; }
        public int Dmg { get; set; }
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
            Console.WriteLine($"HP: {Hp}, Jump Power: {JumpPower}, Speed: {Speed}, Damage: {Dmg}, Is Invincible: {IsInvincible}");
        }
    }
}
