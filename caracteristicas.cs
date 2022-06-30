public class caracteristicas
{
    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;

    public caracteristicas()
    {
        var random  = new Random();

        velocidad = random.Next(1, 11);
        destreza = random.Next(1, 6);
        nivel = random.Next(1, 11);
        fuerza = random.Next(1, 11);
        armadura = random.Next(1, 11);

    }

    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    
}