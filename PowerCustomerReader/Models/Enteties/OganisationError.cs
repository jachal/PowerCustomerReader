using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerCustomerReader.Models.Enteties
{
    /// <summary>
    /// Error entity
    /// </summary>
    public class OganisationError
    {
        public OganisationError(string OrgNmb, string Code)
        {
            this.Code = Code;
            this.OrgNmb = OrgNmb;
        }
        public string OrgNmb { get; set; }
        public string Code { get; set; }
    }
}
