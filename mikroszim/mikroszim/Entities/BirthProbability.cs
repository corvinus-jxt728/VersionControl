using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroszim.Entities
{
     public class BirthProbability
    {
        public int Age { get; set; }
        public byte NmbOfChildren { get; set; }
        public double BirthGivingProb { get; set; }
    }
}
