using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

public class functions
{
    const string cellLeftTop = "┌";
    const string cellRightTop = "┐";
    const string cellLeftBottom = "└";
    const string cellRightBottom = "┘";
    const string cellHorizontalJointTop = "┬";
    const string cellHorizontalJointbottom = "┴";
    const string cellVerticalJointLeft = "├";
    const string cellTJoint = "┼";
    const string cellVerticalJointRight = "┤";
    const string cellHorizontalLine = "─";
    const string cellVerticalLine = "│";
    
    public static rootNames populateNamesApi(rootNames dataNames)
    {
        string url = $"https://randomuser.me/api/?inc=name&?page=1&results=30&noinfo&?nat=es";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string contenidoJson = objReader.ReadToEnd();
                            dataNames = JsonSerializer.Deserialize <rootNames> (contenidoJson);

                        } 

                    }

                }

            }

        }
        catch (System.Exception)
        {
            throw;

        }

        return dataNames;

    }

    public static void leerArchivo(string dataNombreArchivo)
    {
        var archivoRead = new StreamReader(File.Open(dataNombreArchivo, FileMode.OpenOrCreate));
        Console.Clear();
        System.Console.WriteLine("┌──\tLISTADO DE GANADORES");
        System.Console.WriteLine("│");
        System.Console.Write($"{archivoRead.ReadToEnd()}");
        System.Console.WriteLine("│");
        System.Console.WriteLine("├──");
        System.Console.WriteLine("│");
        System.Console.Write("└── Presione una tecla para continuar...");
        char flagTecla = char.ToUpper(Console.ReadKey().KeyChar);
        archivoRead.Close();

    }

    public static void enfrentamiento(string dataArchivoGanadores, Random dataRandom, char dataFlagGuardar, string dataArchivoJugadores, rootNames dataNames)
    {
        var listaPersonajes = new List<personaje>();
        personaje personaje;

        personaje = setPersonaje(dataArchivoJugadores, dataNames);

        listaPersonajes.Add(personaje);

        var listaJugadores = new List<personaje>();
        if (dataFlagGuardar == 'S')
        {
            listaJugadores.Add(personaje);

        }

        int cantidadEnfrentamientos = dataRandom.Next(1, 6);
        int flagEmpate = 0;

        Console.Clear();
        System.Console.WriteLine("┌──\tINICIO");
        System.Console.WriteLine("│");
        System.Console.WriteLine("│ La cantidad de enfrentamientos es aleatorio entre 1 y 5");
        System.Console.WriteLine("│");
        System.Console.WriteLine($"│ Cantidad de enfrentamientos: {cantidadEnfrentamientos}");
        System.Console.WriteLine("│");
        System.Console.WriteLine("├──");
        System.Console.WriteLine("│");
        System.Console.Write("└── Presione una tecla para continuar...");
        char flagTecla = char.ToUpper(Console.ReadKey().KeyChar);

        runPelea(dataRandom, listaPersonajes, ref cantidadEnfrentamientos, ref flagEmpate, dataFlagGuardar, listaJugadores, dataArchivoJugadores, dataNames);

        winPelea(listaPersonajes, dataArchivoGanadores);

        if ((dataFlagGuardar == 'S') && listaJugadores != null)
        {
            foreach (var item in listaJugadores)
            {
                item.DataDatos.Salud = 100;
            
            }
            string listaJugadoresSerializada = JsonSerializer.Serialize(listaJugadores);
            var archivoWrite = new StreamWriter(File.Open(dataArchivoJugadores, FileMode.Create));
            archivoWrite.WriteLine(listaJugadoresSerializada);
            archivoWrite.Close();

        }

    }

    public static personaje setPersonaje(string dataArchivoJugadores, rootNames dataNames)
    {
        personaje personaje;

        Console.Clear();
        System.Console.WriteLine("┌──\tELECCION DE PERSONAJE");
        System.Console.WriteLine("│");
        System.Console.WriteLine("│ Crear o elegir personaje");
        System.Console.WriteLine("│");
        System.Console.WriteLine("│ C - Crear");
        System.Console.WriteLine("│ E - Elegir");
        System.Console.WriteLine("│");
        System.Console.WriteLine("├──");
        System.Console.WriteLine("│");
        System.Console.Write("└── Elije una opcion: ");
        char flagElegir = char.ToUpper(Console.ReadKey().KeyChar);
        if (flagElegir == 'E')
        {
            string contenidoArchivo = File.ReadAllText(dataArchivoJugadores);
            var listaJugadoresDeserializada = JsonSerializer.Deserialize <List<personaje>> (contenidoArchivo);

            Console.Clear();

            if (listaJugadoresDeserializada.Count > 0)
            {
                System.Console.WriteLine("┌──\tPERSONAJES GUARDADOS");
                var contadorItem = 0;
                foreach (var item in listaJugadoresDeserializada)
                {
                    
                    System.Console.WriteLine($"│");
                    System.Console.WriteLine($"├──\tJUGADOR # {contadorItem + 1}");
                    System.Console.WriteLine($"│");
                    System.Console.WriteLine($"│ Nombre:\t{item.DataDatos.Nombre, -30} Alias:\t\t{item.DataDatos.Alias, -30}");
                    System.Console.WriteLine($"│ Tipo:\t\t{item.DataDatos.Tipo, -30} Salud:\t\t{item.DataDatos.Salud, -30}");
                    System.Console.WriteLine($"│ Fech. Nac.:\t{item.DataDatos.FechaNacimiento, -30} Edad:\t\t{item.DataDatos.Edad, -30}");
                    System.Console.WriteLine($"│ Velocidad:\t{item.DataCaracteristicas.Velocidad, -30} Destreza:\t{item.DataCaracteristicas.Destreza, -30}");
                    System.Console.WriteLine($"│ Fuerza:\t{item.DataCaracteristicas.Fuerza, -10} Nivel:\t{item.DataCaracteristicas.Nivel, -6} Armadura:\t{item.DataCaracteristicas.Armadura, -20}");
                    System.Console.WriteLine("│");
                    System.Console.WriteLine("├──");

                    contadorItem++;

                }

                int flagPersonaje = 1;
                System.Console.WriteLine($"│");
                System.Console.Write($"└── Elije entre 1 y {listaJugadoresDeserializada.Count}: ");
                flagPersonaje = Convert.ToInt32(Console.ReadLine());
                personaje = listaJugadoresDeserializada[flagPersonaje - 1];

            }
            else
            {
                System.Console.WriteLine("┌──\tSIN PERSONAJES");
                System.Console.WriteLine("│");
                System.Console.WriteLine("│ No hay personajes disponibles. Se creara uno nuevo");
                System.Console.WriteLine("│");
                System.Console.WriteLine("├──");
                System.Console.WriteLine("│");
                System.Console.Write("└── Presione una tecla para continuar...");
                personaje = new personaje(dataNames);
                char flagTecla = char.ToUpper(Console.ReadKey().KeyChar);

            }
        }
        else
        {
            personaje = new personaje(dataNames);

        }

        return personaje;

    }

    public static void runPelea(Random dataRandom, List<personaje> dataListaPersonajes, ref int dataCantidadEnfrentamientos, ref int dataFlagEmpate, char dataFlagGuardar, List<personaje> dataListaJugadores, string dataArchivoJugadores, rootNames dataNames)
    {
        while (dataCantidadEnfrentamientos > 0)
        {
            if (dataFlagEmpate == 0)
            {
                personaje personaje;
                personaje = setPersonaje(dataArchivoJugadores, dataNames);
                dataListaPersonajes.Add(personaje);

                if (dataFlagGuardar == 'S')
                {
                    dataListaJugadores.Add(personaje);

                }

            }
            else
            {
                System.Console.WriteLine("├──");
                System.Console.WriteLine("│");
                System.Console.WriteLine("│ EMPATE!!, volveran a pelear");
                char flagContinua3 = Console.ReadKey().KeyChar;
                dataCantidadEnfrentamientos++;

            }

            Console.Clear();
            System.Console.WriteLine("┌──\tPERSONAJES ENFRENTANDOS");

            foreach (var item in dataListaPersonajes)
            {
                System.Console.WriteLine($"│");
                System.Console.WriteLine($"│ Nombre:\t{item.DataDatos.Nombre}");
                System.Console.WriteLine($"│ Alias:\t{item.DataDatos.Alias}");
                System.Console.WriteLine($"│ Tipo:\t\t{item.DataDatos.Tipo}");
                System.Console.WriteLine("│");
                System.Console.WriteLine("├──");

            }

            System.Console.WriteLine("│");
            System.Console.Write("└── Presione una tecla para comenzar!");
            char flagPresione = Console.ReadKey().KeyChar;

            for (int i = 0; i < 3; i++)
            {

                Console.Clear();
                System.Console.WriteLine($"┌──\tROUND # {i + 1}");

                processPelea(dataListaPersonajes[1], dataListaPersonajes[0]);
                
                if (dataListaPersonajes[0].DataDatos.Salud <= 0)
                {
                    System.Console.WriteLine("│");
                    System.Console.Write("└── Presione una tecla para continuar...");
                    char flagContinua1 = Console.ReadKey().KeyChar;
                    break;

                }

                processPelea(dataListaPersonajes[0], dataListaPersonajes[1]);

                if (dataListaPersonajes[1].DataDatos.Salud <= 0)
                {
                    System.Console.WriteLine("│");
                    System.Console.Write("└── Presione una tecla para continuar...");
                    char flagContinua2 = Console.ReadKey().KeyChar;
                    break;

                }

                if (i == 2)
                {
                    System.Console.WriteLine("│");
                    System.Console.WriteLine("│ FIN DEL ENFRENTAMIENTO!!!");

                }

                System.Console.WriteLine($"│");
                System.Console.WriteLine($"│ Enfrentamientos restantes: {dataCantidadEnfrentamientos - 1}");
                System.Console.WriteLine("│");
                System.Console.Write("└── Presione una tecla para continuar...");
                char flagContinua = Console.ReadKey().KeyChar;
                Console.Clear();

            }

            if (dataListaPersonajes[0].DataDatos.Salud == dataListaPersonajes[1].DataDatos.Salud)
            {
                dataFlagEmpate = 1;

            }
            else
            {
                if (dataListaPersonajes[0].DataDatos.Salud < dataListaPersonajes[1].DataDatos.Salud)
                {
                    dataListaPersonajes.RemoveAt(0);

                }
                else
                {
                    dataListaPersonajes.RemoveAt(1);

                }

                foreach (var item in dataListaPersonajes)
                {
                    Console.Clear();
                    System.Console.WriteLine("┌──\tGANADOR DEL ENFRENTAMIENTO");
                    System.Console.WriteLine($"│");
                    System.Console.WriteLine($"│ Nombre:\t{item.DataDatos.Nombre, -30} Alias:\t\t{item.DataDatos.Alias, -30}");
                    item.DataDatos.Salud = 100;
                    int randomBonus = dataRandom.Next(2);
                    float bonus = 0;
                    if (randomBonus == 0)
                    {
                        bonus = 10;
                        item.DataDatos.Salud += Convert.ToInt32(bonus);
                        System.Console.WriteLine($"│");
                        System.Console.WriteLine($"│ {item.DataDatos.Alias} obtuvo {bonus} mas de salud");
                        System.Console.WriteLine("│");

                    }
                    else
                    {
                        bonus = dataRandom.Next(5, 11);
                        item.DataCaracteristicas.Fuerza += Convert.ToInt32(bonus / item.DataCaracteristicas.Fuerza);
                        System.Console.WriteLine($"│");
                        System.Console.WriteLine($"│ {item.DataDatos.Alias} obtuvo {bonus}% de fuerza");
                        System.Console.WriteLine("│");

                    }
                }
                dataFlagEmpate = 0;
                System.Console.WriteLine("├──");
                System.Console.WriteLine("│");
                System.Console.Write($"└── Presione una tecla para continuar...");
                char flagContinua = Console.ReadKey().KeyChar;

            }
            dataCantidadEnfrentamientos--;

        }

    }

    public static void winPelea(List<personaje> dataListaPersonajes, string dataArchivoGanadores)
    {
        Console.Clear();
        System.Console.WriteLine("┌──\tGANADOR DE LOS ENFRENTAMIENTOS");
        System.Console.WriteLine($"│");
        System.Console.WriteLine($"│ Nombre:\t{dataListaPersonajes[0].DataDatos.Nombre, -30} Alias:\t\t{dataListaPersonajes[0].DataDatos.Alias, -30}");
        System.Console.WriteLine($"│ Tipo:\t\t{dataListaPersonajes[0].DataDatos.Tipo, -30} Salud:\t\t{dataListaPersonajes[0].DataDatos.Salud, -30}");
        System.Console.WriteLine($"│ Fech. Nac.:\t{dataListaPersonajes[0].DataDatos.FechaNacimiento, -30} Edad:\t\t{dataListaPersonajes[0].DataDatos.Edad, -30}");
        System.Console.WriteLine($"│ Velocidad:\t{dataListaPersonajes[0].DataCaracteristicas.Velocidad, -30} Destreza:\t{dataListaPersonajes[0].DataCaracteristicas.Destreza, -30}");
        System.Console.WriteLine($"│ Fuerza:\t{dataListaPersonajes[0].DataCaracteristicas.Fuerza, -10} Nivel:\t{dataListaPersonajes[0].DataCaracteristicas.Nivel, -6} Armadura:\t{dataListaPersonajes[0].DataCaracteristicas.Armadura, -20}");
        System.Console.WriteLine("│");
        System.Console.WriteLine("├──");
        System.Console.WriteLine("│");
        System.Console.Write("└── Presione una tecla para continuar...");

        var archivoWrite = new StreamWriter(File.Open(dataArchivoGanadores, FileMode.Append));
        string dataWrite = $"│ Nombre: {dataListaPersonajes[0].DataDatos.Nombre}, Alias: {dataListaPersonajes[0].DataDatos.Alias}, Tipo: {dataListaPersonajes[0].DataDatos.Tipo}, Fecha de nacimiento: {dataListaPersonajes[0].DataDatos.FechaNacimiento}, Fecha Enfrentamiento: {DateTime.Now}";
        archivoWrite.WriteLine(dataWrite);
        archivoWrite.Close();
        char continuar = Console.ReadKey().KeyChar;
    }

    public static void processPelea(personaje dataAtacante, personaje dataDefensor)
    {
        var random = new Random();

        float poderDisparo = dataAtacante.DataCaracteristicas.Destreza * dataAtacante.DataCaracteristicas.Fuerza * dataAtacante.DataCaracteristicas.Nivel;
        float efectividadDisparo = random.Next(1, 101);
        float valorAtaque = poderDisparo * efectividadDisparo;
        float poderDefensa = dataDefensor.DataCaracteristicas.Armadura * dataDefensor.DataCaracteristicas.Velocidad;
        float maximoDanio = 50000;
        int danioProvocado = Convert.ToInt32((((valorAtaque * efectividadDisparo) - poderDefensa) / maximoDanio) * 5);
        
        dataDefensor.DataDatos.Salud -= danioProvocado;
        if (dataDefensor.DataDatos.Salud <= 0)
        {
            dataDefensor.DataDatos.Salud = 0;
        }

        System.Console.WriteLine($"│");
        System.Console.WriteLine($"│    ┌──────────┐\t\t\t\t\t ┌────────────────────────┐");
        System.Console.WriteLine($"│ <<<│ ATACANTE │>>> \t{dataAtacante.DataDatos.Nombre, -30} {"-=│ Daño provocado: ", -20} {danioProvocado, 5} │=-");
        System.Console.WriteLine($"│    └──────────┘\t {dataAtacante.DataDatos.Alias, -20} \t\t └────────────────────────┘");
        System.Console.WriteLine("│");
        System.Console.WriteLine("│ \t\t\t   │ │ │");
        System.Console.WriteLine("│ \t\t\t   V V V");
        System.Console.WriteLine($"│    ┌──────────┐\t\t\t\t\t ┌────────────────────────┐");
        System.Console.WriteLine($"│ >>>│ DEFENSOR │<<< \t{dataDefensor.DataDatos.Nombre, -30} {"-=│ Salud: ", -20} {dataDefensor.DataDatos.Salud, 5} │=-");
        System.Console.WriteLine($"│    └──────────┘\t {dataDefensor.DataDatos.Alias, -20} \t\t └────────────────────────┘");
        System.Console.WriteLine("│");
        System.Console.WriteLine($"├──");

    }

}
