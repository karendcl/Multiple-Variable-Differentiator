namespace Derivation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Derivador";
            while (true)
            {
                Console.Clear();
                WriteFunctions();
                Console.Write("> ");
                Console.ForegroundColor = ConsoleColor.Green;
                string line = Console.ReadLine();
                Console.ResetColor();

                if (line.ToLower() == "salir")
                    break;

                if (line.Length == 0) continue;

                FunctionTree function = ParseFunction(line);

                if (function == null) continue;

                Node[] partialDerivative = GetPartialDerivative(function);

                Console.WriteLine();
                WriteFunction(function.Expression);
                WritePartialDerivative(partialDerivative);
                Console.WriteLine();

                Console.Write("Otra?");
                if (Console.ReadLine().ToLower()[0] == 'n') break;
                else continue;
            }
        }

        public static void WriteFunctions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Se pueden usar las siguientes funciones como estan escritas:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" ln \n log\n sin \n cos \n tan \n sqrt \n a^n \n\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pueden ser usadas las siguientes constantes:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" pi/π \n e \n \n");
            Console.ResetColor();

            Console.Write("Por favor usar:");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" * / - + ^ ( ) ");
            Console.ResetColor();

            Console.Write("solamente");
            Console.Write("\n\nLas variables aceptadas son : ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("x, y, z \n \n");
            Console.ResetColor();

        }
        public static FunctionTree ParseFunction(string input)
        {
            FunctionParser parser = new FunctionParser();

            try
            {
                return parser.Parse(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }

            return null;
        }

        public static void WriteFunction(Node function)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(string.Format("f(x, y, z) = {0}\n", function.ToString()));
        }


        public static Node[] GetPartialDerivative(FunctionTree function)
        {
            Node[] result = new Node[3];
            result[0] = function.Expression.Derive(function.Parameters.X);
            result[1] = function.Expression.Derive(function.Parameters.Y);
            result[2] = function.Expression.Derive(function.Parameters.Z);

            return result;
        }

        public static void WritePartialDerivative(Node[] partialDerivative)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Derivada Parcial x: {partialDerivative[0].ToString()}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Derivada Parcial y: {partialDerivative[1].ToString()}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Derivada Parcial z: {partialDerivative[2].ToString()}");
            Console.ResetColor();
        }

    }
}
