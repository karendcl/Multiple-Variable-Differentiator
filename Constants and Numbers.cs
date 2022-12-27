namespace Derivation
{
    public class ConstantNode : Node
    { 
        public string mToken; //tiene el nombre de la constante (e o π)

        public ConstantNode(string token)
        {
            mToken = token;
        }

        public override Node Derive(Parameter p)
        {
            //derivada de una constante es 0
            return new Number(0);
        }

        public override Node Simplify()
        {
            return this;
        }

        public override string ToString()
        {
            return mToken;
        }

        public override bool Equals(object obj)
        {
            if (GetType() == obj.GetType()) return true;
            return false;;
        }
    }

    public class E : ConstantNode
    {
        public E(): base("e"){}//constructor
    }

    public class PI : ConstantNode
    {
         public PI(): base("π"){} //constructor //alt227
    }

    public class Number : Node
    {
        public double Value { get; set; } //el valor del numero

        public Number(double value)
        {
            Value = value;
        }


        public override Node Derive(Parameter p)
        {
            //la derivada de un numero es 0
            return new Number(0);
        }

        public override Node Simplify()
        {
            return this;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Number && Value == ((Number)obj).Value)
                return true;

            return false;
        }

        public Node Add(Node node)
        {
            //se hacen operaciones como numeros
            return new Number(Value + ((Number)node).Value);
        }

        public Node Subtract(Node node)
        {
            return new Number(Value - ((Number)node).Value);
        }

        public Node Multiply(Node node)
        {
            return new Number(Value * ((Number)node).Value);
        }

        public Node Power(Node node)
        {
            return new Number(Math.Pow(Value, ((Number)node).Value));
        }

        public Node Divide(Node node)
        {
            return new Number(Value / ((Number)node).Value);
        }

        public Node Negate()
        {
            return new Number(-Value);
        }

    }
}
