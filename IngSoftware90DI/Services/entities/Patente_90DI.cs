namespace Services_90DI.entities
{
    public class Patente_90DI
    {
        public int IdPatente_90DI { get; set; }
        public string Nombre_90DI { get; set; } = "";
        public string Descripcion_90DI { get; set; } = "";
        public DateTime FechaAlta_90DI { get; set; }

        public override string ToString() => Nombre_90DI;
    }
}
