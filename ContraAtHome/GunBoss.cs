using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public class GunBoss : PictureBox
    {
        private int counter = 0;
        private int durationBetween = 20;
        private bool BurstShoot = false;

        public string facing = "down";

        public void MoveWithPlayer(PictureBox p)
        {
            Left = p.Left;
        }

        public bool IsBurstShoot()
        {
            if (counter <= 0)
            {
                BurstShoot = false;
                counter = 3;
            }
            else { 
                BurstShoot = true;
                counter--;
            }

            return BurstShoot;
        }

        public int GetFrameDurationBetween() {
            return durationBetween;
        }
    }
    
    
}
