using Microsoft.Playwright;
using System.Text.RegularExpressions;
using PlaywrightWikipedia.Helpers;

namespace PlaywrightWikipedia.Pages
{
    public class ColorChange
    {
        private readonly IPage _page;

        public ColorChange(IPage page)
        {
            _page = page;
        }

        private ILocator AppearanceToggle =>
            _page.Locator("#vector-appearance-dropdown-checkbox"); // ID of appearance toggle

        private ILocator DarkModeButton =>
            _page.Locator("#skin-client-pref-skin-theme-value-night"); // ID of Dark Mode button

        private ILocator LightModeButton =>
            _page.Locator("#skin-Client-pref-skin-theme-value-day"); // ID of Light Mode button

        public async Task OpenAppearancePanel() // Openning the appearance by JS script, and test if DarkModeButton is 
        {
            await AppearanceToggle.EvaluateAsync("element => element.click()");
            //await Assertions.Expect(DarkModeButton).ToBeAttachedAsync(); // Not relevant - the #skin-Client-pref-skin-theme-value-day is just hidden and always exist.
            Logger.Log("Appearance panel opened.");
        }

        public async Task SelectDarkMode()
        {
            await DarkModeButton.EvaluateAsync("element => element.click()");
            Logger.Log("Dark mode selected.");
        }

        public async Task AssertDarkModeSelected()
        {
            await Assertions.Expect(DarkModeButton).ToBeCheckedAsync();
            Logger.Log("Assertion passed: Dark mode radio button is checked.");
        }

        public async Task SelectLightMode()
        {
            await LightModeButton.EvaluateAsync("element => element.click()");
            Logger.Log("Light mode selected.");
        }

        public async Task AsserLightModeSelected()
        {
            await Assertions.Expect(LightModeButton).ToBeCheckedAsync();
            Logger.Log("Assertion passed: Light mode radio button is checked.");
        }

        public async Task AssertDarkModeApplied()
        {
            await Assertions.Expect(_page.Locator("html"))
                .ToHaveClassAsync(new Regex("skin-theme-clientpref-night")); //Using Regex to find "night" or "day" for "Light" mode or "os" for Automatic.
            Logger.Log("Assertion passed: HTML class contains 'skin-theme-clientpref-night'.");
        }
    }
}