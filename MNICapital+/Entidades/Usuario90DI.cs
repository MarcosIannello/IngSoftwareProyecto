namespace Entidades
{
    public class Usuario90DI
    {
        public int IdUsuario_90DI { get; set; }

        public string NombreUsuario_90DI { get; set; } = "";

        public string Clave_90DI { get; set; } = "";

        public int IdPerfil_90DI { get; set; }

        public bool Activo_90DI { get; set; } = true;

        public DateTime FechaAlta_90DI { get; set; }

        public string DNI_90DI { get; set; } = "";

        public string? Apellidos_90DI { get; set; }

        public string? Nombre_90DI { get; set; }

        public int Rol_90DI { get; set; }

        public string? Email_90DI { get; set; }

        public bool? Bloquo_90DI { get; set; } = false;
    }
}