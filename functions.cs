using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

public class functions
{
    public static rootNames populateNamesApi(rootNames dataNames)
    {
        string url = $"https://randomuser.me/api/?inc=name&?page=1&results=30&noinfo&?nat=us,%20es,%20nz,%20au,%20fr,%20fi";
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
                            dataNames = JsonSerializer.Deserialize<rootNames>(contenidoJson);

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
        var archivoRead = new StreamReader(File.Open(dataNombreArchivo, FileMode.Open));
        System.Console.WriteLine($"{archivoRead.ReadToEnd()}");
        archivoRead.Close();

    }

    public static void eleccionPelea(string dataArchivoGanadores, Random dataRandom, char dataFlagGuardar, string dataArchivoJugadores, rootNames dataNames)
    {
        var listaPersonajes = new List<personaje>();
        personaje personaje;

        System.Console.WriteLine("--\tEleccion Personaje");
        System.Console.WriteLine("--");

        personaje = setPersonaje(dataArchivoJugadores, dataNames);

        listaPersonajes.Add(personaje);

        var listaJugadores = new List<personaje>();
        if (dataFlagGuardar == 'S')
        {
            listaJugadores.Add(personaje);

        }

        System.Console.WriteLine("La cantidad de peleas es aleatoria entre 1 y 10");
        System.Console.WriteLine("--");

        int cantidadPeleas = dataRandom.Next(1, 11);
        int cantidadEmpates = 0;

        System.Console.WriteLine($"Cantidad de Peleas: {cantidadPeleas}");

        runPelea(dataRandom, listaPersonajes, ref cantidadPeleas, ref cantidadEmpates, dataFlagGuardar, listaJugadores, dataArchivoJugadores, dataNames);

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

        System.Console.Write("Crear o elegir personaje (C - Crear | E - Elegir): ");
        char flagElegir = char.ToUpper(Console.ReadKey().KeyChar);
        System.Console.WriteLine();
        if (flagElegir == 'E')
        {
            string contenidoArchivo = File.ReadAllText(dataArchivoJugadores);
            var listaJugadoresDeserializada = JsonSerializer.Deserialize <List<personaje>> (contenidoArchivo);

            if (listaJugadoresDeserializada.Count > 0)
            {
                System.Console.WriteLine("--\tPersonajes Guardados");
                System.Console.WriteLine("--");
                foreach (var item in listaJugadoresDeserializada)
                {
                    System.Console.WriteLine($" Nombre:\t{item.DataDatos.Nombre}");
                    System.Console.WriteLine($" Alias:\t{item.DataDatos.Alias}");
                    System.Console.WriteLine($" Tipo:\t{item.DataDatos.Tipo}");
                    System.Console.WriteLine($" Fecha de Nacimiento:\t{item.DataDatos.FechaNacimiento}");
                    System.Console.WriteLine($" Edad:\t{item.DataDatos.Edad}");
                    System.Console.WriteLine($" Salud:\t{item.DataDatos.Salud}");
                    System.Console.WriteLine($" Velocidad:\t{item.DataCaracteristicas.Velocidad}");
                    System.Console.WriteLine($" Destreza:\t{item.DataCaracteristicas.Destreza}");
                    System.Console.WriteLine($" Fuerza:\t{item.DataCaracteristicas.Fuerza}");
                    System.Console.WriteLine($" Nivel:\t{item.DataCaracteristicas.Nivel}");
                    System.Console.WriteLine($" Armadura:\t{item.DataCaracteristicas.Armadura}");
                    System.Console.WriteLine("--");

                }

                System.Console.WriteLine($"Elija entre 1 y {listaJugadoresDeserializada.Count}");
                int eleccionPersonaje = Convert.ToInt32(Console.ReadLine());
                personaje = listaJugadoresDeserializada[eleccionPersonaje - 1];

            }
            else
            {
                System.Console.WriteLine("No hay personajes disponibles. Se creara uno nuevo");
                personaje = new personaje(dataNames);

            }
        }
        else
        {
            personaje = new personaje(dataNames);

        }

        return personaje;

    }

    public static void runPelea(Random dataRandom, List<personaje> dataListaPersonajes, ref int dataCantidadPeleas, ref int dataCantidadEmpates, char dataFlagGuardar, List<personaje> dataListaJugadores, string dataArchivoJugadores, rootNames dataNames)
    {
        while (dataCantidadPeleas > 0)
        {
            if (dataCantidadEmpates == 0)
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
                System.Console.WriteLine("--");
                System.Console.WriteLine("EMPATE!!, volveran a pelear");
                dataCantidadPeleas++;

            }

            System.Console.WriteLine("--");
            System.Console.WriteLine("--\tPersonajes a luchar:");
            foreach (var item in dataListaPersonajes)
            {
                System.Console.WriteLine($" Nombre:\t{item.DataDatos.Nombre}");
                System.Console.WriteLine($" Alias:\t{item.DataDatos.Alias}");
                System.Console.WriteLine($" Tipo:\t{item.DataDatos.Tipo}");

            }

            System.Console.WriteLine("Presione una tecla para comenzar!");
            char flagPresione = Console.ReadKey().KeyChar;
            System.Console.WriteLine();
            System.Console.WriteLine("--\tPeleando");
            for (int i = 0; i < 3; i++)
            {
                char flagContinuar;

                processPelea(dataListaPersonajes[1], dataListaPersonajes[0]);
                System.Console.WriteLine("Presione una tecla para continuar");
                flagContinuar = Console.ReadKey().KeyChar;

                if (dataListaPersonajes[1].DataDatos.Salud <= 0)
                {
                    break;
                }

                processPelea(dataListaPersonajes[0], dataListaPersonajes[1]);
                System.Console.WriteLine("Presione una tecla para continuar");
                flagContinuar = Console.ReadKey().KeyChar;

                if (dataListaPersonajes[0].DataDatos.Salud <= 0)
                {
                    break;
                }

                if (i == 2)
                {
                    System.Console.WriteLine("Fin de la pelea!");

                }
                
            }

            if (dataListaPersonajes[0].DataDatos.Salud == dataListaPersonajes[1].DataDatos.Salud)
            {
                dataCantidadEmpates = 1;

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
                    System.Console.WriteLine($"EL GANADOR DE LA PELEA: {item.DataDatos.Nombre} {item.DataDatos.Alias}");
                    item.DataDatos.Salud = 100;
                    int randomBonus = dataRandom.Next(2);
                    float bonus = 0;
                    if (randomBonus == 0)
                    {
                        bonus = 10;
                        item.DataDatos.Salud += Convert.ToInt32(bonus);
                        System.Console.WriteLine($" {item.DataDatos.Alias} obtuvo {bonus} mas de salud");

                    }
                    else
                    {
                        bonus = dataRandom.Next(5, 11);
                        item.DataCaracteristicas.Fuerza += Convert.ToInt32(bonus / item.DataCaracteristicas.Fuerza);
                        System.Console.WriteLine($" {item.DataDatos.Alias} obtuvo {bonus}% de fuerza");

                    }
                }
                dataCantidadEmpates = 0;

            }
            dataCantidadPeleas--;

        }

    }

    public static void winPelea(List<personaje> dataListaPersonajes, string dataArchivoGanadores)
    {

    }

    public static void processPelea(personaje dataAtacante, personaje dataDefensor)
    {
        System.Console.WriteLine($"Atacante:\t{dataAtacante.DataDatos.Nombre} {dataAtacante.DataDatos.Alias}");
        System.Console.WriteLine($"\tDefensor:\t{dataDefensor.DataDatos.Nombre} {dataDefensor.DataDatos.Alias}");
        var random = new Random();

        float poderDisparo = dataAtacante.DataCaracteristicas.Destreza * dataAtacante.DataCaracteristicas.Fuerza * dataAtacante.DataCaracteristicas.Nivel;
        float efectividadDisparo = random.Next(1, 101);
        float valorAtaque = poderDisparo * efectividadDisparo;
        float poderDefensa = dataDefensor.DataCaracteristicas.Armadura * dataDefensor.DataCaracteristicas.Velocidad;
        float maximoDanio = 50000;
        int danioProvocado = Convert.ToInt32((((valorAtaque * efectividadDisparo) - poderDefensa) / maximoDanio) * 5);

        System.Console.WriteLine($"Danio provocado de {dataAtacante.DataDatos.Alias}: {danioProvocado}");
        dataDefensor.DataDatos.Salud -= danioProvocado;
        System.Console.WriteLine($"Salud de {dataDefensor.DataDatos.Alias}: {dataDefensor.DataDatos.Salud}");;

    }

}
