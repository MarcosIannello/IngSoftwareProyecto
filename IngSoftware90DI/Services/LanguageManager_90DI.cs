using Service_90DI;
using Services_90DI.entities;
using System.Text.Json;

namespace Services_90DI
{
    public static class LanguageManager_90DI
    {
        private static readonly List<IObserver_90DI> _observers = new();

        public static Dictionary<string, string> TraduccionesActuales_90DI { get; private set; } = new();

        public static string T(string key) =>
            TraduccionesActuales_90DI.TryGetValue(key, out var v) ? v : $"[{key}]";

        public static void AddObserver_90DI(IObserver_90DI observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public static void RemoveAllObservers_90DI()
        {
            _observers.Clear();
        }

        public static void Unsubscribe_90DI(IObserver_90DI observer)
        {
            _observers.Remove(observer);
        }

        public static void NotifyAllObservers_90DI(Idioma_90DI idioma)
        {
            var json = TranslationService_90DI.ChangeLanguage_90DI(idioma);
            var traducciones = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                               ?? new Dictionary<string, string>();

            TraduccionesActuales_90DI = traducciones;
            SessionManager_90DI.Instancia.IdiomaActual = idioma;

            foreach (var observer in _observers)
                observer.UpdateLanguage_90DI(traducciones);
        }
    }
}
