using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    public string file_name;
    public string Id;

    public Text text;

    private int last_Language;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
    }

    private void Start()
    {

        SetThisText();

    }

    private void LateUpdate()
    {
        if(last_Language != DefaultInfos.current_language) {
            SetThisText();
        }
    }

    public void SetThisText()
    {

        Sentence sentence = DefaultInfos.GetText(Id, file_name);
        text.text =  sentence.text;

        last_Language = DefaultInfos.current_language;

    }

}
