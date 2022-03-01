using System.Collections;
using System.Collections.Generic;

namespace Interpreter.Tests
{
    public class DataForLexer : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            { 
                "", 
                new List<Token>() 
                { 
                    new Token(TokenType.EOF, "") 
                } 
            };

            yield return new object[]
            {
                "1", 
                new List<Token>() 
                { 
                    new Token(TokenType.Number, "1"),
                    new Token(TokenType.EOF, "")
                } 
            };

            yield return new object[]
            {
                ".5",
                new List<Token>() { 
                    new Token(TokenType.Number, "0.5"),
                    new Token(TokenType.EOF, "")
                } 
            };

            yield return new object[]
            { 
                "1 + 2 * 3", 
                new List<Token>() 
                { 
                    new Token(TokenType.Number, "1"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Mult, "*"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.EOF, "")
                } 
            };

            yield return new object[]
            {
                "(1. + 276/9) * .5",
                new List<Token>()
                {
                    new Token(TokenType.OpenPar, "("),
                    new Token(TokenType.Number, "1.0"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "276"),
                    new Token(TokenType.Div, "/"),
                    new Token(TokenType.Number, "9"),
                    new Token(TokenType.ClosePar, ")"),
                    new Token(TokenType.Mult, "*"),
                    new Token(TokenType.Number, "0.5"),
                    new Token(TokenType.EOF, "")
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
