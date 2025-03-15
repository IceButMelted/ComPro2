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
        int bulletSpeec = 7;
        bool _canShooting = true;
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
                    _canShooting = true;
                }
                else
                {
                    currentCoolDown++;
                }
            }
        }

        public bool CanShooting
        {
            get { return _canShooting; }
            set { _canShooting = value; }
        }
        public int BulletSpeed
        {
            get { return bulletSpeec; }
            set { bulletSpeec = value; }
        }

        public void ResetShootCooldown()
        {
            currentCoolDown = 0;
        }

    }
}
