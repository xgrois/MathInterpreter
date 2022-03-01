using Xunit;

namespace Interpreter.Tests
{
    public class LexerParserTest
    {
        [Theory]
        [InlineData("1 + 2 + 3", "((1+2)+3)")]
        [InlineData("1 + 2 * 3", "(1+(2*3))")]
        [InlineData("1 + 2 * 3 - 4 / 5 / 6 * 7", "((1+(2*3))-(((4/5)/6)*7))")]
        public void ConvertInputExpressionToParenthesizedExpression(string inputExpression, string expectedParenthesizedExpression)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            var parser = new Parser(lexer.Tokens);

            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedParenthesizedExpression, actual: root.ToString());
        }

        [Theory]
        [InlineData("-3", "(-3)")]
        [InlineData("-1+2", "((-1)+2)")]
        [InlineData("+1--2", "((+1)-(-2))")]
        [InlineData("-5+3*-7", "((-5)+(3*(-7)))")]
        [InlineData("-+-5*9-6", "(((-(+(-5)))*9)-6)")]
        public void ConvertInputExpressionToParenthesizedExpressionWithUnaryOperations(string inputExpression, string expectedParenthesizedExpression)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            var parser = new Parser(lexer.Tokens);

            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedParenthesizedExpression, actual: root.ToString());
        }

        [Theory]
        [InlineData("(2+3)*4", "((2+3)*4)")]
        [InlineData("-(2+3)", "(-(2+3))")]
        public void ConvertInputExpressionToParenthesizedExpressionWithUnaryOperationsAndParenthesis(string inputExpression, string expectedParenthesizedExpression)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            var parser = new Parser(lexer.Tokens);

            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedParenthesizedExpression, actual: root.ToString());
        }

        [Theory]
        [InlineData("1^2^3", "(1^(2^3))")]
        [InlineData("(1^2)^3", "((1^2)^3)")]
        [InlineData("-(2*2)^3^4", "(-((2*2)^(3^4)))")]
        [InlineData("-.25^(2*2*2/3-1)^3", "(-(0.25^(((((2*2)*2)/3)-1)^3)))")]
        [InlineData("-.25^(2*(-2)*2/3-1)^3+5", "((-(0.25^(((((2*(-2))*2)/3)-1)^3)))+5)")]
        public void ConvertInputExpressionToParenthesizedExpressionWithUnaryOperationsAndParenthesisAndPower(string inputExpression, string expectedParenthesizedExpression)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            var parser = new Parser(lexer.Tokens);

            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedParenthesizedExpression, actual: root.ToString());
        }

        [Theory]
        [InlineData("1!", "(1!)")]
        [InlineData("1!+2!", "((1!)+(2!))")]
        [InlineData("(1!)", "(1!)")]
        [InlineData("-1!", "(-(1!))")]
        [InlineData("-(1!)", "(-(1!))")]
        [InlineData("--1!", "(-(-(1!)))")]
        [InlineData("1^2!^3", "(1^((2!)^3))")]
        [InlineData("1^-2!^3", "(1^(-((2!)^3)))")]
        [InlineData("1^--2!^3", "(1^(-(-((2!)^3))))")]
        [InlineData("1^-2!^-3!", "(1^(-((2!)^(-(3!)))))")]
        [InlineData("-(2+1)!", "(-((2+1)!))")]
        [InlineData("-(3!)!", "(-((3!)!))")]
        public void ConvertInputExpressionToParenthesizedExpressionWithUnaryOperationsAndParenthesisAndPowerAndFactorial(string inputExpression, string expectedParenthesizedExpression)
        {
            // Arrange
            var lexer = new Lexer(inputExpression);
            var parser = new Parser(lexer.Tokens);

            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedParenthesizedExpression, actual: root.ToString());
        }


    }

}
