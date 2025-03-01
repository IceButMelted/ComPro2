using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public class Platform : Tags
    {
        public string[] tags { get; set; }

        public void DisplayPlatformInfo() { 
            Debug.WriteLine($"Platform: {Name}, Postion: ({Location.X},{Location.Y}), Size: {Size}");
        }
    }

}
