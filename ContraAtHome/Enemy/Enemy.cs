using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public abstract class Enemy : PictureBox
    {
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public float Speed { get; protected set; }

        public Enemy(int health, int damage, float speed)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }

        protected abstract void Attack(Player player);
        protected virtual void Move()
        {
            Console.WriteLine("Enemy is moving...");
        }
        protected void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Die();
            }
        }

        protected void Die()
        {
            Console.WriteLine("Enemy defeated!");
        }



    }
}
