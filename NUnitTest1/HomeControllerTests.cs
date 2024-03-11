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
    public class HomeControllerTests
    {
        [Test]
        public void Index_Action_Returns_ViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void About_Action_Returns_ViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void Contact_Action_Returns_ViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Contact() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void Index_Action_Logs_Info_Message()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            controller.Index();

            // Assert
            // You may need to add your own logic to check if the log contains the expected message.
            // This depends on how your log4net configuration is set up.
            // Example:
            // Assert.IsTrue(LogContains("Accessed HomeController's Index action."));
        }

        [Test]
        public void About_Action_Logs_Info_Message()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            controller.About();

            // Assert
            // Similar to the above, you need to check if the log contains the expected message.
            // Example:
            // Assert.IsTrue(LogContains("Accessed HomeController's About action."));
        }

        [Test]
        public void Contact_Action_Logs_Info_Message()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            controller.Contact();

            // Assert
            // Similar to the above, you need to check if the log contains the expected message.
            // Example:
            // Assert.IsTrue(LogContains("Accessed HomeController's Contact action."));
        }
        // Add more test cases as needed for different scenarios
    }
}
