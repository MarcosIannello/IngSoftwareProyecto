using Service_90DI;
using Services_90DI.entities;
using System.Text.Json;

namespace Services_90DI
{
    public class LanguageManager_90DI : IPublisher_90DI
    {
        public static readonly LanguageManager_90DI Current = new();

        private readonly List<IObserver_90DI> _observers = new();

        public Dictionary<string, string> TraduccionesActuales_90DI { get; private set; } = new();

        // Atajo estático: los forms pueden seguir usando LanguageManager_90DI.T("key")
        public static string T(string key) =>
            Current.TraduccionesActuales_90DI.TryGetValue(key, out var v) ? v : $"[{key}]";

        public void AddObserver_90DI(IObserver_90DI observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void RemoveAllObservers_90DI()
        {
            _observers.Clear();
        }

        public void Unsubscribe_90DI(IObserver_90DI observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyAllObservers_90DI(Idioma_90DI idioma)
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
