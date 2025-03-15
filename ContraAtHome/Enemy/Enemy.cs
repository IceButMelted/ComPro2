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
        public bool IsAlive = true;
        protected string facing = "left";

        enum enemyType { EnemyMoving, EnemyShooting }

        // Constructor
        public Enemy(int hp, int speed, params string[] initialTags)
        {
            Hp = hp;
            Speed = speed;
            IsAlive = true;
            Size = new Size(40, 50); // Default size, can be overridden
            BackColor = Color.Red; // Default color, can be overridden

            // Set initial tags
            //this.ReplaceTag(0, );
            for (int i = 1; i < 3; i++)
            {
                this.ReplaceTag(i, i < initialTags.Length ? initialTags[i] : $"DefaultTag{i + 1}");
            }
        }

        // Method to display enemy details
        public void DisplayInfo()
        {
            Debug.WriteLine($"Name : {Name}, HP: {Hp}, Speed: {Speed}, Is Alive: {IsAlive}");
            DisplayTags();
        }

        // Method to attack
        public void Attack(Player player)
        {
            if (Bounds.IntersectsWith(player.Bounds))
            {
                Console.WriteLine($"Player hit! Player HP: {player.Hp}");
            }
        }

        // Method to take damage
        public void TakeDamage()
        {
            Hp -= 1;
            if (Hp <= 0)
            {
                IsAlive = false;
                Debug.WriteLine("Enemy defeated!");
            }
        }

        public void SetFacing(string direction)
        {
            facing = direction;
        }
        public string GetFacing()
        {
            return facing;
        }

        public abstract void EnemyAction(Form f);



    }
}
