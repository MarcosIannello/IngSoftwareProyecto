using DAL;
using Services_90DI.entities;

namespace BLL_90DI
{
    public class IntegridadBLL_90DI
    {
        private readonly IntegridadDAL_90DI _dal = new IntegridadDAL_90DI();
        private BitacoraBLL_90DI? _bitacora;
        private BitacoraBLL_90DI Bitacora => _bitacora ??= new BitacoraBLL_90DI();

        public IntegridadBLL_90DI() { }

        //Metodo para recalcular tabla especifica, se llama desde cada operación que modifica datos (Create, Update, Delete).
        public bool RecalcularTabla_90DI(string nombreTabla) =>
            _dal.RecalcularTabla_90DI(nombreTabla);

        // Recalcula la integridad de múltiples tablas a la vez.
        public bool RecalcularTablas_90DI(params string[] tablas)
        {
            foreach (var tabla in tablas)
            {
                if (!_dal.RecalcularTabla_90DI(tabla))
                    return false;
            }
            return true;
        }

        // Recalcula todas las tablas con espejo DV_ de la BD. Solo Admin forzar.
        // Registra el evento en bitácora cuando el recálculo fue exitoso.
        public bool RecalcularTodo_90DI()
        {
            var response = _dal.RecalcularTodo_90DI();

            if (response)
                Bitacora.CreateLogEvent_90DI("Recálculo total de integridad completado", "Integridad", 1);

            return response;
        }

        // Verifica la integridad de todo el sistema sin persistir. Devuelve estado
        public List<ResultadoIntegridad_90DI> Verificar_90DI() =>
            _dal.Verificar_90DI();

        // Retorna true si no hay ninguna corrupción detectada.
        public bool SistemaIntegro_90DI() =>
            !_dal.Verificar_90DI().Any(r => r.EsCorrupto);
    }
}
