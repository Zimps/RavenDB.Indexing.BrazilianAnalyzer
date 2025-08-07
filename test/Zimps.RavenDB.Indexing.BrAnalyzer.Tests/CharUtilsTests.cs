namespace Zimps.RavenDB.Indexing.BrAnalyzer.Tests;

public class CharUtilsTests
{
    [Theory]
    [InlineData('á', 'a')]
    [InlineData('Á', 'a')]
    [InlineData('ü', 'u')]
    [InlineData('à', 'a')]
    [InlineData('ç', 'c')]
    [InlineData('Ç', 'c')]
    public void ToLowerAndRemovingAccentMarksShouldResultIn(
        char input,
        char expectedOutput
    )
    {
        Assert.Equal(
            expectedOutput,
            CharUtils.RemoveAccentMark(CharUtils.ToLower(input))
        );
    }
}