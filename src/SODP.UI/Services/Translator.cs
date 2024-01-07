using System.Collections.Generic;

namespace SODP.UI.Services
{

    public enum Languages
    {
        En, Pl
    }

    public class Translator : ITranslator
    {
        private readonly Languages _currentLanguage;

        public Languages CurrentLanguage => _currentLanguage;

        public Translator(Languages language)
        {
            _currentLanguage = language;
        }

        public string Translate(string source) => Translate(source, CurrentLanguage);

        public string Translate(string source, Languages lang)
        {
			GetDictionary(lang).TryGetValue(source, out string translation);

			return translation ?? source;
        }

        private Dictionary<string, string> GetDictionary(Languages lang)
        {
            switch (lang)
            {
                case Languages.Pl: return _lang_pl;
                default: return _lang_en;
            }
        }

        private readonly Dictionary<string, string> _lang_pl = new()
        {
            { "Designer", "Projektant" },
            { "Checker", "Sprawdzający" },
            { "BadRequest", "Błędne żądanie" },
			{ "License already exist", "Uprawnienia już istnieją"}
		};

        private readonly Dictionary<string, string> _lang_en = new()
        {
            { "Designer", "Designer" },
            { "Checker", "Checker" },
            { "BadRequest", "Bad request" },
            { "License already exist", "License already exist"}
        };

	}
}
