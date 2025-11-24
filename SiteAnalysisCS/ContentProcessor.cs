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
            var words = content.Split([' ', '\n', '\r', '\t', '.', ',', '!', '?', '"', '-'], StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (SkipWords.Contains(word.ToLower()))
                {
                    continue;
                }

                if (decimal.TryParse(word, out var number))
                {
                    continue;
                }

                if (_wordCounts.TryGetValue(word, out var count))
                {
                    _wordCounts[word] = count + 1;
                }
                else
                {
                    _wordCounts[word] = 1;
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
