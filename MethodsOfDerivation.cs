namespace Derivation
{
    public abstract class BinaryNode : Node
    {
        protected string mOp;

        public Node Left { get; protected set; }
        public Node Right { get; protected set; }

        public BinaryNode(Node l, Node r, string Op)
        {
            Left = l;
            Right = r;

            mOp = Op;
        }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Left.ToString(), mOp, Right.ToString());
        }

        public override bool Equals(object obj)
        {
            if (GetType() == obj.GetType()) //si son del mismo tipo se le hace un casteo y verifico que las partes sean iguales
            {
                BinaryNode node = (BinaryNode)obj;

                if (Left.Equals(node.Left) && Right.Equals(node.Right))
                    return true;
            }

            return false;
        }

    }

    public class Negate : Node
    {
        public Node Node { get;  set; }

        public Negate(Node n)
        {
            Node = n;
        }


        public override Node Derive(Parameter p)
        {
            return new Negate(Node.Derive(p)).Simplify();  //retorna la derivada negada
        }

        public override Node Simplify()
        {
            Node = Node.Simplify();

            if (Is0(Node)) //si es 0 retorno 0, pq 0=-0
                return new Number(0.0);

            if (Node is Number) //si es numero, se niega
                return ((Number)Node).Negate();

            return this; //retorna el nodo reducido
        }

        public override string ToString()
        {
            return string.Format("(-{0})", Node.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj is Negate && Node.Equals(((Negate)obj).Node)) //si es del tipo negate y los nodos son iguales
                return true;

            return false;    

        }

    }

    public class Sum : BinaryNode
    {
        public Sum(Node l, Node r): base(l, r, "+"){}

        public override Node Derive(Parameter p)
        {
            return new Sum(Left.Derive(p), Right.Derive(p)).Simplify(); //retorna la suma de las derivadas
        }

        public override Node Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            if (Is0(Left) && Is0(Right)) //0+0=0
                return new Number(0.0);

            if (Is0(Left)) //0+a=a
                return Right;

            if (Is0(Right)) //a+0=a
                return Left;  

            if (Left is Number && Right is Number)
                return ((Number)Left).Add(Right); //si son  numeros, devuelve su suma

            return this; //retorna la misma instancia
        }
    }

    public class Subtract : BinaryNode
    {
        public Subtract(Node l, Node r)
            : base(l, r, "-")
        {
        }


        public override Node Derive(Parameter p)
        {
            //retorna la resta de las derivadas
            return  new Subtract(Left.Derive(p), Right.Derive(p)).Simplify();
        }

        public override Node Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();
            //si ambos son 0, delvuelve 0 :      0-0=0
            if (Is0(Left) && Is0(Right))
                return new Number(0);


            //si el izquierdo es 0, devuelve el derecho negado   0-a=-a
            if (Is0(Left))
                return new Negate(Right);

            //si es derecho es 0, devuelve el izquiedo
            if (Is0(Right))
                return Left;

            //si la derecha esta negada, retorno la suma:   a--b= a+b
            if (Right is Negate)
                return new Sum(Left, ((Negate)Right).Node).Simplify();

            if (Left.Equals(Right))
            return new Number(0);

            //si son numeros, se restan
            if (Left is Number && Right is Number)
                return ((Number)Left).Subtract(Right);

            //retorno la instancia
            return this;
        }
    }

    public class Divide : BinaryNode 
    {
        public Divide(Node l, Node r): base(l, r, "/"){}

        // se aplica la derivada de la division
        public override Node Derive(Parameter p)
        {
            //retorna la division de : la derivada de arriba x abajo, la derivada de abajo x arriba, todo sobre abajo al cuadrado
                Node l = new Multiply(Left.Derive(p), Right).Simplify();
                Node r = new Multiply(Left, Right.Derive(p)).Simplify();

                Node dividend = new Subtract(l, r).Simplify();
                Node divisor = new Power(Right, new Number(2.0)).Simplify();

                return new Divide(dividend, divisor).Simplify();
        }

        public override Node Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            if (Is0(Left))
                return new Number(0.0);

            if (Is0(Right))    
                throw new Exception ("No se puede dividir entre 0");

            if (Is1(Right))
                return Left;

            if (Left is Number && Right is Number)
                return ((Number)Left).Divide(Right);

            if (Left.Equals(Right))
                return new Number(1);

            return this;
        }
    }

    public class Multiply : BinaryNode
    {
        public Multiply(Node l, Node r): base(l, r, "*"){}

        public override Node Derive(Parameter p)
        {
            //devuelve la suma de las derivadas
            Node l = new Multiply(Left.Derive(p), Right);
            Node r = new Multiply(Left, Right.Derive(p));
            return new Sum(l, r).Simplify();
        }

        public override Node Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            if (Is0(Left) || Is0(Right))
                return new Number(0.0);

            if (Is1(Left))
                return Right;

            if (Is1(Right))
                return Left;

            if (Left is Number && Right is Number)
                return ((Number)Left).Multiply(Right).Simplify();

            if (Left.Equals(Right))
            return new Power(Left,new Number(2));    

            return this;
        }
    }

    public class Power : BinaryNode
    {
        public Power(Node l, Node r): base(l, r, "^"){}


        public override Node Derive(Parameter p)
        {
            //si el exponente es numero : x^2
            if (Right is Number)
            {
                //el nuevo exponente va a ser el exponente -1;
                Node exponent = ((Number)Right).Subtract(new Number(1.0));

                //lo eleva al nuevo exponente
                Node power = new Power(Left, exponent);

                //retorna la multiplicacion del exponente viejo por el nuevo power por la derivada de la base
                return new Multiply(new Multiply(Right, power), Left.Derive(p)).Simplify();
            }
            else
            {
                //devuelve a^x
                Node fPowg = new Power(Left, Right);

                return new Multiply(new Multiply(Right.Derive(p), fPowg),new Ln(Left)).Simplify();
            }
        }

        public override Node Simplify()
        {
            Left = Left.Simplify();
            Right = Right.Simplify();

            if (Is0(Right) || Is1(Left))
                return new Number(1);

            if (Is0(Left))
                return new Number(0);

            if (Is1(Right))
                return Left;

            if (Left is Number && Right is Number)
                return ((Number)Left).Power(Right);

            return this;
        }
    }
}
