using System.Collections.Generic;

namespace Interpreter.Tests
{
    public class TokenEqualityComparer : IEqualityComparer<Token>
    {
        public bool Equals(Token t1, Token t2)
        {
            if (t2 == null && t1 == null)
                return true;
            else if (t1 == null || t2 == null)
                return false;
            else if (t1.Type == t2.Type && t1.Text == t2.Text)
                return true;
            else
                return false;
        }

        public int GetHashCode(Token t)
        {
            int hCode = t.Text.GetHashCode() * 17 + t.Type.GetHashCode();
            return hCode.GetHashCode();
        }
    }

}
