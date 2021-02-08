using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerCustomerReader.Models.Enteties
{
    /// <summary>
    /// Entity for organisations stats
    /// </summary>
    public class OrganisationStats
    {
        public int ENK { get; set; }
        public int ANDRE { get; set; }
        public int AS_ZeroToFour { get; set; }
        public int AS_FiveToTen { get; set; }
        public int AS_OverTen { get; set; }
        public int Total { get; set; }

        //Proecentage of values
        public double ProsANDRE { get; set; }
        public double ProsENK { get; set; }
        public double Pros_AS_ZeroToFour { get; set; }
        public double Pros_AS_FiveToTen { get; set; }
        public double Pros_AS_OverTen { get; set; }
    }
}
