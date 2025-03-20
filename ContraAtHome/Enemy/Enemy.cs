using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ContraAtHome
{
    public abstract class Enemy : Tags
    {
        // Properties
        public int Hp;
        public int Speed;
        protected string facing = "left";

        public bool _IsAlive;
        //State And Animation
        protected int currentFrame = 0;
        protected int currentDeathFrame = 0;
        protected bool _IsFinishDeath = false;

        // Constructor
        public Enemy(int hp, int speed, params string[] initialTags)
        {
            Hp = hp;
            Speed = speed;
            _IsAlive = true;
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
            Debug.WriteLine($"Name : {Name}, HP: {Hp}, Speed: {Speed}");
            DisplayTags();
        }
        //Death Control
        public bool GetIsFinishDeath() {
            return _IsFinishDeath;
        }
        public void SetFinishDeath() { 
            _IsFinishDeath=true;
        }
        public int GetCurrentFrameDeath()
        {
            return currentDeathFrame;
        }
        public void CurrentDeathFrameIncreas()
        {
            currentDeathFrame++;
        }
        // Method to take damage
        public void TakeDamage()
        {
            Hp -= 1;
            if (Hp <= 0) 
            { 
                _IsAlive=false;
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
        public int GetCurrentFrame()
        {
            return currentFrame;
        }
        public void SetCurrentFrame(int frame)
        {
            currentFrame = frame;
        }
        public abstract void EnemyAction(Form f);



    }
}
