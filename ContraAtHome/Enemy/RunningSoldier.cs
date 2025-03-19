using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    class RunningSoldier : Enemy
    {
        public string lastFacing = "";
        public RunningSoldier(int hp, int speed, params string[] initialTags) : base(hp, speed, initialTags)
        {

        }

        public override void EnemyAction(Form f)
        {
            if(Speed > 0) facing = "right";
            else facing = "left";

            Left += Speed;
        }

    }
}
