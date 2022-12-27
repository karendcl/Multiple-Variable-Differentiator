namespace Derivation
{
    public abstract class FunctionNode : Node
    {
        public string mToken; //contiene el nombre de la funcion
        public Node Node { get; protected set; } //es el nodo de adentro de la funcion
                                                 //ej. cos(2*x+65)

        public FunctionNode(Node node, string token)
        {
            Node = node;
            mToken = token;
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", mToken, Node.ToString());
        }

    }

    public class Sin : FunctionNode
    {
        public Sin(Node node): base(node, "Sin"){}

        public override Node Derive(Parameter p)
        {
            //la derivada del seno
            return new Multiply(new Cos(Node), Node.Derive(p)).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();
            
            //sen pi es 0
            if (Node is PI)
                return new Number(0);

            if (Node is Number){
                return new Number(Math.Sin( (Node as Number).Value));
            }    

            return this;
        }
    }


    public class Cos : FunctionNode
    {
        public Cos(Node node): base(node, "Cos"){}

        public override Node Derive(Parameter p)
        {
            //derivada de coseno
            return new Multiply(new Negate(new Sin(Node)), Node.Derive(p)).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

            //cos de pi es -1
            if (Node is PI)
                return new Number(-1);  

            if (Node is Number){
                return new Number(Math.Cos( (Node as Number).Value));
            }    

            return this;
        }
    }

     public class Tan : FunctionNode
    {
        public Tan(Node node): base(node, "Tan"){}
        
        public override Node Derive(Parameter p)
        {
            //deriva la tangente
            return new Divide(Node.Derive(p), new Power(new Cos(Node), new Number(2))).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

            if (Node is Number){
                return new Number(Math.Tan( (Node as Number).Value));
            }

            return this;
        }
    }

    public class Sqrt : FunctionNode
    {
        public Sqrt(Node node): base(node, "Sqrt"){}


        public override Node Derive(Parameter p)
        {
            //derivar la raiz cuadrada
            Node denom = new Multiply(new Number(2), new Sqrt(Node));      

            return new Divide(Node.Derive(p), denom).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

             if (Node is Number){
                return new Number(Math.Sqrt( (Node as Number).Value));
            }

            return this;
        }
    }

    public class Ln : FunctionNode
    {
        public Ln(Node node) : base(node, "Ln"){}

        public override Node Derive(Parameter p)
        {
            //deriva el logaritmo neperiano
            return new Divide(Node.Derive(p), Node).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

            if (Node is E)
            return new Number(1);

            if (Node is Number)
            return (new Number(Math.Log((Node as Number).Value)));

            return this;
        }
    }

    public class Log : FunctionNode
    {
        public Log(Node node): base(node, "Log"){}

        public override Node Derive(Parameter p)
        {
            //deriva el logaritmo
            return new Divide(Node.Derive(p), new Multiply(Node, new Ln(new Number(10)))).Simplify();
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

            //log de 1 es 0
             if (Node is Number){
                return new Number(Math.Log10((Node as Number).Value));
            }
            return this;
        }
    }
}
