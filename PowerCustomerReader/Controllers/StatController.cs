using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PowerCustomerReader.Models.Enteties;
using PowerCustomerReader.Models.Service;
using PowerCustomerReader.Models.ViewModels;
using System.IO;

namespace PowerCustomerReader.Controllers
{
    /// <summary>
    /// Controller for showing stats
    /// </summary>
    public class StatController : Controller
    {

        /// <summary>
        /// Checks if file exists
        /// uses fileservice to read updated file
        /// Updates ViewModel with updated stats
        /// </summary>
        /// <param name="hostingEnviroment">Enviroment</param>
        /// <returns>StatsViewModel</returns>
        public async Task<IActionResult> Index([FromServices] IHostingEnvironment hostingEnviroment)
        {
            string filePath = $"{hostingEnviroment.WebRootPath}\\files\\oppdatert-po-kunder.csv";
            OrganisationStats organisationStats = new OrganisationStats();
            FileService fileService = new FileService();

            if (System.IO.File.Exists(filePath))
            {
                organisationStats = await fileService.ReadUpdatedFileForStats(filePath);
            } else
            {
                organisationStats = null;
            }

            StatsViewModel model = new StatsViewModel()
            {
                organisationStats = organisationStats
            };

            return View(model);
        }
    }
}
