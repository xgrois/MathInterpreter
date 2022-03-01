using System.Collections.Generic;
using Xunit;

namespace Interpreter.Tests
{
    public class LexerTests
    {
        [Theory]
        [ClassData(typeof(DataForLexer))]
        public void Tokenize(string inputExpression, List<Token> expectedTokens)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            // Act
            var tokens = lexer.Tokens;

            // Assert
            Assert.Equal( expectedTokens, tokens, new TokenEqualityComparer());
        }

    }

}
