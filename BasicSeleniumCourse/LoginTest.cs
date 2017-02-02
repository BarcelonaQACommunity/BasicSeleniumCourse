using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BasicSeleniumWebDriver
{
    /// <summary>
    /// Testing the Login functionallity
    /// </summary>
    [TestClass]
    public class LoginTest
    {
        /// <summary>
        /// The web driver
        /// </summary>
        private IWebDriver _webDriver;


        [TestInitialize]
        public void SetUp()
        {
            //Set Up Web Driver
            this._webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(@"D:\"), new ChromeOptions(), TimeSpan.FromSeconds(10));

            // Go to the Web Page
            this._webDriver.Navigate().GoToUrl("http://demo.guru99.com/V4/index.php");
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            this._webDriver.Close();
        }

        /// <summary>
        /// As a User, I can Logon into the web page.
        /// </summary>
        [TestMethod]
        public void UserLogon()
        {
            // -- Arrange --

            // Expected
            var expectedUrl = "http://demo.guru99.com/V4/manager/Managerhomepage.php";

            // Buttons
            var userIdTextBox = this._webDriver.FindElement(By.Name("uid"));
            var userPasswordTextBox = this._webDriver.FindElement(By.Name("password"));
            var clickLoginButton = this._webDriver.FindElement(By.Name("btnLogin"));

            // -- Act --

            // User enters the User ID
            userIdTextBox.SendKeys("mngr55046");

            // User enters the Password
            userPasswordTextBox.SendKeys("AdusemU");

            // User clicks login button
            clickLoginButton.Click();

            // -- Assert --
            Assert.AreEqual(expectedUrl, this._webDriver.Url, string.Format("The url must be {0}", expectedUrl));
        }

        /// <summary>
        /// As a Invalid User, I cant Logon into the web page.
        /// </summary>
        [TestMethod]
        public void InvalidUserCantLogon()
        {
            // -- Arrange --

            // Buttons
            var userIdTextBox = this._webDriver.FindElement(By.Name("uid"));
            var userPasswordTextBox = this._webDriver.FindElement(By.Name("password"));
            var clickLoginButton = this._webDriver.FindElement(By.Name("btnLogin"));

            // -- Act --

            // User enters the User ID
            userIdTextBox.SendKeys("mngr52720");

            // User enters the Password
            userPasswordTextBox.SendKeys("invalidPassword");

            // User clicks login button
            clickLoginButton.Click();

            // -- Assert --

            // Expected
            var alertText = "User or Password is not valid";

            // Switch to Alert message
            var alert = this._webDriver.SwitchTo().Alert();

            // Get the text alert
            var realText = alert.Text;

            // Click accept button alert
            alert.Accept();

            Assert.AreEqual(alertText, realText, string.Format("The alert text must be {0}", alertText));
        }
    }
}
