using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    class Boss : Enemy
    {
        public Boss(int hp, int speed, params string[] initialTags) : base(hp, speed, initialTags)
        {

        }

        public override void EnemyAction(Form f)
        {
            StepLeftRight(f);
            //Debug.WriteLine(facing);
            //Debug.WriteLine($"Boss : left{this.Left}");
        }

        private void StepLeftRight(Form f) {

            if (this.Left + this.Width > 800)
            {
                facing = Direction.Left;
            }
            else if (this.Left < 0) {
                facing = Direction.Right;
            }

            if (facing == Direction.Left)
            {
                this.Left -= Speed;
            }
            else
            {
                this.Left += Speed;
            }

        }


    }
}
