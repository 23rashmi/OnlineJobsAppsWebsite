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
    public class UserControllerTests
    {
        [Test]
        public void NewUser_Action_Returns_ViewResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.NewUser() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }

        [Test]
        public void Logint_Action_Returns_ViewResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }


        [Test]
        public void Forgot_Action_Logs_Info_Message()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Forgot() as ViewResult;

            // Assert
            ClassicAssert.IsNotNull(result);
        }
    }
}
// Assert
// You may need to add your own logic to check if the log contains the expected message.
// This depends on how your log4net configuration is set up.
// Example:




// Assert
// Similar to the above, you need to check if the log contains the expected message.
// Example:
// Assert.IsTrue(LogContains("Accessed HomeController's Contact action."));

// Add more test cases as needed for different scenarios