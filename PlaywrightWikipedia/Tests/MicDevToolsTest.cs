using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using PlaywrightWikipedia.Pages;
using PlaywrightWikipedia.Helpers;

namespace PlaywrightWikipedia.Tests
{
    [TestFixture]
    public class MicrosoftDevToolsTest : BaseTest
    {
        private MicDevToolsPage _devToolsPage;

        [SetUp]
        public async Task SetUp()
        {
            Logger.Initialize("MicrosoftDevToolsTest");
            await Page.GotoAsync("https://en.wikipedia.org/wiki/Playwright_(software)");
            _devToolsPage = new MicDevToolsPage(Page);
        }

        [Test]
        public async Task AllTechnologyNamesShouldBeLinks()
        {
            await _devToolsPage.ExpandTable();

            var nonLinkItems = await _devToolsPage.GetNonLinkItems();

            Assert.That(nonLinkItems, Is.Empty,
                $"The following technology names are not links: {string.Join(", ", nonLinkItems)}");
        }
    }
}