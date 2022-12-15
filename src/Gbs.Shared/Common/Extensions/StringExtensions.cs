namespace Gbs.Shared.Common.Extensions;

public static class StringExtensions
{
    public static string CapitalizeFirstLetterOfEachWord(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        
        var words = str.Trim().ToLower().Split(' ');
        var capitalizedWords = words.Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1)).ToList();

        return string.Join(" ", capitalizedWords);
    }
}