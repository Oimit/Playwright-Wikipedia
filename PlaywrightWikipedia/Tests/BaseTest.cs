using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace PlaywrightWikipedia.Tests
{
    public class BaseTest : PageTest
    {
        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            };
        }
    }
}