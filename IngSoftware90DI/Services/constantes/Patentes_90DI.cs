namespace Services_90DI.constantes
{
    // Nombres de las patentes del sistema. Cada valor debe coincidir EXACTAMENTE
    // con el Nombre_90DI de la fila correspondiente en la tabla de patentes (BD).
    // Centralizar acá evita repetir las magic strings en los forms.
    public static class Patentes_90DI
    {
        // ─── Usuarios ───
        public const string CambiarPassword = "CAMBIAR_PASSWORD";

        // ─── Admin ───
        public const string AbmUsuarios  = "ABM_USUARIOS";
        public const string Bitacora     = "BITACORA";
        public const string AdminFamilia = "ADMIN_FAMILIA";
        public const string AdminRoles   = "ADMIN_ROLES";

        // ─── Maestro ───
        public const string Clientes  = "CLIENTES";
        public const string Prestamos = "PRESTAMOS";
        public const string Medicos   = "MEDICOS";
        public const string Pacientes = "PACIENTES";

        // ─── Reportes ───
        public const string HistorialCliente   = "HISTORIAL_CLIENTE";
        public const string SimulacionPrestamo = "SIMULACION_PRESTAMO";
    }
}
