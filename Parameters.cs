namespace Derivation
{
    public class Parameters
    {
        #region Properties

        public Parameter X { get;  set; }
        public Parameter Y { get;  set; }
        public Parameter Z { get; set; }

        #endregion

        public Parameters()
        {
            X = new Parameter("x",1);
            Y = new Parameter("y",2);
            Z = new Parameter("z",3);
        }
    }
}
