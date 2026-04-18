using Microsoft.Playwright;
using PlaywrightWikipedia.Helpers;

namespace PlaywrightWikipedia.Pages
{
    public class MicDevToolsPage
    {
        private readonly IPage _page;

        private ILocator MicDevToolsTable =>
            _page.Locator("[aria-labelledby*='Microsoft_development_tools']");

        private ILocator ShowButton =>
            MicDevToolsTable.Locator("button.mw-collapsible-toggle");

        private ILocator AllListItems =>
            MicDevToolsTable.Locator("td li");

        public MicDevToolsPage(IPage page)
        {
            _page = page;
        }

        public async Task ExpandTable()
        {
            await ShowButton.ClickAsync();
        }

        public async Task<List<string>> GetNonLinkItems()
        {
            var nonLinkItems = new List<string>();
            var items = await AllListItems.AllAsync();

            foreach (var item in items)
            {
                var text = (await item.InnerTextAsync()).Trim();
                if (string.IsNullOrWhiteSpace(text)) continue;

                var hasSubList = await item.Locator("ul").CountAsync() > 0;
                if (hasSubList)
                {
                    Logger.Log($"[SKIPPED - Grouping Label] '{text.Split('\n')[0].Trim()}' is a parent grouping label, not a technology name.");
                    continue;
                }

                var hasLink = await item.Locator("a").CountAsync() > 0;
                if (hasLink)
                {
                    var linkText = await item.Locator("a").First.InnerTextAsync();
                    var linkClass = await item.Locator("a").First.GetAttributeAsync("class") ?? "";
                    if (linkClass.Contains("mw-selflink"))
                        Logger.Log($"[SKIPPED - Self Link] '{linkText}' is the current page.");
                    else
                        Logger.Log($"[PASS] '{text.Trim()}' is a valid link.");
                }
                else
                {
                    Logger.Log($"[FAIL] '{text}' is NOT a link.");
                    nonLinkItems.Add(text);
                }
            }

            return nonLinkItems;
        }
    }
}