using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.UI;
public class LocalizationManager : MonoBehaviour
{
    public Locale[] langs;
    Dictionary<string, string> langNameDict = new Dictionary<string, string>();
    Text text;
    int langIndex;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Value").GetComponent<Text>();
        langNameDict["Japanese (Japan) (ja-JP)"] = "“ú–{Œê";
        langNameDict["English (United States) (en-US)"] = "English";
        
        //LocalizationSettings.SelectedLocale;
        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Next()
    {
        langIndex++;
        langIndex = langIndex % langs.Length;
        LocalizationSettings.SelectedLocale = langs[langIndex];
        text.text = langNameDict[langs[langIndex].name];
    }

    public void Back()
    {
        langIndex--;
        if (langIndex < 0)
        {
            langIndex = langs.Length - 1;
        }
        LocalizationSettings.SelectedLocale = langs[langIndex];
        text.text = langNameDict[langs[langIndex].name];
    }
}
