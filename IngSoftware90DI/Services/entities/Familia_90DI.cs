namespace Services_90DI.entities
{
    public class Familia_90DI
    {
        public int IdFamilia_90DI { get; set; }
        public string Nombre_90DI { get; set; } = "";
        public string Descripcion_90DI { get; set; } = "";
        public DateTime FechaAlta_90DI { get; set; }

        public List<Patente_90DI> Patentes_90DI { get; set; } = [];
        public List<Familia_90DI> SubFamilias_90DI { get; set; } = [];

        public override string ToString() => Nombre_90DI;
    }
}
