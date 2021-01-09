using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    public string file_name;
    public string[] dialogs;
    int index;
    public float typingSpeed = 0.01f;
    public Text DialogTextBox;
    bool typingText;
    private int last_Language;

    Coroutine coroutine;

    public UnityEvent OnEndDialog;

    public void Start()
    {

        DialogTextBox.text = DefaultInfos.GetText(dialogs[index], file_name).text;

    }

    public void nextDialog()
    {
        last_Language = DefaultInfos.current_language;

        if (typingText)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            DialogTextBox.text = DefaultInfos.GetText(dialogs[index], file_name).text;

            typingText = false;
        }
        else
        {
            if (index + 1 < dialogs.Length)
            {
                index++;
                coroutine = StartCoroutine(SetDialog(DefaultInfos.GetText(dialogs[index], file_name)));
            }
            else
            {
                OnEndDialog?.Invoke();
            }

 
        }

    }

    IEnumerator SetDialog(Sentence text)
    {
        typingText = true;

        DialogTextBox.text = "";

        char[] latters_text = text.text.ToCharArray();


        foreach (char latter in latters_text)
        {

            DialogTextBox.text += latter;

            yield return new WaitForSeconds(typingSpeed);
        }

        typingText = false;
    }

    private void ResetTextByLanguage()
    {
        if (last_Language != DefaultInfos.current_language)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            DialogTextBox.text = DefaultInfos.GetText(dialogs[index], file_name).text;

            typingText = false;

            last_Language = DefaultInfos.current_language;
        }
    }

    private void LateUpdate()
    {
        ResetTextByLanguage();
    }


}
