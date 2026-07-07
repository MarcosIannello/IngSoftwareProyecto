using DAL;
using Services_90DI.constantes;

namespace BLL_90DI
{
    public class BackupBLL_90DI
    {
        private readonly BackupDAL_90DI _dal = new BackupDAL_90DI();
        private BitacoraBLL_90DI? _bitacora;
        private BitacoraBLL_90DI Bitacora => _bitacora ??= new BitacoraBLL_90DI();

        public BackupBLL_90DI() { }

        // Nombre de archivo por defecto del backup (la carpeta la resuelve el servidor SQL).
        public static string ArchivoBackupPorDefecto => BackupConfig_90DI.NombreArchivoBackup;

        // Genera un backup del estado actual de la base. Lo dispara el Admin desde el menú.
        // Registra el evento en bitácora si el backup fue exitoso.
        public bool GenerarBackup_90DI(string? nombreArchivo = null)
        {
            var archivo = nombreArchivo ?? ArchivoBackupPorDefecto;
            var ok = _dal.Backup_90DI(archivo);

            if (ok)
                Bitacora.CreateLogEvent_90DI("Backup de base de datos generado", Modulos_90DI.Backup, 1);

            return ok;
        }

        // Restaura la base desde el último backup. Se usa ante una falla de integridad.
        // No se registra en bitácora acá: la base queda en el estado del backup y un log
        // posterior recalcularía el DV de EVENTOS_90DI sobre datos ya restaurados.
        public bool RestaurarBackup_90DI(string? nombreArchivo = null)
        {
            var archivo = nombreArchivo ?? ArchivoBackupPorDefecto;
            return _dal.Restore_90DI(archivo);
        }

        // Fecha/hora de creación del último backup disponible, o null si nunca se generó uno.
        // Sirve para mostrarle al Admin a qué estado volvería la base al restaurar.
        public DateTime? ObtenerFechaUltimoBackup_90DI()
        {
            return _dal.ObtenerFechaUltimoBackup_90DI();
        }
    }
}
