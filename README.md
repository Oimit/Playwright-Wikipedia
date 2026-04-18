# Playwright Wikipedia Tests

Automation tests for the Wikipedia Playwright page using C# and Playwright.

## Tech Stack
- C# / .NET 10
- Playwright
- NUnit
- ExtentReports (HTML report)

## Project Structure
- `Pages/` - Page Object Model classes
- `Tests/` - Test files
- `Helpers/` - Logger, API helper, Text normalizer
- `Logs/` - Test logs (generated on run)
- `Reports/` - HTML report (generated on run)

## Tests
- **Task 1** - Extracts the "Debugging Features" section via UI and API, normalizes both texts and asserts the unique word count is equal
- **Task 2** - Validates that all technology names under "Microsoft Development Tools" are text links
- **Task 3** - Changes the Wikipedia color theme to Dark and validates the change was applied

## How to Run

Install dependencies:
```bash
dotnet restore
```

Run all tests:
```bash
dotnet test --settings pw.runsettings
```

## After Running
- Logs are saved in `Logs/`
- HTML report is saved in `Reports/TestReport.html` - open in browser to view
