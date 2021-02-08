using PowerCustomerReader.Models.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerCustomerReader.Models.Service
{
    /// <summary>
    /// Service for requests
    /// </summary>
    public class RequestService
    {
        private readonly HttpClient client;
        private FileService fileService;

        public RequestService(HttpClient testClient = null)
        {
            if (testClient == null)
            {
                client = new HttpClient();
            } else
            {
                client = testClient;
            }
            fileService = new FileService();
        }

        /// <summary>
        /// Sends requests to brreg for organisation info using OrgNr.
        /// Limits number for requests to only two at the same time
        /// writes updated organisations to file
        /// </summary>
        /// <param name="organisations">List of organisations</param>
        /// <param name="filePath">string to path, where to store files</param>
        /// <param name="test">bool to run method in tesing</param>
        /// <returns>List of updated organisations</returns>
        public async Task<List<Organisation>> GetOrganizations(List<Organisation> organisations, string filePath, bool test = false)
        {
            var updatedOrgs = new List<Organisation>();
            var lisErrors = new List<OganisationError>();
            var list = new List<HttpResponseMessage>();
            var tasks = new List<Task>();
            var threads = 2;
            var url = "https://data.brreg.no/enhetsregisteret/api/enheter/";

            var throttler = new SemaphoreSlim(initialCount: threads);

            foreach (var org in organisations)
            {
                await throttler.WaitAsync();
                tasks.Add(
                    Task.Run(async () =>
                    {
                        try
                        {
                            var responseMessage = await client.GetAsync(url + org.organisasjonsnummer);

                            if (responseMessage.IsSuccessStatusCode)
                            {
                                var json = await responseMessage.Content.ReadAsStringAsync();
                                var organisation = JsonConvert.DeserializeObject<Organisation>(json);
                                Console.WriteLine(responseMessage.Content);
                                list.Add(responseMessage);
                                updatedOrgs.Add(organisation);
                            }
                            else
                            {
                                string code = responseMessage.StatusCode.ToString();
                                lisErrors.Add(new OganisationError(org.organisasjonsnummer, code));
                            }
                        }
                        finally
                        {
                            throttler.Release();
                        }
                    }));
            }
            if(!test)
            {
                await fileService.ErrorWriter(lisErrors, filePath);
            }
            await fileService.WriteUpdatedFile(updatedOrgs, filePath);
            return updatedOrgs;

            //List<Organisation> list = await Task.WhenAll(tasks);
        }
    }
}

