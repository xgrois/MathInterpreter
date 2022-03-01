using System;

namespace Interpreter
{

    public abstract class Node
    {
        public abstract double Eval();
    }

    public abstract class BinOpNode : Node
    {
        public Node NodeLeft { get; set; }
        public Node NodeRight { get; set; }

        public BinOpNode(Node n1, Node n2)
        {
            NodeLeft = n1;
            NodeRight = n2;
        }

    }


    public abstract class UnaryOpNode : Node
    {
        public Node Node { get; set; }

        public UnaryOpNode(Node n)
        {
            Node = n;
        }
    }

    public class NumberNode : Node
    {
        public double Value { get; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override double Eval()
        {
            return Value;
        }
    }

    public class PlusNode : UnaryOpNode
    {
        public PlusNode(Node n) : base(n) { }

        public override string ToString() => $"(+{Node})";

        public override double Eval()
        {
            return Node.Eval();
        }
    }

    public class MinusNode : UnaryOpNode
    {
        public MinusNode(Node n) : base(n) { }
        public override string ToString() => $"(-{Node})";

        public override double Eval()
        {
            return -Node.Eval();
        }
    }

    public class FactorialNode : UnaryOpNode
    {
        public FactorialNode(Node n) : base(n) { }

        public override string ToString() => $"({Node}!)";

        public override double Eval()
        {
            double d = Node.Eval();
            int n = (int)d; // Warning, we assume user can type 1.2! and we calculate 1! This needs some warning at least

            if ((d > n) || (d < n))
                Console.WriteLine($"WARNING: Factorial of {d}! will be computed as {n}!. Please, use positive integers (1!, 2!, ..) for factorial numbers.");

            return Factorial(n);
        }

        private double Factorial(int n)
        {
            if (n < 0) throw new Exception("Factorial of a negative number? Are you kidding me?");
            if ((n == 0) || (n == 1))
                return 1;

            return n * Factorial(n-1);
        }
    }

    public class AddNode : BinOpNode
    {
        public AddNode(Node n1, Node n2) : base(n1, n2) { }

        public override string ToString() => $"({NodeLeft}+{NodeRight})";

        public override double Eval()
        {
            return NodeLeft.Eval() + NodeRight.Eval();
        }
    }
    public class SubNode : BinOpNode
    {
        public SubNode(Node n1, Node n2) : base(n1, n2) { }

        public override string ToString() => $"({NodeLeft}-{NodeRight})";

        public override double Eval()
        {
            return NodeLeft.Eval() - NodeRight.Eval();
        }
    }
    public class MulNode : BinOpNode
    {
        public MulNode(Node n1, Node n2) : base(n1, n2) { }

        public override string ToString() => $"({NodeLeft}*{NodeRight})";

        public override double Eval()
        {
            return NodeLeft.Eval() * NodeRight.Eval();
        }
    }
    public class DivNode : BinOpNode
    {
        public DivNode(Node n1, Node n2) : base(n1, n2) { }

        public override string ToString() => $"({NodeLeft}/{NodeRight})";

        public override double Eval()
        {
            return NodeLeft.Eval() / NodeRight.Eval();
        }
    }

    public class PowNode : BinOpNode
    {
        public PowNode(Node n1, Node n2) : base(n1, n2) { }

        public override string ToString() => $"({NodeLeft}^{NodeRight})";

        public override double Eval()
        {
            return Math.Pow(NodeLeft.Eval(), NodeRight.Eval());
        }
    }

}
