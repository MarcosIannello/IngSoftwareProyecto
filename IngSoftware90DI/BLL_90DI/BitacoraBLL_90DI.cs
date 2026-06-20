using DAL;
using Service_90DI;
using Services_90DI.entities;
using Services_90DI.constantes;

namespace BLL_90DI
{
    public class BitacoraBLL_90DI
    {
        private readonly BitacoraDAL_90DI _dal = new BitacoraDAL_90DI();
        private IntegridadBLL_90DI? _integridad;
        private IntegridadBLL_90DI Integridad => _integridad ??= new IntegridadBLL_90DI();

        public BitacoraBLL_90DI() { }

        // Registra un evento ya armado y recalcula la integridad de EVENTOS_90DI.
        public bool CreateLogEvent_90DI(Event_90DI logEvent_90DI)
        {
            var result = _dal.CreateLogEvent_90DI(logEvent_90DI);
            if (result)
                Integridad.RecalcularTabla_90DI(TablasDV_90DI.Eventos);
            return result;
        }

        public bool CreateLogEvent_90DI(string evento, string modulo, byte criticidad = 1)
        {
            return CreateLogEvent_90DI(new Event_90DI
            {
                Login_90DI      = SessionManager_90DI.Instancia.userActual.NombreUsuario_90DI,
                Fecha_90DI      = DateTime.Now,
                Hora_90DI       = DateTime.Now.TimeOfDay,
                Modulo_90DI     = modulo,
                Evento_90DI     = evento,
                Criticidad_90DI = criticidad
            });
        }

        public List<Event_90DI> GetLogEvent_90DI()
        {
            return _dal.GetLogEvent_90DI();
        }
    }
}
