using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using PlaywrightWikipedia.Pages;

namespace PlaywrightWikipedia.PlaywrightWikipedia
{
    [TestFixture]
    public class ColorTest : PageTest
    {
        private ColorChange _colorChange;

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            };
        }

        [SetUp]
        public async Task SetUp()
        {
            await Page.GotoAsync("https://en.wikipedia.org/wiki/Playwright_(software)");
            _colorChange = new ColorChange(Page);
        }

        [Test]
        public async Task DarkModeSelection()
        {
            await _colorChange.OpenAppearancePanel();
            await _colorChange.SelectDarkMode();
            await _colorChange.AssertDarkModeSelected();
            await _colorChange.AssertDarkModeApplied();
            await Page.PauseAsync();
        }
    }
}