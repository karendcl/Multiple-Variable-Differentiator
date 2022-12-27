namespace Derivation
{
    public class OperatorAdd : IOperator
    {
        public int Priority { get;  set; }

        public OperatorAdd()
        {
            Priority = 1; //adicion prioridad 1
        }

        public Node Apply(Node l, Node r)
        {
            return new Sum(l, r);
        }
    }

    public class OperatorSubtract : IOperator
    {
        public int Priority { get;  set; }

        public OperatorSubtract()
        {
            Priority = 1;  //restar prioridad 1
        }

        public Node Apply(Node l, Node r)
        {
            return new Subtract(l, r);
        }
    }

    public class OperatorDivide : IOperator
    {
        public int Priority { get;  set; }

        public OperatorDivide()
        {
            Priority = 2;  //dividir prioridad 2
        }

        public Node Apply(Node l, Node r)
        {
            return new Divide(l, r);
        }
    }

    public class OperatorMultiply : IOperator
    {
        public int Priority { get;  set; }

        public OperatorMultiply()
        {
            Priority = 2; //multiplicar prioridad 2
        }

        public Node Apply(Node l, Node r)
        {
            return new Multiply(l, r);
        }
    }

    public class OperatorPower : IOperator
    {
        public int Priority { get;  set; }

        public OperatorPower()
        {
            Priority = 3;  //elevar prioridad 3
        }

        public Node Apply(Node l, Node r)
        {
            return new Power(l, r);
        }
    }

    public class OperatorNegate : IOperator, IUnaryOperator
    {
        public int Priority { get;  set; }

        public OperatorNegate()
        {
            Priority = 1;  //negar prioridad 1
        }

        public Node Apply(Node l, Node r)
        {
            if (r is Number && ((Number)r).Value == 0)
                return r;

            return new Negate(r);
        }
    }
}
