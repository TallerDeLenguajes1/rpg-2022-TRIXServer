public class personaje
{
    private datos dataDatos;
    private caracteristicas dataCaracteristicas;

    public personaje()
    {
    }

    public personaje(rootNames dataNames)
    {
        this.dataDatos = new datos(dataNames);
        this.dataCaracteristicas = new caracteristicas();
        
    }

    public datos DataDatos { get => dataDatos; set => dataDatos = value; }
    public caracteristicas DataCaracteristicas { get => dataCaracteristicas; set => dataCaracteristicas = value; }
}