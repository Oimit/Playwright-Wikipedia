using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightWikipedia.Pages;
using PlaywrightWikipedia.Helpers;

namespace PlaywrightWikipedia.Tests
{
    [TestFixture]
    public class DebuggingFeaturesTest : PageTest
    {
        private DebuggingFeaturesPage _debuggingFeaturesPage;

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
            Logger.Initialize("DebuggingFeaturesTest");
            await Page.GotoAsync("https://en.wikipedia.org/wiki/Playwright_(software)");
            _debuggingFeaturesPage = new DebuggingFeaturesPage(Page);
        }

        [Test]
        public async Task UIAndAPIUniqueWordCountShouldBeEqual()
        {
            // Get raw text via UI
            var uiRawText = await _debuggingFeaturesPage.ExtractDebuggingFeaturesViaUI();
            Logger.Log($"[UI] Raw text: {uiRawText}");

            // Get raw text via API
            var apiRawText = await WikiAPI.GetSectionWikitext(Page.APIRequest, "Playwright_(software)", 5);
            Logger.Log($"[API] Raw text: {apiRawText}");

            // Normalize both texts
            var uiNormalized = TextNormalizer.Normalize(uiRawText);
            var apiNormalized = TextNormalizer.Normalize(apiRawText);
            Logger.Log($"[UI] Normalized text: {uiNormalized}");
            Logger.Log($"[API] Normalized text: {apiNormalized}");

            // Count unique words in both
            var uiCount = TextNormalizer.CountUniqueWords(uiNormalized);
            var apiCount = TextNormalizer.CountUniqueWords(apiNormalized);
            Logger.Log($"[UI] Unique word count: {uiCount}");
            Logger.Log($"[API] Unique word count: {apiCount}");

            // Assert both counts are equal
            Assert.That(uiCount, Is.EqualTo(apiCount),
                $"UI count ({uiCount}) does not match API count ({apiCount})");
        }
    }
}