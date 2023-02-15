namespace SODP.UI.Services
{
    public interface ITranslator
    {
        Languages CurrentLanguage { get; }
        string Translate(string source, Languages lang);
        string Translate(string source);
    }
}
