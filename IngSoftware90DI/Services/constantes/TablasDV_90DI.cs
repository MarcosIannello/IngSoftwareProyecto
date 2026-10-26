namespace Services_90DI.constantes
{
    public static class TablasDV_90DI
    {
        // Tablas principales
        public const string Patente = "Patente_90DI";
        public const string User    = "User_90DI";
        public const string Rol     = "Rol_90DI";
        public const string Familia = "Familia_90DI";
        public const string Eventos = "EVENTOS_90DI";

        // Tablas intermedias
        public const string RolPatente     = "Rol_Patente_90DI";
        public const string RolFamilia     = "Rol_Familia_90DI";
        public const string FamiliaPatente = "Familia_Patente_90DI";
        public const string FamiliaFamilia = "Familia_Familia_90DI";

        // Columnas por las que se acota el recálculo después de un ABM
        public const string ColIdUsuario      = "IdUsuario_90DI";
        public const string ColNombreUsuario  = "NombreUsuario_90DI";
        public const string ColIdEvento       = "Id_Evento";
        public const string ColIdRol          = "IdRol_90DI";
        public const string ColIdFamilia      = "IdFamilia_90DI";
        public const string ColIdFamiliaPadre = "IdFamiliaPadre_90DI";
        public const string ColIdFamiliaHija  = "IdFamiliaHija_90DI";

        // Las tablas que el código da por protegidas. Si alguna falta en el resultado de la
        // verificación (por ejemplo, la sacaron de RegistroDV_90DI) se trata como ERROR.
        public static readonly string[] Todas =
            { Patente, User, Rol, Familia, Eventos, RolPatente, RolFamilia, FamiliaPatente, FamiliaFamilia };
    }
}
