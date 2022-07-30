public class datos
{
    private string tipo;
    private string nombre;
    private string alias;
    private string fechaNacimiento;
    private int edad;
    private int salud;

    public datos()
    {
    }

    public string Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Alias { get => alias; set => alias = value; }
    public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Salud { get => salud; set => salud = value; }

    public datos(rootNames dataNames)
    {
        var random = new Random();

        var tipoPersonaje = new string[]
        {
            "Escort",
            "Soldado",
            "Pirado",
            "Demonio",
            "Pesado",
            "Ingeniero",
            "Medico",
            "Francotirador",
            "Espia"
        };

        var aliasPersonaje = new string[]
        {
            "Pala Roja",
            "Chicle",
            "Peliador",
            "Pantalla",
            "Negruzco",
            "Diosito",
            "Perro",
            "Manitas",
            "Estrellita",
            "Bicho de Luz",
            "Manzana Mordida",
            "El Sodero",
            "Chofer",
            "Paracaido",
            "Evo Zurdito"
        };

        tipo = tipoPersonaje[random.Next(0, tipoPersonaje.Length)];
        int rootNamesRandom = random.Next(0, dataNames.Results.Count());
        nombre = $"{dataNames.Results[rootNamesRandom].Name.First} {dataNames.Results[rootNamesRandom].Name.Last}";
        alias = aliasPersonaje[random.Next(0, aliasPersonaje.Length)];
        var fechaRandom = new DateOnly(random.Next(1723, 2023), random.Next(1, 13), random.Next(1, 29));
        fechaNacimiento = Convert.ToString(fechaRandom);
        edad = DateTime.Now.Year - fechaRandom.Year;
        if (fechaRandom.Month > DateTime.Now.Month)
        {
            edad--;

        }
        else
        {
            if (fechaRandom.Month == DateTime.Now.Month && fechaRandom.Day > DateTime.Now.Day)
            {
                edad--;

            }

        }
        salud = 100;

    }

}