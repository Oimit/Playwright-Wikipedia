using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace PlaywrightWikipedia.Tests
{
    public class BaseTest : PageTest
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _htmlReporter;

        protected ExtentTest _test;

        [OneTimeSetUp]
        public static void InitReport()
        {
            if (_extent != null) return; // Already initialized, don't overwrite
            
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var reportPath = Path.Combine(projectRoot, "Reports", "TestReport.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);

            _htmlReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
        }

        [SetUp]
        public void InitTest()
        {
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void LogResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
                _test.Pass("Test passed.");
            else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                _test.Fail(TestContext.CurrentContext.Result.Message);
        }

        [OneTimeTearDown]
        public static void FlushReport()
        {
            _extent.Flush();
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            };
        }
    }
}