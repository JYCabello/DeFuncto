using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.ActivePatternMatching
{
    public class Matches
    {
        [Fact(DisplayName = "Matches with the correct pattern")]
        public void Works()
        {
            var result = "correct"
                .ActMatch(
                    With(
                        Pattern<string, string, string>(_ => None, _ => "incorrect"),
                        Pattern<string, string, string>(Some, Id),
                        Pattern<string, string, string>(_ => None, _ => "incorrect"),
                        Pattern<string, string, string>(_ => None, _ => "incorrect")
                    ),
                    _ => "incorrect");
            Assert.NotEqual("incorrect", result);
            Assert.Equal("correct", result);
        }
    }
}
