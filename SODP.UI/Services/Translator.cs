using SODP.Model;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace SODP.UI.Services
{
    
    public enum Languages
    {
        En, Pl
    }

    public class Translator : ITranslator
    {
        private Languages _currentLanguage;

        public Languages CurrentLanguage => _currentLanguage;

        public Translator(Languages language)
        {
            _currentLanguage = language;
        }

        public string Translate(string source) => Translate(source, CurrentLanguage);

        public string Translate(string source, Languages lang)
        {
			//var regex = new Regex("'(.*?)'");
			//var regex = new Regex("'(.*?[^\\])'")
			//var matches = regex.Match(source);

			GetDictionary(lang).TryGetValue(source, out string translation);

			return translation ?? "";
        }

        private Dictionary<string, string> GetDictionary(Languages lang)
        {
            switch (lang)
            {
                case Languages.Pl: return lang_pl;
                default: return lang_en;
            }
        }

        private readonly Dictionary<string, string> lang_pl = new Dictionary<string, string>
        {
            { "Designer", "Projektant" },
            { "Checker", "Sprawdzający" }
        };

        private readonly Dictionary<string, string> lang_en = new Dictionary<string, string>
        {
            { "Designer", "Designer" },
            { "Checker", "Checker" }
        };

	}
}
