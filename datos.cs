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

    public datos(rootNames datanames)
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

        
    }
}