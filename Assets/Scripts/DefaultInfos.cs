using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DefaultInfos : MonoBehaviour
{
    public static Dictionary<string, Dictionary<string, TextInfo>> game_Texts = new Dictionary<string, Dictionary<string, TextInfo>>();

    public static int current_language = 0;

    public static List<string> languages = new List<string>();

    static Action<string> OnLoadText;

    private void Awake()
    {
        OnLoadText += LoadText;
    }

    public static Sentence GetText(string id, string file_name)
    {
        if (game_Texts != null)
        {

            string language;
            if (!game_Texts.ContainsKey(file_name))
            {
                LoadText(file_name);

                language = languages[current_language];
            }
            else
            {
                language = languages[current_language];
            }

            if (game_Texts[file_name].ContainsKey(id))
                return game_Texts[file_name][id].sentences[language];
            else
                Debug.LogError($"Don't have a text with the Id: " + id);

            return null;
        }
        else
        {
            Debug.LogError($"The text aren't started.");
            return null;
        }
    }

    public static void LoadText(string file_name)
    {

        List<string> names = GetFilesNames();

        if (names.Contains($"{file_name}.csv"))
        {

            string[] rows = File.ReadAllLines($"{Application.dataPath}\\Texts\\{file_name}.csv");

            if (languages.Count == 0)
            {
                List<string> languages = new List<string>();
                string[] rows_languages = rows[0].Split(';');
                for (int i = 2; i < rows_languages.Length; i += 2)
                {
                    languages.Add(rows_languages[i].ToUpper());
                }

                DefaultInfos.languages = languages;
            }

            Dictionary<string, TextInfo> game_Texts = new Dictionary<string, TextInfo>();

            for (int i = 2; i < rows.Length; i++)
            {

                string[] values = rows[i].Split(';');

                TextInfo info = new TextInfo(values, languages);
                //info.ShowText();
                game_Texts.Add(info.Id, info);
            }

            DefaultInfos.game_Texts.Add($"{file_name}", game_Texts);
        }
        else
        {

            Debug.LogError($"O arquivo {file_name} não foi encontrado.");

        }
    }

    public static List<string> GetFilesNames()
    {
        string[] files = Directory.GetFiles(Application.dataPath + "\\Texts\\");

        List<string> names = new List<string>();

        foreach (var file in files)
        {
            string name = file.Replace(Application.dataPath + "\\Texts\\", "");

            if (!name.Contains(".meta"))
            {
                names.Add(name);
            }

        }

        return names;
    }

}

public class TextInfo
{

    public string Id { get; set; }
    public string Type { get; set; }
    public string Font { get; set; }
    // A chave representa a lingua
    public Dictionary<string, Sentence> sentences { get; set; }

    public TextInfo() { }

    public TextInfo(string[] values, List<string> linguas)
    {
        Id = values[0];
        Type = values[1];

        int id_language = 0;

        sentences = new Dictionary<string, Sentence>();

        for (int i = 2; i < values.Length; i += 2)
        {

            Sentence new_sentence = new Sentence();
            new_sentence.font = values[i];
            new_sentence.text = values[i + 1];


            sentences.Add(linguas[id_language], new_sentence);

            id_language++;
        }
    }

    public void ShowText()
    {
        foreach (var sentence in sentences.Keys)
        {
            Debug.Log($"Id: {Id} Lingua: {sentence} Texto: {sentences[sentence].text}");

        }
    }

}

public class Sentence
{
    public string font;
    public string text;
}