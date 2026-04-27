namespace BE
{
    public class Usuario200MI
    {
        public int IdUsuario_200MI { get; set; }


        public string NombreUsuario_200MI { get; set; } = "";

        //Descomento cuando tenga Hasheo
        //public string Clave_200MI { get; set; } = "";

        public int IdPerfil_200MI { get; set; } 
        public bool Activo_200MI { get; set; }
        public DateTime FechaAlta_200MI { get; set; } 
        
    }
}
