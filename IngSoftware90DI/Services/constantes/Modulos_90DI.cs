namespace Services_90DI.constantes
{
    // Módulos del sistema para la bitácora. Se usan tanto al registrar eventos (BLL)
    // como para llenar el combo de filtro en FrmBitacora, así no hay dos listas distintas.
    public static class Modulos_90DI
    {
        public const string Login      = "Login";
        public const string Usuarios   = "Usuarios";
        public const string Roles      = "Roles";
        public const string Familias   = "Familias";
        public const string Backup     = "Backup";
        public const string Integridad = "Integridad";

        // En orden, para el combo de módulos de la bitácora.
        public static readonly string[] Todos =
            { Login, Usuarios, Roles, Familias, Backup, Integridad };
    }
}
