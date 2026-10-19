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

        // Nombre por defecto del .bak. La carpeta la decide el SQL Server, no nosotros.
        public static string ArchivoBackupPorDefecto => BackupConfig_90DI.NombreArchivoBackup_90DI;

        // Hace un backup de la base tal como está ahora (lo usa el admin desde el menú).
        // Si sale bien queda registrado en la bitácora.
        public bool GenerarBackup_90DI(string? nombreArchivo = null)
        {
            var archivo = nombreArchivo ?? ArchivoBackupPorDefecto;
            var ok = _dal.Backup_90DI(archivo);

            if (ok)
                Bitacora.CreateLogEvent_90DI("Backup de base de datos generado", Modulos_90DI.Backup, 1);

            return ok;
        }

        // Restaura el último backup, se usa cuando falla la integridad.
        // Ojo: acá no se registra en bitácora, porque eso recalcularía el DV de EVENTOS_90DI
        // sobre la base recién restaurada.
        public bool RestaurarBackup_90DI(string? nombreArchivo = null) 
        {
            var archivo = nombreArchivo ?? ArchivoBackupPorDefecto;
            return _dal.Restore_90DI(archivo);
        }

        // Fecha del último backup (null si nunca se hizo ninguno). La mostramos para que
        // el admin sepa a qué punto vuelve la base si restaura.
        public DateTime? ObtenerFechaUltimoBackup_90DI()
        {
            return _dal.ObtenerFechaUltimoBackup_90DI();
        }
    }
}
