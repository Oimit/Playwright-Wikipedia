using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using PlaywrightWikipedia.Pages;
using PlaywrightWikipedia.Helpers;

namespace PlaywrightWikipedia.Tests
{
    [TestFixture]
    public class ColorTest : BaseTest
    {
        private ColorChange _colorChange;

        [SetUp]
        public async Task SetUp()
        {
            Logger.Initialize("ColorTest");
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
            //await Page.PauseAsync();
        }
    }
}