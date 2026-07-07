namespace Services_90DI.constantes
{
    // Nombres de los módulos del sistema para la bitácora. Única fuente de verdad:
    // se usan al REGISTRAR eventos (BLL) y para poblar el combo de filtro (FrmBitacora)
    public static class Modulos_90DI
    {
        public const string Login      = "Login";
        public const string Usuarios   = "Usuarios";
        public const string Roles      = "Roles";
        public const string Familias   = "Familias";
        public const string Backup     = "Backup";
        public const string Integridad = "Integridad";

        // Lista (en orden) para poblar el combo de filtro de módulo en la Bitácora.
        public static readonly string[] Todos =
            { Login, Usuarios, Roles, Familias, Backup, Integridad };
    }
}
