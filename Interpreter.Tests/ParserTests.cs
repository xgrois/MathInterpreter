using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Interpreter.Tests
{

    public class ParserTests
    {
        [Theory]
        [ClassData(typeof(DataForParser))]
        public void Parse(List<Token> inputTokens, string expectedResult)
        {
            // Arrange
            var parser = new Parser(inputTokens);
            // Act
            var root = parser.GetAST();

            // Assert
            Assert.Equal(expected: expectedResult, actual: root.ToString());
        }


    }

}
