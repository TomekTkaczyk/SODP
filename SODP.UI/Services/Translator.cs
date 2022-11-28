using System.Collections.Generic;

namespace SODP.UI.Services
{
    public enum Languages
    {
        En, Pl
    }

    public class Translator : ITranslator
    {
        public string Translate(string source, Languages lang)
        {
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
