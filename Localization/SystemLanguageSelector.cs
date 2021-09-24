using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine;

[System.Serializable]
public class SystemLanguageSelector
  : UnityEngine.Localization.Settings.IStartupLocaleSelector
{
    public Locale GetStartupLocale(ILocalesProvider provider)
    {
        Debug.Log(LocalizationSettings.AvailableLocales.GetLocale(Application.systemLanguage));
        return provider.GetLocale(Application.systemLanguage);
    }
}