using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerCustomerReader.Models;
using PowerCustomerReader.Models.Enteties;
using PowerCustomerReader.Models.Service;


namespace PowerCustomerReader.Controllers
{
    /// <summary>
    /// Controller for home views
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        /// Returns empty index
        /// </summary>
        /// <param name="model">OrganisationViewModel</param>
        /// <returns>OrganisationViewModel</returns>
        [HttpGet]
        public IActionResult Index(OrganisationsViewModel model = null)
        {
            model = model == null ? new OrganisationsViewModel() : model;
            return View(model);
        }

        /// <summary>
        /// Takes in file and copies it
        /// Uses file service to read file
        /// sends request with Request service
        /// Updates ViewModel with updatedOrganisations
        /// </summary>
        /// <param name="formFile">Uploaded file</param>
        /// <param name="hostingEnviroment">enviroment</param>
        /// <returns>OrganisationViewModel</returns>
        [HttpPost]
        public async Task<IActionResult> IndexAsync(IFormFile formFile, [FromServices] IHostingEnvironment hostingEnviroment)
        {
            string filePath = $"{hostingEnviroment.WebRootPath}\\files\\";
            string fileTitle = filePath + formFile.FileName;
            using (FileStream fileStream = System.IO.File.Create(fileTitle))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush();
            }

            FileService fileService = new FileService();
            List<Organisation> organisations = await fileService.ReadFileAsync(fileTitle);

            RequestService requestService = new RequestService();
            List<Organisation> updatedOrganisations = await requestService.GetOrganizations(organisations, filePath);

            OrganisationsViewModel model = new OrganisationsViewModel()
            {
                Organisations = updatedOrganisations
            };

            return View(model);
        }

        /// <summary>
        /// Delets all files stored
        /// </summary>
        /// <param name="hostingEnviroment">Enviroment</param>
        /// <param name="test">parameter for running in test mode</param>
        /// <returns>Redirects to Index</returns>
        public IActionResult Purge([FromServices] IHostingEnvironment hostingEnviroment, bool test = false)
        {
            string filePath = $"{hostingEnviroment.WebRootPath}\\files\\";
            DirectoryInfo di = new DirectoryInfo(filePath);

            if(test == false)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Views privacy
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
