using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Start_menu : MonoBehaviour
{
    public TextMeshProUGUI test_text;
    void Start()
    {
        if(PlayerPrefs.HasKey("Language") == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetInt("Language", 1);
            else if (Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional) PlayerPrefs.SetInt("Language", 2);
            else PlayerPrefs.SetInt("Language", 0);
        }
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
    }

    public void Language_change(int languageID)
    {
        PlayerPrefs.SetInt("Language", languageID);
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
    }

    public void Show_text()
    {
        test_text.enabled = true;
        test_text.text = Translator.Get_text(4);
    }
}
