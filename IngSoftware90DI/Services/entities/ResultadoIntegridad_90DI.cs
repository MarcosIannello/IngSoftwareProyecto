namespace Services_90DI.entities
{
    // Resultado de verificar la integridad de una tabla.
    // Mapea la salida de sp_VerificarTodo_90DI:
    //   NombreTabla | Estado | FilaAfectada | ColumnaAfectada
    public class ResultadoIntegridad_90DI
    {
        public string  NombreTabla_90DI     { get; set; } = "";
        public string  Estado_90DI          { get; set; } = ""; // "INTEGRO" | "CORRUPTO" | "ERROR"
        public string? FilaAfectada_90DI    { get; set; }       // PK de la fila rota (null si no aplica)
        public string? ColumnaAfectada_90DI { get; set; }       // columna rota (null si no aplica)

        public bool EsCorrupto => Estado_90DI == "CORRUPTO";
        public bool EsIntegro  => Estado_90DI == "INTEGRO";
        public bool EsError    => Estado_90DI == "ERROR";

        public override string ToString() =>
            EsIntegro
                ? $"[INTEGRO] {NombreTabla_90DI}"
                : $"[{Estado_90DI}] Tabla: {NombreTabla_90DI} | Fila: {FilaAfectada_90DI ?? "—"} | Columna: {ColumnaAfectada_90DI ?? "—"}";
    }
}
