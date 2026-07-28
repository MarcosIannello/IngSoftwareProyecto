namespace Services_90DI.constantes
{
    // Configuración del servicio de backup/restore.
    public static class BackupConfig_90DI
    {
        // Solo el NOMBRE del archivo .bak (no la ruta completa). La carpeta se resuelve
        // en tiempo de ejecución preguntándole al propio SQL Server su directorio de
        // backups por defecto (InstanceDefaultBackupPath), así funciona en CUALQUIER
        // máquina/servidor sin hardcodear una ruta que rompa en otro dispositivo.
        //
        // Archivo único: cada backup pisa al anterior (WITH INIT) y la restauración por
        // falla de integridad siempre usa este mismo archivo.
        public const string NombreArchivoBackup_90DI = "IngSoftware90DI_backup.bak";
    }
}
