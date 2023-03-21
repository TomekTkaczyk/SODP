using Microsoft.Extensions.Configuration;

namespace SODP.UI.Services
{
	public class LanguageTranslatorFactory
	{
		private ITranslator _translator;
		public LanguageTranslatorFactory(IConfiguration configuration)
		{
			_translator = new Translator(Languages.Pl);
		}

		public ITranslator GetTranslator() 
		{
			return _translator;
		}
	}
}
