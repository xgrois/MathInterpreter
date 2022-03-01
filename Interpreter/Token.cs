using System.Text.Json;

namespace Interpreter
{
    public enum TokenType
    {
        Number,
        Plus,
        Minus,
        Mult,
        Div,
        OpenPar,
        ClosePar,
        EOF,
        Pow,
        Factorial
    }

    public class Token
    {
        public string Text { get; } // Only null if EOF type
        public TokenType Type { get; }

        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text;
        }

        public override string ToString()
        {
            return $"Token [ Type: {Type}, Text: '{Text}' ]";
        }

        public string ToJSON()
        {
            string jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }

    }
}
