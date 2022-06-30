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
            "Scout",
            "Soldier",
            "Pyro",
            "Daemon",
            "Heavy",
            "Engineer",
            "Medic",
            "Sniper",
            "Spy"
        };

        var aliasPersonaje = new string[]
        {
            "Red Reaper",
            "Gunner",
            "Doomsday",
            "Dark Sword",
            "Insidious",
            "God's Punisher",
            "War Dog",
            "Iron Hand",
            "Blood Star",
            "Atomic Arsenic",
            "Atomic Apple",
            "Purple Sun",
            "Anger",
            "Guts Destructor",
            "Ego Monster"
        };

        int nombreRandom = random.Next(0, dataNames.Results.Count());
        tipo = tipoPersonaje[random.Next(0, tipoPersonaje.Length)];
        nombre = $"{dataNames.Results[nombreRandom]} {dataNames.Results[nombreRandom].Name.Last}";
        alias = aliasPersonaje[random.Next(0, aliasPersonaje.Length)];
        var fecha = new DateOnly(random.Next(1723, 2023), random.Next(1, 13), random.Next(1, 29));
        fechaNacimiento = Convert.ToString(fecha);
        edad = DateTime.Now.Year - fecha.Year;
        if (fecha.Month > DateTime.Now.Month)
        {
            edad--;

        }
        else
        {
            if (fecha.Month == DateTime.Now.Month && fecha.Day > DateTime.Now.Day)
            {
                edad--;

            }

        }
        salud = 100;

    }

}