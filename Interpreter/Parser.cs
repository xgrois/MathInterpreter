using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    /// <summary>
    /// Parser generates a unambiguous math expression given a list of tokens
    /// 
    /// It is built for this specific syntax grammar:
    /// expr ::= term { addop term }                                 ---> {} means 0 or more
    /// term ::= factor { mulop factor }
    /// factor ::= atom ['^' factor] | addop factor ['^' factor]     ---> [] means 0 or 1
    /// atom ::= (  number | '(' expr ')'  ) ['!']                   ---> (a | b) means a or b
    /// 
    /// where
    /// 
    /// addop ::= ('+' | '-')
    /// mulop ::= ('*' | '/')
    /// 
    /// With shorter syntax
    /// E ::= T { addop T }
    /// T ::= F { mulop F }
    /// F ::= A ['^' F] | addop F ['^' F]
    /// A ::= (  number | '(' E ')'  ) ['!']  
    /// 
    /// A number is any double (1, 12.3, .5)
    /// 
    /// The parser builds a tree using recursion
    /// You can access to a full parenthesized expression (unambiguous)
    /// by printing the root node (string)
    /// 
    /// 1+2*3 = (1+(2*3))
    /// (1+2)*3 = ((1+2)*3)
    /// -(-2+3) = (-((-2)+3))
    /// 1*2*3*4 = (((1*2)*3)*4)
    /// 1^2^3^4 = (1^(2^(3^4)))
    /// 1+2+3*4/5^6^7 = ((1+2)+((3*4)/(5^(6^7))))
    /// 
    /// This generated tree (or the full parenthesized expression)
    /// can be used by an interpreter
    /// to evaluate the expression without ambiguities
    /// 
    /// </summary>
    public class Parser
    {
        private readonly List<Token> _tokens;

        private int _position;

        public Token Current
        {
            get
            {
                return _tokens[_position];
            }
        }

        private void Advance()
        {
            if (_position < _tokens.Count() - 1)
                _position++;
        }

        private void Back() => _position--;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _position = 0;
        }

        public Node GetAST()
        {
            var root = Expr();

            if (Current.Type != TokenType.EOF) throw new Exception("Expression is not valid.");

            return root;
        }

        private Node Expr()
        {
            // expr ::= term { addop term }

            var result = Term();

            while ((Current.Type == TokenType.Plus) || (Current.Type == TokenType.Minus))
            {
                if (Current.Type == TokenType.Plus)
                {
                    Advance();
                    result = new AddNode(result, Term());
                }
                else if (Current.Type == TokenType.Minus)
                {
                    Advance();
                    result = new SubNode(result, Term());
                }
            }

            return result;

        }


        private Node Term()
        {
            // term ::= factor { mulop factor }

            var result = Factor();

            while ((Current.Type == TokenType.Mult) || (Current.Type == TokenType.Div))
            {
                if (Current.Type == TokenType.Mult)
                {
                    Advance();
                    result = new MulNode(result, Factor());
                }
                else if (Current.Type == TokenType.Div)
                {
                    Advance();
                    result = new DivNode(result, Factor());
                }
            }

            return result;

        }

        private Node Factor()
        {
            // addop ::= ('+' | '-')

            // factor ::= atom ['^' factor] | addop factor ['^' factor]

            Token token = Current;

            Node result;

            if (token.Type == TokenType.Plus)
            {
                Advance();
                result = new PlusNode(Factor());
            }
            else if (token.Type == TokenType.Minus)
            {
                Advance();
                result = new MinusNode(Factor());
            }
            else
            {
                result = Atom();
            }
            
            if (Current.Type == TokenType.Pow)
            {
                Advance();
                return new PowNode(result, Factor());
            }

            return result;

        }

        private Node Atom()
        {
            // atom ::= (  number | '(' expr ')'  ) [!] 

            Token token = Current;
            Node result = null;

            if (token.Type == TokenType.OpenPar)
            {
                Advance();
                result = Expr();

                if (Current.Type != TokenType.ClosePar)
                    throw new Exception("Expected ')' but none found. Expression is not valid.");

            }
            else if (token.Type == TokenType.Number)
            {
                result = new NumberNode(double.Parse(token.Text));
            }

            Advance();
            if (Current.Type == TokenType.Factorial)
            {
                Advance();
                return new FactorialNode(result);
            }

            return result;
           

        }


    }
}
