using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageOption : MonoBehaviour
{

    public string file_name = "languages_options";
    Dropdown dropdown;
    public Text displayText;
    private int last_Language;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void Start()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 0; i < DefaultInfos.languages.Count; i++)
        {
            string Id = (i + 1).ToString();
            Dropdown.OptionData option = new Dropdown.OptionData(DefaultInfos.GetText(Id, file_name).text);

            options.Add(option);

        }

        last_Language = DefaultInfos.current_language;


        dropdown.options = options;

    }

    public void SetLanguage()
    {
        DefaultInfos.current_language = dropdown.value;
    }

    private void ResetTextByLanguage()
    {



        if (last_Language != DefaultInfos.current_language)
        {

            for (int i = 0; i < dropdown.options.Count; i++)
            {
                string Id = (i + 1).ToString();
                dropdown.options[i].text = DefaultInfos.GetText(Id, file_name).text;
            }

            if (displayText != null)
            {
                string Id = (dropdown.value + 1).ToString();

                displayText.text = DefaultInfos.GetText(Id, file_name).text;
            }

            last_Language = DefaultInfos.current_language;
        }
    }

    private void LateUpdate()
    {
        ResetTextByLanguage();
    }

}
