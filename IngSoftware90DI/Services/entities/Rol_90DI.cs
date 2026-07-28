namespace Services_90DI.entities
{
    public class Rol_90DI
    {
        public int IdRol_90DI { get; set; }
        public string Nombre_90DI { get; set; } = "";
        public string Descripcion_90DI { get; set; } = "";
        public DateTime FechaAlta_90DI { get; set; }

        public List<Familia_90DI> Familias_90DI { get; set; } = [];
        public List<Patente_90DI> Patentes_90DI { get; set; } = [];
    }
}
