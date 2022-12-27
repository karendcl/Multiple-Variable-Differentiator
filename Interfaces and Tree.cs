namespace Derivation
{
    public interface IOperator
    {
        int Priority { get; } //cada operador tiene una prioridad
        Node Apply(Node l, Node r);
    }

    public interface IUnaryOperator {} //operadores unarios 

     public class FunctionTree
    {
        public Parameters Parameters { get;  set; }
        public Node Expression { get;  set; }

        public FunctionTree(Parameters parameters, Node expression)
        {
            Parameters = parameters;
            Expression = expression;
        }
    }
}