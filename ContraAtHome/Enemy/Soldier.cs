using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public class Soldier : Enemy
    {
        private bool CanMoveAndShoot { get; set; }
        public Soldier() : base(health: 50, damage: 10, speed: 2.0f) { }

        public Soldier(bool canMoveNShoot) : base(health: 50, damage: 10, speed: 2.0f)
        {
            CanMoveAndShoot = canMoveNShoot;
        }
        public Soldier(int health, int damage, float speed, bool canMoveNShoot) : base(health, damage, speed)
        {
            CanMoveAndShoot = canMoveNShoot;
        }

        protected override void Attack(Player p)
        {
            Console.WriteLine("Soldier shoots!");
        }
        protected override void Move()
        {
            if (CanMoveAndShoot)
            {
                Console.WriteLine("Soldier moves and shoots!");
            }
            else
            {
                Console.WriteLine("Soldier moves!");
            }
        }

    }
}
