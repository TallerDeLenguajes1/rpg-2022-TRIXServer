var functions = new functions();
string archivoGanadores = "ganadores.csv";
string archivoJugadores = "jugadores.json";

var random = new Random();

rootNames names = null;
names = functions.populateNamesApi(names);

do
{
    System.Console.Clear();
    System.Console.WriteLine("┌──\tRPG");
    System.Console.WriteLine("│");
    System.Console.WriteLine("│ L - Listado de ganadores");
    System.Console.WriteLine("│ E - Enfrentamientos");
    System.Console.WriteLine("│");
    System.Console.WriteLine("│ S - Salir");
    System.Console.WriteLine("│");
    System.Console.WriteLine("├──");
    System.Console.WriteLine("│");
    System.Console.Write("└── Elije una opcion: ");
    char flagInicio = char.ToUpper(Console.ReadKey().KeyChar);
    System.Console.WriteLine();

    if (flagInicio == 'L')
    {
        functions.leerArchivo(archivoGanadores);

    }
    else
    {
        if (flagInicio == 'S')
        {
            Console.Clear();
            break;

        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("┌──\tENFRENTAMIENTOS");
            System.Console.WriteLine("│");
            System.Console.WriteLine("│ Guardar informacion de los luchadores?");
            System.Console.WriteLine("│");
            System.Console.WriteLine("│ S - Si");
            System.Console.WriteLine("│ N - No");
            System.Console.WriteLine("│");
            System.Console.WriteLine("├──");
            System.Console.WriteLine("│");
            System.Console.Write("└── Elije una opcion: ");
            char flagGuardar = char.ToUpper(Console.ReadKey().KeyChar);
            System.Console.WriteLine();
            functions.enfrentamiento(archivoGanadores, random, flagGuardar, archivoJugadores, names);
            
        }
    }

}
while (true);

