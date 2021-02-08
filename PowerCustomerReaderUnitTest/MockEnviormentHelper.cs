using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerCustomerReaderUnitTest
{
    class MockEnviormentHelper
    {
        private Mock<IHostingEnvironment> mockEnv;

        protected IHostingEnvironment hostingEnviroment { get; private set; }

        public MockEnviormentHelper(IHostingEnvironment hosting)
        {
            hostingEnviroment = hosting;
        }

        public MockEnviormentHelper(Mock<IHostingEnvironment> mockEnv)
        {
            this.mockEnv = mockEnv;
        }
    }
}
