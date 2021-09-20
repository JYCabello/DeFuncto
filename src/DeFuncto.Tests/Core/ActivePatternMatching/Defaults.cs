using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.ActivePatternMatching
{
    public class Defaults
    {
        [Fact(DisplayName = "Defaults when all are none")]
        public void Works()
        {
            var result = "input"
                .ActMatch(
                    With(
                        Pattern<string, string, string>(_ => None, _ => "incorrect"),
                        Pattern<string, string, string>(_ => None, _ => "incorrect"),
                        Pattern<string, string, string>(_ => None, _ => "incorrect"),
                        Pattern<string, string, string>(_ => None, _ => "incorrect")
                    ),
                    _ => "correct");
            Assert.NotEqual("incorrect", result);
            Assert.Equal("correct", result);
        }
    }
}
