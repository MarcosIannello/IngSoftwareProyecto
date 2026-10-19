using DAL;
using Services_90DI.entities;
using Services_90DI.constantes;

namespace BLL_90DI
{
    public class IntegridadBLL_90DI
    {
        private readonly IntegridadDAL_90DI _dal = new IntegridadDAL_90DI();
        private BitacoraBLL_90DI? _bitacora;
        private BitacoraBLL_90DI Bitacora => _bitacora ??= new BitacoraBLL_90DI();

        public IntegridadBLL_90DI() { }

        // Recalcula el DV de una sola tabla. Se llama después de cada alta, baja o modificación.
        public bool RecalcularTabla_90DI(string nombreTabla) =>
            _dal.RecalcularTabla_90DI(nombreTabla);

        // Igual que el anterior pero para varias tablas juntas.
        public bool RecalcularTablas_90DI(params string[] tablas)
        {
            foreach (var tabla in tablas)
            {
                if (!_dal.RecalcularTabla_90DI(tabla))
                    return false;
            }
            return true;
        }

        // Recalcula todas las tablas cargadas en RegistroDV_90DI. Solo lo usa el admin,
        // y si sale bien queda registrado en la bitácora.
        public bool RecalcularTodo_90DI()
        {
            var response = _dal.RecalcularTodo_90DI();

            if (response)
                Bitacora.CreateLogEvent_90DI("Recálculo total de integridad completado", Modulos_90DI.Integridad, 1);

            return response;
        }

        // Verifica todo sin guardar nada y devuelve el resultado de cada tabla.
        public List<ResultadoIntegridad_90DI> Verificar_90DI() =>
            _dal.Verificar_90DI();

        // true si no encontró ninguna tabla corrupta.
        public bool SistemaIntegro_90DI() =>
            !_dal.Verificar_90DI().Any(r => r.EsCorrupto);
    }
}
