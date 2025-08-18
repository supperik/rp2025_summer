using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    public static string ReverseWords(string text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Replace(text, match =>
        {
            string word = match.Value;
            char[] chars = word.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        });
    }
}
