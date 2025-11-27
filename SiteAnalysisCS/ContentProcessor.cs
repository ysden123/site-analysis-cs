namespace SiteAnalysisCS
{
    public class ContentProcessor
    {
        private static readonly HashSet<string> SkipWords =
            [
              "на",
              "по",
              "не",
              "из",
              "как",
              "что",
              "за",
              "раз",
              "без",
              "безо",
              "близ",
              "в",
              "во",
              "вместо",
              "вне",
              "для",
              "до",
              "за",
              "из",
              "и",
              "изо",
              "из - за",
              "из - под",
              "к",
              "ко",
              "кроме",
              "между",
              "на",
              "над",
              "надо",
              "о",
              "об",
              "обо",
              "от",
              "ото",
              "перед",
              "передо",
              "пред",
              "предо",
              "пo",
              "под",
              "подо",
              "при",
              "про",
              "ради",
              "с",
              "со",
              "сквозь",
              "среди",
              "у",
              "через",
              "чрез",
              "когда",
              "после",
              "но",
              "его",
              "ее",
              "её",
              "ли",
              "чем",
              "чтобы"
            ];

        private readonly Dictionary<string, int> _wordCounts = [];

        public void ProcessContent(string content)
        {
            var words = content
                .Replace("&nbsp;", " ")
                .Split([' ', '\n', '\r', '\t', '.', ',', '!', '?', '"', '-', '(', ')', ':'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (word.Length < 2)
                {
                    continue;
                }

                var wordLower = word.ToLower();
                if (SkipWords.Contains(wordLower))
                {
                    continue;
                }

                if (decimal.TryParse(wordLower, out _))
                {
                    continue;
                }

                if (_wordCounts.TryGetValue(wordLower, out var count))
                {
                    _wordCounts[wordLower] = count + 1;
                }
                else
                {
                    _wordCounts[wordLower] = 1;
                }
            }
        }

        public List<WordData> GetWordCounts()
        {
            return [.. (from item in _wordCounts
                        where item.Value > 1
                       orderby item.Value descending
                       select new WordData { Word = item.Key, Count = item.Value }).Take(15)];
        }
    }
}
