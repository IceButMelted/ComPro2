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

        //State And Animation
        protected string state = "idle";
        protected int currentFrame = 0;

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

        //Method Face Direction
        public void SetFacing(string direction)
        {
            facing = direction;
        }
        public string GetFacing()
        {
            return facing;
        }

        //state And Animation Methods
        public void SetState(string newState)
        {
            state = newState;
        }
        public string GetState()
        {
            return state;
        }
        public int GetCurrentFrame()
        {
            return currentFrame;
        }
        public void SetCurrentFrame(int frame)
        {
            currentFrame = frame;
        }
        public void ResetFrame()
        {
            currentFrame = 0;
        }

        public abstract void EnemyAction(Form f);



    }
}
