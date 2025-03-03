using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ContraAtHome
{
    public abstract class Enemy : Tags
    {
        // Properties
        public int Hp { get; set; }
        public int Speed { get; set; }
        public int Dmg { get; set; }
        public bool IsAlive { get; set; }

        // Constructor
        public Enemy(int hp, int speed, int dmg, params string[] initialTags)
        {
            Hp = hp;
            Speed = speed;
            Dmg = dmg;
            IsAlive = true;
            Size = new Size(40, 50); // Default size, can be overridden
            BackColor = Color.Red; // Default color, can be overridden

           
            for (int i = 0; i < 3; i++)
            {
                this.ReplaceTag(i, i < initialTags.Length ? initialTags[i] : $"DefaultTag{i + 1}");
            }
        }

        // Method to display enemy details
        public void DisplayInfo()
        {
            Debug.WriteLine($"Name : {Name}, HP: {Hp}, Speed: {Speed}, Damage: {Dmg}, Is Alive: {IsAlive}");
            DisplayTags();
        }

        // Method to attack
        public void Attack(Player player)
        {
            if (Bounds.IntersectsWith(player.Bounds))
            {
                player.Hp -= Dmg;
                Console.WriteLine($"Player hit! Player HP: {player.Hp}");
            }
        }

        // Method to take damage
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                IsAlive = false;
                Console.WriteLine("Enemy defeated!");
            }
        }
    }
}
