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
        var archivoLeer = new StreamReader(File.Open(dataNombreArchivo, FileMode.Open));
        System.Console.WriteLine($"{archivoLeer.ReadToEnd()}");
        archivoLeer.Close();

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

        System.Console.WriteLine("La cantidad de peleas se designara aleatoriamente entre 1 y 10");

        int cantidadPeleas = dataRandom.Next(1, 11);
        ref cantidadPeleas;
        

    }

    public static personaje setPersonaje(string dataArchivoJugadores, rootNames dataNames)
    {

    }

}
