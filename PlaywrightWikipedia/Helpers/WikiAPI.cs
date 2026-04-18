using Microsoft.Playwright;
using System.Text.Json;

namespace PlaywrightWikipedia.Helpers
{
    public static class WikiAPI
    {
        public static async Task<string> GetSectionWikitext(IAPIRequestContext apiContext, string page, int sectionIndex)
        {
            var url = $"https://en.wikipedia.org/w/api.php?action=parse&page={page}&prop=wikitext&section={sectionIndex}&format=json";

            var response = await apiContext.GetAsync(url);
            var json = await response.JsonAsync();

            var wikitext = json?.GetProperty("parse")
                .GetProperty("wikitext")
                .GetProperty("*")
                .GetString();

            Logger.Log($"[API] Raw wikitext: {wikitext}");
            return wikitext ?? string.Empty;
        }
    }
}