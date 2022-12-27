namespace Derivation
{
    public abstract class Node
    {
        public abstract Node Derive(Parameter p);
        public abstract Node Simplify();
        public static bool Is0(Node node) //si es 0
        {
            if (node is Number)
                return (node as Number).Value == 0.0;

            return false;
        }

        public static bool Is1(Node node) //si es 1
        {
            if (node is Number)
                return (node as Number).Value == 1.0; 

            return false;
        }

    }

    public class Parameter : Node
    {
        public string mName; //nombre del parametro x , y ,z
        public int mID;  //le da un id a cada parametro

        public Parameter( string name, int id)
        {
            mID = id;
            mName = name;
        }

        public override Node Derive(Parameter p)
        {
            if (mID == p.mID)  //si estoy derivando respecto a esta variable, la derivada es 1
                return new Number(1);

            return new Number(0); //sino se toma como constante, y es 0
        }

        public override Node Simplify()
        {
            return this; //no se simplifica
        }

        public override string ToString()
        {
            return mName;
        }

        public override bool Equals(object obj)
        {
            return (obj is Parameter && mID == ((obj as Parameter).mID)); //son iguales si tienen el mismo nombre
        }

    }
}
