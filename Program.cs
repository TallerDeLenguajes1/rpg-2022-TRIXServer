var functions = new functions();
string archivoGanadores = "ganadores.csv";
string archivoJugadores = "jugadores.json";

var random = new Random();

rootNames names = null;
names = functions.populateNamesApi(names);

do
{
    System.Console.Clear();
    System.Console.WriteLine("--\tRPG");
    System.Console.WriteLine();
    System.Console.WriteLine(" L - Lista de ganadores");
    System.Console.WriteLine(" P - Pelea");
    System.Console.WriteLine(" F - Finalizar");
    System.Console.WriteLine();
    System.Console.WriteLine("--");
    System.Console.WriteLine();
    System.Console.Write("Elija una opcion: ");
    char flagInicio = char.ToUpper(Console.ReadKey().KeyChar);
    System.Console.WriteLine();

    if (flagInicio == 'L')
    {
        functions.leerArchivo(archivoGanadores);

    }
    else
    {
        if (flagInicio == 'F')
        {
            break;

        }
        else
        {
            Console.Clear();
            System.Console.WriteLine("--\tPELEA");
            System.Console.WriteLine();
            System.Console.WriteLine(" Guardar la informacion de los luchadores?");
            System.Console.WriteLine(" S - SI");
            System.Console.WriteLine(" N - NO");
            System.Console.WriteLine();
            System.Console.WriteLine("--");
            System.Console.WriteLine();
            System.Console.Write("Elija una opcion: ");
            char opcionGuardar = char.ToUpper(Console.ReadKey().KeyChar);
            System.Console.WriteLine();
            functions.eleccionPelea(archivoGanadores, random, opcionGuardar, archivoJugadores, names);
            
        }
    }

}
while (true);

