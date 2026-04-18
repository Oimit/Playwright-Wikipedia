using System.Text.RegularExpressions;

namespace PlaywrightWikipedia.Helpers
{
    public static class TextNormalizer
    {
        public static string Normalize(string text)
        {
            // Remove ===heading===
            text = Regex.Replace(text, @"={2,}.*?={2,}", "");
            
            // Remove <ref>...</ref>
            text = Regex.Replace(text, @"<ref.*?</ref>", "");
            
            // Remove [15] footnotes
            text = Regex.Replace(text, @"\[\d+\]", "");
            
            // Remove * bullet markers
            text = Regex.Replace(text, @"^\*\s*", "", RegexOptions.Multiline);
            
            // Remove punctuation
            text = Regex.Replace(text, @"[^\w\s]", " ");
            
            // Lowercase
            text = text.ToLower();

            return text;
        }

        public static int CountUniqueWords(string text)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var uniqueWords = new HashSet<string>(words);

            Logger.Log($"[Normalizer] Unique word count: {uniqueWords.Count}");

            return uniqueWords.Count;
        }
    }
}