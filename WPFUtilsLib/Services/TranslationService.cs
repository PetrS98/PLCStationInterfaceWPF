using System;
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.Services
{
    public class TranslationService : ITranslationService
    {
        public event EventHandler<Language> LanguageChanged;

        private Language _language = Language.English;
        public Language Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    LanguageChanged?.Invoke(this, value);
                }
            }
        }
    }
}
