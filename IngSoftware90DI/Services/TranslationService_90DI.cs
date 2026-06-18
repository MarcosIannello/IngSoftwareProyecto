using Services_90DI.entities;

namespace Services_90DI
{
    public static class TranslationService_90DI
    {
        public static string ChangeLanguage_90DI(Idioma_90DI idioma)
        {
            try
            {
                var ruta = Path.Combine(AppContext.BaseDirectory, "Resources", "lang", $"{idioma.CodigoIdioma_90DI}.json");
                if (!File.Exists(ruta)) return "{}";
                return File.ReadAllText(ruta);
            }
            catch
            {
                return "{}";
            }
        }
    }
}
