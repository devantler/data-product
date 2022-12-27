namespace Devantler.StringHelpers.Test;

public class StringExtensionsTest
{
    private const string SINGLE_LINE_TEXT = "This is a test";
    private const string MULTI_LINE_TEXT =
        """
        First line
        Second line
        """;

    [Fact]
    public void IndentBy_WithSingleLineText_IndentsByFourSpaces()
    {
        var expected = $"    {SINGLE_LINE_TEXT}";
        var actual = SINGLE_LINE_TEXT.IndentBy();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndentBy_WithMultiLineText_IndentsByFourSpaces()
    {
        const string EXPECTED =
            """
                First line
                Second line
            """;
        var actual = MULTI_LINE_TEXT.IndentBy();
        Assert.Equal(EXPECTED, actual);
    }

    [Fact]
    public void IndentBy_WithSingleLineTextAndSpacesArgument_IndentsBySpecifiedSpaces()
    {
        var expected = $"    {SINGLE_LINE_TEXT}";
        var actual = SINGLE_LINE_TEXT.IndentBy(4);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndentBy_WithMultiLineTextAndSpacesArgument_IndentsBySpecifiedSpaces()
    {
        const string EXPECTED =
            """
                First line
                Second line
            """;
        var actual = MULTI_LINE_TEXT.IndentBy(4);
        Assert.Equal(EXPECTED, actual);
    }

    [Fact]
    public void IndentBy_WithNegativeSpacesArgument_ThrowsArgumentOutOfRangeException() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => SINGLE_LINE_TEXT.IndentBy(-1));

    [Fact]
    public void ToPascalCase_WithSingleWord_ReturnsPascalCase()
    {
        const string TEXT = "test";
        const string EXPECTED = "Test";
        var actual = TEXT.ToPascalCase();
        Assert.Equal(EXPECTED, actual);
    }

    [Fact]
    public void ToPascalCase_WithMultipleWords_ReturnsPascalCase()
    {
        const string TEXT = "this is a test";
        const string EXPECTED = "ThisIsATest";
        var actual = TEXT.ToPascalCase();
        Assert.Equal(EXPECTED, actual);
    }

    [Fact]
    public void ToPascalCase_WithCamelCase_ReturnsPascalCase()
    {
        const string TEXT = "thisIsATest";
        const string EXPECTED = "ThisIsATest";
        var actual = TEXT.ToPascalCase();
        Assert.Equal(EXPECTED, actual);
    }
}
