using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public class GunBoss : PictureBox
    {

        public void MoveWithPlayer(PictureBox p)
        {
            Left = p.Left;
        }
    }
    
    
}
