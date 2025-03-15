using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ContraAtHome
{
    public class ShootingSoldier : Enemy
    {
        int coolDown = 45;
        int currentCoolDown = 0;
        // Constructor
        public ShootingSoldier(int hp, int speed, params string[] initialTags) : base(hp, speed, initialTags)
        {

        }

        public override void EnemyAction(Form f)
        {
            //Debug.WriteLine($"Shooting Soldier Action {this.IsAlive}");
            if (IsAlive == true)
            {
                if (currentCoolDown > coolDown)
                {
                    Shoot(f);
                    Debug.WriteLine("Shooting");
                }
                else
                {
                    currentCoolDown++;
                }
            }
        }

        public void Shoot(Form f)
        {

            // Shoot bullet
            this.facing = "right";
            Bullet bullet = new Bullet();

            bullet.direction = this.facing;
            bullet.bulletLeft = this.Left + (this.Width / 2);
            bullet.bulletTop = this.Top + (this.Height / 2);

            bullet.MakeEnemyBullet(f);
            //Debug.WriteLine(bullet);
            currentCoolDown = 0;
            
        }


    }
}
