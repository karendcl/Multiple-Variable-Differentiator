namespace Derivation
{
    public class FunctionTreeBuilder
    {
        public IOperator Operator1;
        public IOperator Operator2;
        public IOperator Operator3;

        public Node Expression1;
        public Node Expression2;
        public Node Expression3;

        public bool SetOperator(IOperator op)
        {
            bool noError = true;

            if (Operator1 == null)
                noError = SetOperator1(op);
            else if (Operator2 == null)
                noError = SetOperator2(op);
            else
                noError = SetOperator3(op);

            return noError;
        }

        public bool LeftIsEmpty()
        {
            return (Expression1 == null);
        }

        public bool SetExpression(Node expr)
        {
            bool noError = true;

            if (Expression1 == null)
                SetExpression1(expr);
            else if (Expression2 == null)
                noError = SetExpression2(expr);
            else
                noError = SetExpression3(expr);

            return noError;
        }

        public Node GetResult()
        {
            if (Operator2 != null)
            {
                if (Expression3 == null)
                    return null;

                ApplyOperator2(Expression3);
            }

            if (Operator1 != null)
            {
                if (Expression2 == null)
                    return null;

                ApplyOperator1(Expression2);
            }

            return Expression1;
        }

        public bool SetOperator1(IOperator op)
        {
            //si la expresion 1 es vacia y la operacion es binaria, hay error
            if (Expression1 == null && !(op is IUnaryOperator))
                return false;

            Operator1 = op;  

            return true;
        }

        public bool SetOperator2(IOperator op)
        {
            if (Expression2 == null)
                return false;

            //si la prioridad de este operador es menor que la del 1
            if (op.Priority <= Operator1.Priority)
            {
                ApplyOperator1(Expression2);
                //se aplica el operador 1 y este pasa a ser el nuevo operador
                Operator1 = op; 
            }
            else
            {
                //sino este pasa a ser el operador 2
                Operator2 = op;
            }

            return true;
        }

        public bool SetOperator3(IOperator op)
        {
            if (Expression3 == null)
                return false;

            if (op.Priority <= Operator2.Priority)
            {
                //si la prioridad de este operador es menor que la del 2, se aplica y se substituye
                ApplyOperator2(Expression3);

                if (op.Priority <= Operator1.Priority)
                {
                    ApplyOperator1(Expression2);
                    Operator1 = op;
                }
                else
                {
                    Operator2 = op;
                }
            }
            else
            {
                //sino se pone como operador 3
                Operator3 = op;
            }

            return true;
        }

        public void SetExpression1(Node expr)
        {
            if (Operator1 != null && Operator1 is IUnaryOperator)
                Expression2 = expr;  //si el operador 1 una operacion unaria, esta expresion se pone como expresion 2 

           
            Expression1 = expr;
        }

        public bool SetExpression2(Node expr)
        {
            if (Operator1 == null)
                return false;

            if (Operator1.Priority == 3)  //si tiene prioridad maxima se aplica
                 ApplyOperator1(expr);
            else
                 Expression2 = expr; //si no se pone como expresion

            return true;
        }

        public bool SetExpression3(Node expr)
        {
            if (Operator2 == null)
                return false;

            if (Expression3 == null)
            {
                if (Operator2.Priority == 2)
                    Expression3 = expr;
                else
                    ApplyOperator2(expr);
            }
            else
            {
                ApplyOperator3(expr);
            }

            return true;
        }

        public void ApplyOperator1(Node expr)
        {
            Expression1 = Operator1.Apply(Expression1, expr);
            Operator1 = null;
            Expression2 = null;
        }

        public void ApplyOperator2(Node expr)
        {
            Expression2 = Operator2.Apply(Expression2, expr);
            Operator2 = null;
            Expression3 = null;
        }

        public void ApplyOperator3(Node expr)
        {
            Expression3 = Operator3.Apply(Expression3, expr);
            Operator3 = null;
        }


    }
}
