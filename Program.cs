using System;
using System.Collections.Generic;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {

            Lexer lexer = new Lexer("123+ 45*(2   -6)");
            var alltokens = lexer.GetAllTokens();

            var tokens = lexer.GetAllValidTokens();

        }
    }

    enum TokenType
    {
        Number,
        LeftParenthesis,
        RightParenthesis,
        WhiteSpace,
        Plus,
        Minus,
        Star,
        Splash,
        Hat,
        EndOfLine,
        Unknown
    }

    class Token
    {
        public TokenType Type { get; }
        public int Position { get; }
        public int Length { get; }
        public string Text { get; }
        public Token(TokenType tokenType, int position, int length, string text)
        {
            Type = tokenType;
            Position = position;
            Length = length;
            Text = text;
        }

        public override string ToString()
        {
            return $"Type: {Type}. Text = {Text}";
        }
    }

    class Lexer
    {
        private readonly string _text;
        private int _pos;
        private readonly char _DECIMAL_SEPARATOR = '.';


        public char Current
        {
            get
            {
                if (_pos >= _text.Length)
                    return '\0';
                return _text[_pos]; 
            }
        }


        public Lexer(string text)
        {
            _text = text;
            _pos = 0;
        }

        public List<Token> GetAllTokens()
        {
            ResetPosition();

            List<Token> tokens = new List<Token>();

            Token token;
            do
            {
                token = GetNextToken();
                tokens.Add(token);

            } while (token.Type != TokenType.EndOfLine);
                
            
            return tokens;
        }


        public List<Token> GetAllValidTokens()
        {
            ResetPosition();

            List<Token> tokens = new List<Token>();

            Token token;
            do
            {
                token = GetNextToken();
                if((token.Type != TokenType.WhiteSpace) && (token.Type != TokenType.Unknown) && (token.Type != TokenType.EndOfLine))
                    tokens.Add(token);

            } while (token.Type != TokenType.EndOfLine);


            return tokens;
        }

        private void ResetPosition()
        {
            _pos = 0;
        }

        public Token GetNextToken()
        {
            char c = Current;

            if (c == '\0')
                return new Token(TokenType.EndOfLine, _pos++, 1, c.ToString());

            // Open parenthesis
            if (c == '(')
                return new Token(TokenType.LeftParenthesis, _pos++, 1, c.ToString());

            // Close parenthesis
            if (c == ')')
                return new Token(TokenType.RightParenthesis, _pos++, 1, c.ToString());

            // Operator
            if (c == '+')
                return new Token(TokenType.Plus, _pos++, 1, c.ToString());
            else if (c == '-')
                return new Token(TokenType.Minus, _pos++, 1, c.ToString());
            else if (c == '*')
                return new Token(TokenType.Star, _pos++, 1, c.ToString());
            else if (c == '/')
                return new Token(TokenType.Splash, _pos++, 1, c.ToString());
            else if (c == '^')
                return new Token(TokenType.Hat, _pos++, 1, c.ToString());


            // Number (integer)
            if (Char.IsDigit(c))
            {
                int start = _pos;
                while (Char.IsDigit(Current))
                    _pos++;

                int length = _pos - start;
                return new Token(TokenType.Number, start, length, _text.Substring(start, length));
            }

            // Space(s)
            if (Char.IsWhiteSpace(c))
            {
                int start = _pos;
                while (Char.IsWhiteSpace(Current))
                    _pos++;

                int length = _pos - start;
                return new Token(TokenType.WhiteSpace, start, length, _text.Substring(start, length));
            }

            return new Token(TokenType.Unknown, _pos++, 1, c.ToString());

        }



    }

    class Parser
    {
        public Parser(string text)
        {
            var lexer = new Lexer(text);
            var tokens = lexer.GetAllValidTokens();
        }
    }
}
