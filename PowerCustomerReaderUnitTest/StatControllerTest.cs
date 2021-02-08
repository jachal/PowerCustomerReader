using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerCustomerReader.Controllers;
using PowerCustomerReader.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerCustomerReaderUnitTest
{
    [TestClass]
    public class StatControllerTest
    {
        [TestMethod]
        public void IndexRetursnNotNull()
        {
            //Arrange
            var controller = new StatController();

            //Mocking hosting enviroment
            var mockHostingEnvironment = new Mock<IHostingEnvironment>(MockBehavior.Strict);

            //Act
            var result = controller.Index(mockHostingEnvironment.Object);

            //Assert
            Assert.IsNotNull(result, "View reuslut is not null");
        }
    }
}
