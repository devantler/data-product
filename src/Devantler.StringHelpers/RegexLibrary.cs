using System.Text.RegularExpressions;

namespace Devantler.StringHelpers;

public static partial class RegexLibrary
{
    // Find word parts using the following rules:
    // 1. all lowercase starting at the beginning is a word
    // 2. all caps is a word.
    // 3. first letter caps, followed by all lowercase is a word
    // 4. the entire string must decompose into words according to 1,2,3.
    // Note that 2&3 together ensure MPSUser is parsed as "MPS" + "User".
    [GeneratedRegex("[A-Z][a-z]+|[a-z]+|[A-Z]")]
    public static partial Regex WordsRegex();
}
