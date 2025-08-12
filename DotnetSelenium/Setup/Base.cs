using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotnetSelenium.Setup
{
    public class Base
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static ExtentReports extent;
        protected ExtentTest test;

        private static List<(string Name, string Status, double Duration ,string browser)> testResults
            = new List<(string, string, double, string)>();
        private DateTime testStartTime;
        private string current_browser;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
           
            string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports");
            Directory.CreateDirectory(reportDir);

            string reportPath = Path.Combine(reportDir, "index.html");
            var sparkReporter = new ExtentSparkReporter(reportPath);
            sparkReporter.Config.DocumentTitle = "Selenium NUnit Test Report";
            sparkReporter.Config.ReportName = "Automation Suite";
            sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", "Lokii");
        }

        [SetUp]
        public void BeforeEachTest()
        {
           
            testStartTime = DateTime.Now;

           
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            
            string browser = "chrome"; 
            current_browser= browser;   
            if (browser.ToLower() == "chrome")
                driver = new ChromeDriver();
            else if (browser.ToLower() == "firefox")
                driver = new FirefoxDriver();
            else
                throw new ArgumentException($"Unsupported browser: {browser}");

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        [TearDown]
        public void AfterEachTest()
        {
            var duration = (DateTime.Now - testStartTime).TotalSeconds;
            var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
            var name = TestContext.CurrentContext.Test.Name;

            testResults.Add((name, status, duration,current_browser));

       
            if (status == "Passed")
            {
                test.Pass("Test Passed");
            }
            else if (status == "Failed")
            {
                string screenshotPath = CaptureScreenshot(name);
                test.Fail("Test Failed")
                    .Fail($"Error: {TestContext.CurrentContext.Result.Message}")
                    .Fail($"Stack Trace: {TestContext.CurrentContext.Result.StackTrace}")
                    .AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                test.Skip("Test Skipped");
            }

            driver.Quit();
            driver.Dispose();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
         
            extent.Flush();


            var html = new StringBuilder();
            html.Append("<html><head><title>Emailable Report</title>");
            html.Append("<style>");
            html.Append("body { font-family: Arial, sans-serif; background-color: #f9fafb; margin: 20px; color: #333; }");
            html.Append("h2 { text-align: center; color: #2c3e50; }");
            html.Append("p { text-align: center; font-size: 16px; margin-bottom: 20px; }");
            html.Append("table { border-collapse: collapse; width: 90%; margin: auto; box-shadow: 0 4px 8px rgba(0,0,0,0.1); border-radius: 8px; overflow: hidden; }");
            html.Append("th { background-color: #4CAF50; color: white; padding: 12px; text-align: left; font-size: 14px; }");
            html.Append("td { padding: 12px; text-align: left; font-size: 14px; border-bottom: 1px solid #ddd; }");
            html.Append("tr:hover { background-color: #f1f1f1; transition: 0.2s; }");
            html.Append(".passed { color: #2ecc71; font-weight: bold; }");
            html.Append(".failed { color: #e74c3c; font-weight: bold; }");
            html.Append(".skipped { color: #f39c12; font-weight: bold; }");
            html.Append("</style></head><body>");
            html.Append("<h2>Test Summary</h2>");
            html.Append($"<p>Total: {testResults.Count} &nbsp;|&nbsp; " +
                        $"<span class='passed'>Passed: {testResults.Count(r => r.Status == "Passed")}</span> &nbsp;|&nbsp; " +
                        $"<span class='failed'>Failed: {testResults.Count(r => r.Status == "Failed")}</span> &nbsp;|&nbsp; " +
                        $"<span class='skipped'>Skipped: {testResults.Count(r => r.Status == "Skipped")}</span></p>");

            html.Append("<table>");
            html.Append("<tr><th>Test Name</th><th>Status</th><th>Duration (s)</th><th>Browser</th></tr>");
            foreach (var result in testResults)
            {
                string statusClass = result.Status.ToLower() switch
                {
                    "passed" => "passed",
                    "failed" => "failed",
                    "skipped" => "skipped",
                    _ => ""
                };
                html.Append($"<tr><td>{result.Name}</td><td class='{statusClass}'>{result.Status}</td><td>{result.Duration:F2}</td><td>{result.browser}</td></tr>");
            }
            html.Append("</table></body></html>");

            string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports");
            Directory.CreateDirectory(reportDir);
            File.WriteAllText(Path.Combine(reportDir, "emailable.html"), html.ToString());
        }

        
        private string CaptureScreenshot(string testName)
        {
            string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports", "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath); 
            return filePath;
        }

    }
}


