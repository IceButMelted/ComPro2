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
        // Constructor
        public Soldier(int hp, int speed, int dmg) : base(hp, speed, dmg, "enemy", "soldier")
        {

        }
    }
}
