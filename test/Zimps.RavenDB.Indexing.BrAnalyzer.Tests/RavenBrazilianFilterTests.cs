namespace Zimps.RavenDB.Indexing.BrAnalyzer.Tests;

public class RavenBrazilianFilterTests
{
    [Theory]
    [InlineData("À moda Mem de sá", new [] {"moda", "mem", "sa"})]
    [InlineData("Guaraná Fantástica", new[] { "guarana", "fantastica"})]
    public void Case1(string input, string[] expectedTokens)
    {
        var tokenizer = new StandardTokenizer(Version.LUCENE_30, new StringReader(input));
        TokenStream sut = new RavenBrazilianFilter(tokenizer);

        var term = sut.AddAttribute<ITermAttribute>();

        var index = 0;
        sut.Reset();

        while (sut.IncrementToken())
        {
            Assert.Equal(expectedTokens[index], term.Term);
            index++;
        }

        Assert.Equal(expectedTokens.Length, index);
    }
}