namespace Services_90DI.Entidades
{
    public class User90DI
    {
        public int IdUsuario_90DI { get; set; }
        public string NombreUsuario_90DI { get; set; } = "";
        public string Clave_90DI { get; set; } = "";
        public int IdPerfil_90DI { get; set; }
        public bool Activo_90DI { get; set; }
        public DateTime FechaAlta_90DI { get; set; }
        public string DNI_90DI { get; set; } = "";
        public string Apellidos_90DI { get; set; } = "";
        public string Nombre_90DI { get; set; } = "";
        public string Rol_90DI { get; set; } = "";
        public string Email_90DI { get; set; } = "";
        public bool Bloqueo_90DI { get; set; }
    }
}
