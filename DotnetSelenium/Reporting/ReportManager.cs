using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace DotnetSelenium.Reporting
{
    public static class ReportManager
    {
        private static ExtentReports _extent;

        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                string reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestReports");
                Directory.CreateDirectory(reportDir);

                string reportPath = Path.Combine(reportDir, "index.html");

                var sparkReporter = new ExtentSparkReporter(reportPath);
                sparkReporter.Config.DocumentTitle = "Selenium NUnit Test Report";
                sparkReporter.Config.ReportName = "Automation Suite";
                sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);
                _extent.AddSystemInfo("Environment", "QA");
                _extent.AddSystemInfo("Tester", "Lokesh");
            }
            return _extent;
        }
    }
}
