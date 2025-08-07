namespace RavenDB.Indexing.BrazilianAnalyzer;

public static class CharUtils
{
    public static char RemoveAccentMark(char c)
    {
        // Normalize the character to remove diacritics
        var normalizedString = c.ToString().Normalize(NormalizationForm.FormD);

        // Get the first character without diacritics
        foreach (var ch in normalizedString.Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
        {
            return ch;
        }

        return c;
    }

    public static char ToLower(char c)
    {
        int cInt = c;

        if (c >= 128 || !IsAsciiCasingSameAsInvariant)
        {
            return InvariantTextInfo.ToLower(c);
        }

        if (cInt is >= 65 and <= 90)
        {
            c |= ' ';
        }

        return c;
    }

    // ReSharper disable twice StringLiteralTypo
    private static readonly bool IsAsciiCasingSameAsInvariant = CultureInfo.InvariantCulture.CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0;
    private static readonly TextInfo InvariantTextInfo = CultureInfo.InvariantCulture.TextInfo;
}