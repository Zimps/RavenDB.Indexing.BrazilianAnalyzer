namespace RavenDB.Indexing.BrazilianAnalyzer;

public sealed class RavenBrazilianAnalyzer(Version matchVersion) : StandardAnalyzer(matchVersion)
{
    private readonly Version _matchVersion = matchVersion;

    public override TokenStream TokenStream(string fieldName, TextReader reader)
    {
        var tokenStream = new StandardTokenizer(_matchVersion, reader)
        {
            MaxTokenLength = DEFAULT_MAX_TOKEN_LENGTH
        };

        var res = new RavenBrazilianFilter(tokenStream);
        PreviousTokenStream = res;
        return res;
    }

    public override TokenStream ReusableTokenStream(string fieldName, TextReader reader)
    {
        if (PreviousTokenStream is not RavenBrazilianFilter previousTokenStream)
        {
            return TokenStream(fieldName, reader);
        }

        // if the inner tokenazier is successfuly reset                  // we failed so we generate a new token stream
        return previousTokenStream.Reset(reader) ? previousTokenStream : TokenStream(fieldName, reader);
    }
}