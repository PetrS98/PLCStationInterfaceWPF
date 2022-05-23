using System;
using WPFUtilsLib.Services.Enums;

namespace WPFUtilsLib.Services
{

    public interface ITranslationService
    {
        public event EventHandler<Language> LanguageChanged;
        Language Language { get; set; }
    }
}
