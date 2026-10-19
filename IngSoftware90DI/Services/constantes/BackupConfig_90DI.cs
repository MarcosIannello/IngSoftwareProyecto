namespace Services_90DI.constantes
{
    // Configuración del backup/restore.
    public static class BackupConfig_90DI
    {
        // Acá va solo el nombre del .bak, no la ruta. La carpeta se le pregunta al SQL Server
        // en tiempo de ejecución (InstanceDefaultBackupPath), así funciona en cualquier máquina
        // sin hardcodear una ruta que en otra PC no existe.
        //
        // Es un único archivo: cada backup pisa al anterior (WITH INIT) y cuando falla la
        // integridad se restaura siempre desde este mismo.
        public const string NombreArchivoBackup_90DI = "IngSoftware90DI_backup.bak";
    }
}
