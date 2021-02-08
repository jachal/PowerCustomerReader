using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerCustomerReader.Models.Enteties;

namespace PowerCustomerReader.Models.ViewModels
{
    public class StatsViewModel
    {
        public OrganisationStats organisationStats { get; set; }

    }
}
