using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{

    /// <summary>
    /// Lexer receives math expression as a string and generates a list of tokens: PLUS, MINUS, NUMBER, etc.
    /// The list of tokens can be used as the input for a Parser
    /// 
    /// Example:
    /// input: "1+ .3"
    /// output: 
    ///     token 1 --> Type = Number, Text = "1"
    ///     token 2 --> Type = Plus, Text = "+"
    ///     token 3 --> Type = Number, Text = "0.3"
    /// 
    /// </summary>
    public class Lexer
    {
        private readonly char[] _expression;

        private int _position;

        public char Current 
        {
            get
            {
                if (_position >= _expression.Length)
                    return '\0';
                return _expression[_position];
            }
        }

        private List<Token> _tokens;

        public List<Token> Tokens
        {
            get { return _tokens; }
        }

        private void Advance() => _position++;
        private void Back() => _position--;


        public Lexer(string expression)
        {
            _expression = expression.ToCharArray();
            _position = 0;
            _tokens = new List<Token>();

            Tokenize();
        }

        private void Tokenize()
        {
            // Current can be [0-9], '.', '+-*/', '()', '!', space, or a char that we dont recognize as valid
            while (Current != '\0')
            {
                if (Current == '+')
                    _tokens.Add(new Token(TokenType.Plus, "+"));

                else if (Current == '-')
                    _tokens.Add(new Token(TokenType.Minus, "-"));

                else if (Current == '*')
                    _tokens.Add(new Token(TokenType.Mult, "*"));

                else if (Current == '/')
                    _tokens.Add(new Token(TokenType.Div, "/"));

                else if (Current == '^')
                    _tokens.Add(new Token(TokenType.Pow, "^"));

                else if (Current == '(')
                    _tokens.Add(new Token(TokenType.OpenPar, "("));

                else if (Current == ')')
                    _tokens.Add(new Token(TokenType.ClosePar, ")"));

                else if (Current == '!')
                    _tokens.Add(new Token(TokenType.Factorial, "!"));

                else if ((Current == '.') || (Char.IsDigit(Current)))
                    TryTokenizeNumber();

                else if (Char.IsWhiteSpace(Current)) { }

                else // char does not belong to recognized tokens
                    throw new Exception($"Invalid character for available tokens (position: {_position}, character: '{Current}')");
                

                Advance();

            }

            // out of while

            _tokens.Add(new Token(TokenType.EOF, ""));

        }

        private void TryTokenizeNumber()
        {
            int dots = 0;
            StringBuilder sb = new StringBuilder();
            while ((Char.IsDigit(Current) || (Current == '.')) && (Current != '\0'))
            {
                if (Current == '.') dots++;

                sb.Append(Current);

                Advance();
            }

            if ((dots == 1) && (sb.Length == 1))
                throw new Exception("Numbers cannot consist of a single '.'. Decimal representation requires '.x' or 'x.x' format."); 
            if (dots > 1)
                throw new Exception("Numbers cannot have more than one decimal separator ('.').");

            // Everything was ok, so create the number token and add it to the list
            if (sb[0] == '.') sb.Insert(0, "0");
            if (sb[sb.Length - 1] == '.') sb.Append("0");
            _tokens.Add(new Token(TokenType.Number, sb.ToString()));
            Back();
        }

        private  bool IsOp(char c)
        {
            return (c == '+') || (c == '-') || (c == '*') || (c == '/');
        }


    }
}
