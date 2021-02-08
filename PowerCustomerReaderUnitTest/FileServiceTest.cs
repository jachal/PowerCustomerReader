using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerCustomerReader.Models.Enteties;
using PowerCustomerReader.Models.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PowerCustomerReaderUnitTest
{
    [TestClass]
    public class FileServiceTest
    {
        [TestMethod]
        public void CalculateProcentageReturnsCorrectTotal()
        {
            //Arrange
            var organisationStats = new OrganisationStats()
            {
                ENK = 100,
                ANDRE = 100,
                AS_ZeroToFour = 100,
                AS_FiveToTen = 100,
                AS_OverTen = 100
            };

            FileService fileService = new FileService();

            //Act
            var result = fileService.CalculateProcentage(organisationStats);

            //Assert
            Assert.AreEqual(500, result.Total);
        }

        [TestMethod]
        public void CalculateProcentageReturnsCorrectPrecentage()
        {
            //Arrange
            var organisationStats = new OrganisationStats()
            {
                ENK = 100,
                ANDRE = 100,
                AS_ZeroToFour = 100,
                AS_FiveToTen = 100,
                AS_OverTen = 100
            };

            FileService fileService = new FileService();

            //Act
            var result = fileService.CalculateProcentage(organisationStats);

            //Assert
            Assert.AreEqual(20, result.ProsENK);
            Assert.AreEqual(20, result.ProsANDRE);
            Assert.AreEqual(20, result.Pros_AS_ZeroToFour);
            Assert.AreEqual(20, result.Pros_AS_FiveToTen);
            Assert.AreEqual(20, result.Pros_AS_OverTen);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task ReadUpdatedFileForStatsReadsFileAndReturnsCorrectStatsAsync()
        {
            //Arrange
            var filePath = "C:\\Users\\marja\\source\\repos\\PowerCustomerReader\\PowerCustomerReaderUnitTest\\TestFiles\\MockFile.csv";
            FileService fileService = new FileService();

            //Act
            OrganisationStats result = await fileService.ReadUpdatedFileForStats(filePath);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OrganisationStats));
            Assert.AreEqual(result.ENK, 0);
            Assert.AreEqual(result.AS_OverTen, 37);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task WriteUpdatedFileReturnsTrueWhenDone()
        {
            //Arrange
            var filePath = "C:\\Users\\marja\\source\\repos\\PowerCustomerReader\\PowerCustomerReaderUnitTest\\TestFiles\\UpdatedFile.csv";
            FileService fileService = new FileService();

            List<Organisation> organisations = new List<Organisation>()
            {
                new Organisation()
                {
                    antallAnsatte = 10,
                    organisasjonsform = new Organisasjonsform()
                    {
                        kode = "AS"
                    },
                    navn = "Kjell's potter og panner",
                    naeringskode1 = new Naeringskode1()
                    {
                        kode = "41.101"
                    }
                },
                new Organisation()
                {
                    antallAnsatte = 5,
                    organisasjonsform = new Organisasjonsform()
                    {
                        kode = "ESEK"
                    },
                    navn = "raskebriller.no",
                    naeringskode1 = new Naeringskode1()
                    {
                        kode = "41.101"
                    }
                },
                new Organisation()
                {
                    antallAnsatte = 1,
                    organisasjonsform = new Organisasjonsform()
                    {
                        kode = "ENK"
                    },
                    navn = "Marit's makrame",
                    naeringskode1 = new Naeringskode1()
                    {
                        kode = "41.101"
                    }
                }
            };

            //Act
            bool result = await fileService.WriteUpdatedFile(organisations, filePath);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task ErrorWriterReturnsTrueWhenDoneWriting()
        {
            //Arrange
            var filePath = "C:\\Users\\marja\\source\\repos\\PowerCustomerReader\\PowerCustomerReaderUnitTest\\TestFiles\\ErrorFile.csv";
            FileService fileService = new FileService();

            List<OganisationError> organisations = new List<OganisationError>
            {
                new OganisationError("897488582", "404"),
                new OganisationError("897488532", "404"),
                new OganisationError("897488232", "401"),
                new OganisationError("895488582", "401"),
                new OganisationError("897488522", "404")
            };

            //Act
            bool result = await fileService.ErrorWriter(organisations, filePath);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestIfReadFileReturnsListFromReadingFile()
        {
            //Arrange
            var filePath = "C:\\Users\\marja\\source\\repos\\PowerCustomerReader\\PowerCustomerReaderUnitTest\\TestFiles\\po-kunder.csv";
            FileService fileService = new FileService();

            //Act
            List<Organisation> result = await fileService.ReadFileAsync(filePath);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Organisation>));
            Assert.AreEqual(1000, result.Count);
        }
    }
}
