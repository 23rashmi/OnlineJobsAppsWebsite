using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JobsPortal.Controllers;
using Moq;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace NUnitTest1
{
    [TestFixture]
    public class JobControllerTests
    {
        private JobController controller;

        [SetUp]
        public void Setup()
        {
            controller = new JobController();
        }

        [Test]
        public void PostJob_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var controller = new JobController();
            {
                // Populate properties with invalid data
            };
            controller.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            // Assert
        }

        // Write more test methods for other actions as needed...

        [TearDown]
        public void Teardown()
        {
            controller.Dispose();
        }
    }
}