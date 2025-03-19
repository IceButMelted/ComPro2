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
        private int _count = 0;
        public bool _IsBossDeath = false;

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
                _count++;
            }
            else if (this.Left < 0) {
                facing = Direction.Right;
                _count++;
            }

            if (facing == Direction.Left)
            {
                this.Left -= Speed;
            }
            else
            {
                this.Left += Speed;
            }

            if (_count > 1) {
                _count = 0;
                int rnd = ContraToolUtility.RandomNumberRange(0, 50);
                if (rnd > 40)
                    Speed = 30;
                else if (rnd > 20)
                    Speed = 10;
                else if (rnd > 10)
                    Speed = 5;
            }
        }


    }
}
