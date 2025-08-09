using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using DotnetSelenium.Reporting;
using AventStack.ExtentReports;
using System;
using System.IO;

namespace DotnetSelenium.Setup
{
    public class Base
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            extent = ReportManager.GetInstance();
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace ?? "";

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                test.Fail("Test Failed").AddScreenCaptureFromPath(screenshotPath);
                test.Fail(stacktrace);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }

            driver.Quit();
            driver.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }

        private string CaptureScreenshot(string testName)
        {
            string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports", "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath); // use string instead of enum
            return filePath;
        }
    }
}
