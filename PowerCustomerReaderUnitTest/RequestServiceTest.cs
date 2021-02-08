using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PowerCustomerReader.Models.Enteties;
using PowerCustomerReader.Models.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PowerCustomerReaderUnitTest
{
    [TestClass]
    public class RequestServiceTest
    {
        [TestMethod]
        public async Task GetOrganizationReturnsListOfOrgs()
        {
            //Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var organisation = new Organisation()
            {
                navn = "Kåres Kålrot",
                antallAnsatte = 64,
                naeringskode1 = new Naeringskode1()
                {
                    kode = "10.11"
                },
                organisasjonsform = new Organisasjonsform()
                {
                    kode = "AS"
                }
            };

            var content = await Task.Run(() => JsonConvert.SerializeObject(organisation));
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = httpContent
            };

            var httpClient = new HttpClient(handlerMock.Object);
            RequestService requestService = new RequestService(httpClient);
            var errorFilePath = "C:\\Users\\marja\\source\\repos\\PowerCustomerReader\\PowerCustomerReaderUnitTest\\TestFiles\\";

            List<Organisation> organisations = new List<Organisation>()
            {
                organisation
            };

            //Act
            List<Organisation> result = await requestService.GetOrganizations(organisations, errorFilePath, true);

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Organisation>));
        }
    }
}
