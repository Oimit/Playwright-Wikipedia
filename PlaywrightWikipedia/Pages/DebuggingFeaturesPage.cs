using Microsoft.Playwright;
using PlaywrightWikipedia.Helpers;
using NUnit.Framework;

namespace PlaywrightWikipedia.Pages
{
    public class DebuggingFeaturesPage
    {
        private readonly IPage _page;

        private ILocator SectionParagraph =>
            _page.Locator(".mw-heading:has(#Debugging_features) + p");

        private ILocator SectionList =>
            _page.Locator(".mw-heading:has(#Debugging_features) + p + ul");

        public DebuggingFeaturesPage(IPage page)
        {
            _page = page;
        }

        public async Task<string> ExtractDebuggingFeaturesViaUI()
        {
            var paragraph = await SectionParagraph.InnerTextAsync();
            var list = await SectionList.InnerTextAsync();

            var fullText = paragraph + " " + list;
            Logger.Log($"[UI] Extracted text: {fullText}");

            return fullText;
        }
    }
}