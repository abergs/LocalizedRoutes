using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LocalizedRoutes.Test
{
    public static class DemoPropertyDataSource
    {
        private static readonly List<object[]> Data
            = new List<object[]>
                {
                    new object[] {"<account>", "<konto>", "konto"},
                    new object[] {"<account>/id", "<konto>","konto/id"},
                    new object[] {"~/<account>/id", "<konto>","~/konto/id"},
                    new object[] {"/<account>/id", "<konto>","/konto/id"},
                    new object[] {"<account>/{id:int}", "<konto>","konto/{id:int}"},
                    new object[] {"<account>/{id:int}/details", "<konto>","konto/{id:int}/details"},
                    new object[] {"<account>/{id:int}/<details>", "<konto>,<detaljer>","konto/{id:int}/detaljer"}
                };

        public static IEnumerable<object[]> TestData
        {
            get { return Data; }
        }
    }
    public class TokenTest
    {
        private RouteAngleBracketsReplacer _replacer;

        public TokenTest()
        {
            _replacer = new RouteAngleBracketsReplacer();
        }

        [Theory]
        [MemberData("TestData", MemberType = typeof(DemoPropertyDataSource))]
        public void TokenReplacementWorks(string template, string translation, string expectation)
        {
            var result = _replacer.ReplaceTokens(template, translation);
            Assert.Equal(result, expectation);
        }
    }
}
