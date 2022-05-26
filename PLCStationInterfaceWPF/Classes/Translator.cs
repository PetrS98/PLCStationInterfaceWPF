using System;
using System.Collections.Generic;
using System.Text;

namespace PLCStationInterfaceWPF.Classes
{
    public enum EnumLanguage { CZ, ENG, DE}

    public static class Translator
    {
        private static EnumLanguage language = EnumLanguage.ENG;
        public static event EventHandler<EnumLanguage> LanguageChanged;

        public static EnumLanguage Language
        {
            get 
            { 
                return language; 
            }
            set
            {
                language = value;
                LanguageChanged?.Invoke(null, value);
            }
        }

    }
}
