namespace Derivation
{
    public class FunctionParser
    {
        public int Index;  //variable que va a llevar mi indice al leer la funcion
        public string Input;  //variable que contiene la funcion
        public Parameters Params;  //para hacer los nodos de variables

        public FunctionTree Parse(string input) //constructor
        {
            Index = 0;
            Input = input;
            Params = new Parameters();

            return new FunctionTree(Params, Parse(false)); //false no hay parentesis abiertos
        }

        public Node Parse(bool Par)
        {
            FunctionTreeBuilder expression = new FunctionTreeBuilder();

            while (Index < Input.Length)  //ver cada letra del input
            {
                switch (Input[Index])
                {
                    case ' ':
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                    case ',':
                        IsNumber(expression);
                        break;
                    case 'x':
                    case 'X':
                        SetExpression(expression, Params.X);
                        break;
                    case 'y':
                    case 'Y':
                        SetExpression(expression, Params.Y);
                        break;
                    case 'z':
                    case 'Z':
                        SetExpression(expression, Params.Z);
                        break;
                    case '+':
                        SetOperator(expression, new OperatorAdd());
                        break;
                    case '-':
                        HandleMinus(expression);
                        break;
                    case '/':
                        SetOperator(expression, new OperatorDivide());
                        break;
                    case '*':
                        SetOperator(expression, new OperatorMultiply());
                        break;
                    case '^':
                        SetOperator(expression, new OperatorPower());
                        break;
                    case '(':
                        Index++;
                        SetExpression(expression, Parse(true));
                        break;
                    case ')':
                        if (!Par)
                            throw new Exception("No hay parentesis abiertos pero has intentado cerrar uno");
                        return GetResultExpression(expression);
                    case 'c':
                    case 'C':
                        if (ISWord("cos"))
                        {
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Cos(Parse(true)));
                        }
                        else
                            throw  new NotImplementedException();
                        break;
                    case 'e':
                    case 'E':
                         if (ISWord("e"))
                            SetExpression(expression, new E());
                        break;
                    case 'l':
                    case 'L':
                        if (ISWord("ln"))
                        {
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Ln(Parse(true)));
                        }
                        else
                        if (ISWord("log"))
                        {
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Log(Parse(true)));
                        }
                        break;
                    case 'p':
                    case 'P':
                        if (ISWord("pi"))
                            SetExpression(expression, new PI());
                            break;
                    case 'π':
                    if (ISWord("π"))
                            SetExpression(expression, new PI());
                            break;
                    case 's':
                    case 'S':
                        if (ISWord("sin"))
                        {
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Sin(Parse(true)));
                        }
                        else if (ISWord("sqrt"))
                        {
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Sqrt(Parse(true)));
                        }
                        break;
                    case 't':
                    case 'T':
                        if (ISWord("tan")){
                            MoveIndexForParenthesis();
                            SetExpression(expression, new Tan(Parse(true)));
                        }
                        break;
                    default:
                        throw new Exception("Ocurrio algun error");
                }

                Index++;
            }

            if (Par)
                throw new Exception("Cierre los parentesis");

            return expression.GetResult();
        }

        public void IsNumber(FunctionTreeBuilder expression)
        {
            bool point = (Input[Index] == '.' || Input[Index]==',');  //si en esa posicion hay un punto
            int begin = Index++; //comiezo desde una posiscion mas arriba

            while (Index < Input.Length)
            {
                if ((Input[Index] == '.') || (Input[Index]==','))
                {
                    if (point)  //si hay dos puntos en el mismo numero, hay algo mal
                        throw new Exception("Hay dos comas a la vez");

                    point = true;
                }
                else if (!char.IsDigit(Input[Index]))  //si se acaban los numeros me salgo
                {
                    break;
                }

                Index++;
            }

            Index--;

            if (Input[Index] == '.')  //si es solo un punto, hay error
                throw new Exception("Escribio algo mal");
            

            //copio el numero y lo hago una expresion
            double d = double.Parse(Input.Substring(begin, Index - begin + 1));
            SetExpression(expression, new Number(d));
        }

        public void SetExpression(FunctionTreeBuilder expression, Node expr)
        {
            if (!expression.SetExpression(expr)) 
                throw new Exception("Ocurrio algun error");
        }

        public void HandleMinus(FunctionTreeBuilder expression)
        {
            if (expression.LeftIsEmpty())  //si la izquierda es vacia, es que no es una resta, sino una negacion
                SetOperator(expression, new OperatorNegate());
            else
                SetOperator(expression, new OperatorSubtract());
        }

        public void SetOperator(FunctionTreeBuilder expression, IOperator op)
        {
            if (!expression.SetOperator(op))
                throw new Exception("Ocurrio algn error");
        }

        public bool ISWord(string name)
        {
            
            if (Input.Substring(Index, name.Length).ToLower() == name)
            {
                Index += name.Length - 1;
                return true;
            }

            return false;
        }

        public void MoveIndexForParenthesis()
        {
            Index++;

            while (Index < Input.Length && Input[Index] == ' ') //me paro en donde no haya un espacio
                Index++;

            if (Index >= Input.Length || Input[Index] != '(') //si me fui de la longitud
               throw new Exception("Ocurrio un error al escribir la funcion");

            Index++;
        }

        public Node GetResultExpression(FunctionTreeBuilder expression)
        {
            Node result = expression.GetResult();

            if (result == null)
                throw new Exception("Ocurrio un error en el algoritmo");

            return result;
        }
    }
}
