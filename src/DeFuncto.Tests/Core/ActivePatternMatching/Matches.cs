using System.Collections.Generic;
using DeFuncto.ActivePatternMatching;
using Xunit;
using static DeFuncto.Prelude;

namespace DeFuncto.Tests.Core.ActivePatternMatching
{
    public class Matches
    {
        [Theory(DisplayName = "Matches with the correct pattern")]
        [MemberData(nameof(Functions))]
        public void Works(ActivePatternBase<string, string>[] patterns)
        {
            var result = "correct".ActMatch(patterns, _ => "incorrect");
            Assert.NotEqual("incorrect", result);
            Assert.Equal("correct", result);
        }

        public static IEnumerable<object[]> Functions()
        {
            yield return new object[]
            {
                With(
                    Pattern<string, string, string>(Some, Id),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect")
                )
            };

            yield return new object[]
            {
                With(
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(Some, Id),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect")
                )
            };

            yield return new object[]
            {
                With(
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(Some, Id),
                    Pattern<string, string, string>(_ => None, _ => "incorrect")
                )
            };

            yield return new object[]
            {
                With(
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(_ => None, _ => "incorrect"),
                    Pattern<string, string, string>(Some, Id)
                )
            };
        }
    }
}
