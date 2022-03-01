using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    /// <summary>
    /// This interpreter does the actual math
    /// Given the root node of the parsed tree
    /// it computes the final value
    /// 
    /// This interpreter just visits all tree nodes
    /// generated from Parser. All nodes have an Eval()
    /// function that makes this interpreter extremely short
    /// 
    /// Just need to call the Eval() of the root
    /// and nodes will do their job!
    /// 
    /// NOTE: you can create another interpreter that receives 
    /// the full parenthesized expression instead of the root node.
    /// That interpreter would need different algorithms, e.g., 
    /// converting the full parenthesized expression in pre/post-fix mode
    /// and then evaluate it.
    /// In our case, it does not make much sense since we already have
    /// generated the abstract syntax tree with the Parser. With this
    /// approach the computation is absolutely magic "_root.Eval()".
    /// 
    /// Since our Lexer - Parser - Interpreter are quite dependent 
    /// it might have more sense to embeed the Lexer and Parser in the Interpreter
    /// 
    /// </summary>
    public class Interpreter
    {
        private readonly Node _root;

        public Interpreter(Node root)
        {
            _root = root;
        }

        public double Eval()
        {
           return _root.Eval();
        }

    }
}
