namespace Services_90DI.constantes
{
    public static class TablasDV_90DI
    {
        // ─── Tablas simples ───────────────────────────────────────
        public const string Patente = "Patente_90DI";
        public const string User    = "User_90DI";
        public const string Rol     = "Rol_90DI";
        public const string Familia = "Familia_90DI";
        public const string Eventos = "EVENTOS_90DI";

        // ─── Tablas intermedias ────────────────────────
        public const string RolPatente     = "Rol_Patente_90DI";
        public const string RolFamilia     = "Rol_Familia_90DI";
        public const string FamiliaPatente = "Familia_Patente_90DI";
        public const string FamiliaFamilia = "Familia_Familia_90DI";

        

        // Alta/baja/modificación de un Rol toca el rol y sus relaciones.
        public static readonly string[] Roles =
            { Rol, RolPatente, RolFamilia };

        // Alta/baja/modificación de una Familia toca la familia y sus relaciones.
        public static readonly string[] Familias =
            { Familia, FamiliaPatente, FamiliaFamilia };

        // Borrar una Familia además impacta en Rol_Familia
        public static readonly string[] FamiliasConRol =
            { Familia, FamiliaPatente, FamiliaFamilia, RolFamilia };
    }
}
