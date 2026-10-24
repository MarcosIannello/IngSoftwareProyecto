using System.Globalization;
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

        // Recalcula el DV solo de las filas donde [columna] = valor. Se llama después de cada
        // alta, baja o modificación con lo que tocó ese ABM, nunca con la tabla entera:
        // recalcular todo daría por buena cualquier modificación manual que hubiera.
        public bool RecalcularFilas_90DI(string tabla, string columna, object valor) =>
            _dal.RecalcularFilas_90DI(tabla, columna, Convert.ToString(valor, CultureInfo.InvariantCulture) ?? "");

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
        // Las tablas que el código da por protegidas tienen que aparecer sí o sí: si falta alguna
        // (la verificación falló o la sacaron de RegistroDV_90DI) se agrega como ERROR.
        public List<ResultadoIntegridad_90DI> Verificar_90DI()
        {
            var resultados = _dal.Verificar_90DI();

            foreach (var tabla in TablasDV_90DI.Todas)
            {
                if (!resultados.Any(r => string.Equals(r.NombreTabla_90DI, tabla, StringComparison.OrdinalIgnoreCase)))
                    resultados.Add(new ResultadoIntegridad_90DI { NombreTabla_90DI = tabla, Estado_90DI = "ERROR" });
            }

            return resultados;
        }

        // true solo si todas las tablas dieron INTEGRO (un ERROR también cuenta como problema).
        public bool SistemaIntegro_90DI() =>
            Verificar_90DI().All(r => r.EsIntegro);
    }
}
