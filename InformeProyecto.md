-Karen Dianelis Cantero Lopez
-Alejandro Solar Ruiz
-Luis Alejandro Rodriguez Otero
-Luis Manuel Gamboa


# Informe del Proyecto #

Con este proyecto, se ha logrado crear un programa funcional capaz de hallar la derivada de una función de una o varias variables con respecto a cualquiera de sus variables. Para lograr esto, se ha creado una jerarquía de clases, dentro de las cuales se logró establecer un orden de prioridades en cuanto a las operaciones matemáticas que se encuentren en la función que se este tratando. También, se crearon clases abstractas que garantizan que cualquier función heredera de dicha clase sea capaz de derivarse, y simplificarse como expresión.

#### Clase `Node` ####

Esta clase abstracta es la clase de la cual heredan otras clases tales como `FunctionNode`, `OperatorNode` y `ConstantNode`. La misma garantiza que sus clases hijas tengan los metodos `Derive` y `Simplify`.

Como parte de las clases que heredan de Node, se encuentra el nodo de `Number`, el cual deriva como 0, pero tambien inclue metodos como Add, Substract, Multiply, Power, Divide y Negate, los cuales retornan su respectiva operacion ya que es con numeros.


#### Clase `Function` ####

Esta clase hereda de la clase abstracta `Node` y tiene como subclases varias funciones matemáticas, las cuales son Sen, Cos, Tan, raíz cuadrada, potencia, logaritmo y  logaritmo neperiano. Cada una de estas subclases se encarga de sobrescribir el método `Derive` y `Simplify`, los cuales se encuentran en `Node`, por tanto, estas clases son las encargadas de derivar y reducir sus respectivas funciones.


#### Clase `Operator` ####

La clase `Operator` es la encargada de ejecutar las operaciones matemáticas necesarias para derivar y reducir la función en cuestión. Dentro de esta clase, se encuentran varias subclases tales como `NegateNode`, que se encarga de cambiar de signo cierta expresión, `SumNode`, el cual deriva la suma de 2 expresiones, `SubstractNode`, encargado de derivar la resta de 2 expresiones cualesquiera, `DivideNode` deriva la división de 2 expresiones mediante el metodo de la division, `MultiplyNode` realiza la derivación de la multiplicación mediante el metodo de la multiplicacion y `PowerNode` deriva una función potencia, siempre haciendo uso de la regla de la cadena. 


#### Clase `Operators` ####

Dicha clase establece una jerarquía, o árbol de prioridades dentro de las operaciones matemáticas que podemos encontrar dentro de una expresión. Esto quiere decir que, la potencia, por ejemplo, se ejecutará en primer lugar sobre una suma o una multiplicación de 2 términos en donde aparezca. Las prioridades se establecen en orden numérico, cuanto mas alto es el número, la operación adquiere una mayor prioridad. Esto es necesario para respetar el orden en que deben ejecutarse las operaciones a la hora de efectuar el cálculo de la derivada de una función. Dentro de esta clase, se encuentran la interfaz `IOperator`, la cual asegura que se defina la prioridad de la cual se habla, y otras subclases tales como `OperatorAdd`, `OperatorSubstract` y `OperatorNegate` (establecen la prioridad de la suma y la resta, así como la transformación de una expresión de positiva a negativa o viceversa, siendo las 3 del mismo nivel), `OperatorDivide` y `OperatorMultiply` (definen la prioridad de la multiplicación y división) y `OperatorPower` (la cual representa la prioridad de la potencia).


#### Clase `Parameter` ####

En esta clase se define la variable en relación a la cual se quiere hallar la derivada de la función en cuestión. Esta clase también hereda de `Node`, puesto que también la variable necesita derivarse y reducirse como expresión.

#### Clase `Parameters` ####

En esta clase se define el nombre en string que pueden tener las variables de las cuales se trate en la función, los cuales son "x", "y" y "z".

#### Clase `Parser` ####

Esta clase es la encargada de traducir la expresión que tenemos en un principio en forma de string y procesarla a través de todas las clases mencionadas anteriormente hasta encontrar finalmente su derivada. Primeramente se crean 3 variables de la cual se auxiliará la clase para poder trabajar con la entrada brindada en un principio. Estas son `Index`, la cual marca la posición mientras se lee la entrada, `Input` que guardará el string que representa la función a derivar y `Params` para contabilizar la cantidad de variables que va a tener dicha función. Se inicializa la variable `Params` como una instancia de la clase `Parameters`. Luego, comenzamos a parsear revisando letra por letra, y verificándolos contra los números del 0 al 9, los signos de los operadores y las letras "x", "y" y "z" que marcarán las variables. Esto se hace en un ciclo `while`, en donde se hace un uso exhaustivo del `switch case`.

#### Clase `FunctionTreeBuilder`

Esta clase es la encargada de ejecutar las operaciones respecto a la jerarquia establecida. Contamos con 3 operaciones y 3 expresiones ya que en nuestra jerarquia solo hay 3 niveles de prioridad. 
Como funciona es que: cada vez que el parser encuentre una expresion o un operador, va a llamar al Metodo SetOperator o SetExpression segun corresponda donde se le va a asignar o el operador/expresion 1 o el 2 o el 3, segun esten vacios. Para el operador 1, se le pone el operador que reciba. Para el operador 2, se pregunta si el operador que se va a poner es de prioridad menor o igual que la prioridad del operador 1, y si es asi, se aplica el operador 1 y este pasa a ser el nuevo operador 1; sino se pone como operador 2. Pasa algo similar con el operador 3.  


	