using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerCustomerReader.Controllers;
using System.IO;


namespace PowerCustomerReaderUnitTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexRetursnNotNull()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = (ViewResult)controller.Index();

            //Assert
            Assert.IsNotNull(result, "View reuslut is not null");
        }

        [TestMethod]
        public void PurgeReturnsNotNull()
        {
            //Arrange
            var controller = new HomeController();

            //Mocking hosting enviroment
            var mockHostingEnvironment = new Mock<IHostingEnvironment>(MockBehavior.Default);

            //Act
            var result = controller.Purge(mockHostingEnvironment.Object, true);

            //Assert
            Assert.IsNotNull(result, "View reuslut is not null");
        }

        [TestMethod]
        public void IndexAsyncReturnsNotNull()
        {
            //Arrange
            var controller = new HomeController();
            var fileMock = new Mock<IFormFile>();

            //Setup for mock file
            var content = "This is a mock file, nothing spescial here";
            var fileName = "test.csv";
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(content);
            writer.Flush();
            memoryStream.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);

            var file = fileMock.Object;

            //Mocking hosting enviroment
            var mockHostingEnvironment = new Mock<IHostingEnvironment>(MockBehavior.Strict);

            //Act
            var result = controller.IndexAsync(file, mockHostingEnvironment.Object);

            //Assert
            Assert.IsNotNull(result, "View Result is not null");
        }

        [TestMethod]
        public void PrivacyReturnsNotNull()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = (ViewResult)controller.Privacy();

            //Assert
            Assert.IsNotNull(result, "View reuslut is not null");
        }
    }
}
