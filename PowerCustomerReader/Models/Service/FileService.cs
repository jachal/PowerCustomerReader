using PowerCustomerReader.Models.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace PowerCustomerReader.Models.Service
{
    /// <summary>
    /// Service for reading and writing files
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// Reads uploaded file, and returns list of orgnaisations
        /// </summary>
        /// <param name="filePath">Where to read the file from</param>
        /// <returns>List of organisations</returns>
        public async Task<List<Organisation>> ReadFileAsync(string filePath)
        {
            var organisations = new List<Organisation>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] lineSplit = line.Split(";");

                    Organisation orgnaisation = new Organisation()
                    {
                        navn = lineSplit[1],
                        organisasjonsnummer = lineSplit[0]
                    };

                    organisations.Add(orgnaisation);
                }
            }
            return organisations;
        }

        /// <summary>
        /// Method for writing errors to file
        /// </summary>
        /// <param name="errors">List of errors</param>
        /// <param name="filePath">where to write file</param>
        /// <returns>true on succsess</returns>
        public async Task<bool> ErrorWriter(List<OganisationError> errors, string filePath)
        {
            try
            {
                foreach (var error in errors)
                {
                    using (StreamWriter file = new StreamWriter(filePath + "error.csv", true))
                    {
                        await file.WriteLineAsync(error.OrgNmb + ";" + error.Code);
                        file.Flush();
                    }
                }
            }
            catch (IOException e)
            {
                throw new ApplicationException("Something went wrong, wrting errors:", e);
            }
            return true;
        }

        /// <summary>
        /// Method for writing updated file from request
        /// </summary>
        /// <param name="updatedList">List of updated organisations</param>
        /// <param name="filePath">where to write file</param>
        /// <returns>true on success</returns>
        public async Task<bool> WriteUpdatedFile(List<Organisation> updatedList, string filePath)
        {
            try
            {
                using StreamWriter file = new StreamWriter(filePath + "oppdatert-po-kunder.csv");
                foreach (var item in updatedList.ToList())
                {
                    if (item.naeringskode1 != null || item.organisasjonsform != null || item.navn != null)
                    {
                        await file.WriteLineAsync(item.antallAnsatte + ";" + item.organisasjonsform.kode + ";" + item.navn);
                        file.Flush();
                    }
                }
            }
            catch (IOException e)
            {

                throw new ApplicationException("Something went wrong, when wrting updated customers to new file:", e);

            }
            return true;
        }

        /// <summary>
        /// Reads updated files and returns organisations stats
        /// </summary>
        /// <param name="filePath">where to read file</param>
        /// <returns>List of organsiations stats</returns>
        public async Task<OrganisationStats> ReadUpdatedFileForStats(string filePath)
        {
            var organisationStats = new OrganisationStats();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] lineSplit = line.Split(";");

                    int numberOfEmp = Int32.Parse(lineSplit[0]);

                    if (lineSplit[1].Equals("AS"))
                    {
                        if (numberOfEmp <= 5)
                        {
                            organisationStats.AS_ZeroToFour += numberOfEmp;
                        }
                        else if (numberOfEmp <= 10 && numberOfEmp > 5)
                        {
                            organisationStats.AS_FiveToTen += numberOfEmp;
                        }
                        else if (numberOfEmp > 10)
                        {
                            organisationStats.AS_OverTen += numberOfEmp;
                        }
                    }
                    else if (lineSplit[1] == "ENK")
                    {
                        organisationStats.ENK += numberOfEmp;
                    }
                    else
                    {
                        organisationStats.ANDRE += numberOfEmp;
                    }
                }
            }
            return CalculateProcentage(organisationStats);
        }

        /// <summary>
        /// Helper method for calculating precentage of organisations stats
        /// </summary>
        /// <param name="organisationStats">Object of type organsiations stats</param>
        /// <returns>Updated organisation stats</returns>
        public OrganisationStats CalculateProcentage(OrganisationStats organisationStats)
        {
            organisationStats.Total = organisationStats.AS_ZeroToFour + organisationStats.AS_FiveToTen + organisationStats.AS_OverTen + organisationStats.ENK + organisationStats.ANDRE;

            organisationStats.ProsANDRE = (int)Math.Round((double)(100 * organisationStats.ANDRE) / organisationStats.Total);
            organisationStats.ProsENK = (int)Math.Round((double)(100 * organisationStats.ENK) / organisationStats.Total);
            organisationStats.Pros_AS_ZeroToFour = (int)Math.Round((double)(100 * organisationStats.AS_ZeroToFour) / organisationStats.Total);
            organisationStats.Pros_AS_FiveToTen = (int)Math.Round((double)(100 * organisationStats.AS_FiveToTen) / organisationStats.Total);
            organisationStats.Pros_AS_OverTen = (int)Math.Round((double)(100 * organisationStats.AS_OverTen) / organisationStats.Total);

            return organisationStats;
        }
    }
}
