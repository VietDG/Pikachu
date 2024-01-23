﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameLanguage : MonoBehaviour
{
    #region Instance
    private static GameLanguage _instance;
    public static GameLanguage Instance
    {
        get { return _instance; }
    }
    #endregion

    #region Inspector Variables
    public string docID = "1aLfWcdRqS-DvuXdBM_MDJv2L4zha615UF-z4sG8HVwY";
    public int sheetID = 927464915;
    public TextAsset asset;
    public List<LanguageAsset> langs;
    public string default_code = "EN";
    #endregion

    #region Member Variables

    public string crr_lang_code;
    private static Dictionary<string, Hashtable> dictionaryLang = new Dictionary<string, Hashtable>();
    public static Hashtable dictHash;
    public static LanguageAsset langAsset;
    public static bool DefaultLanguage;

    public delegate void SimpleEvent();
    // public event SimpleEvent evtChangeFont;
    //public event SimpleEvent evtChangeText;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _instance = this;
        PrepareLanguageData();

        crr_lang_code = PlayerPrefs.GetString("SETTING_LANG", "");
        if (crr_lang_code == "")
        {
            // Check device language
            Debug.Log("System Language: " + Application.systemLanguage.ToString());
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    crr_lang_code = "EN";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Vietnamese:
                    crr_lang_code = "VI";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Korean:
                    crr_lang_code = "KO";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.German:    //tiếng Đức
                    crr_lang_code = "DE";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Spanish:
                    crr_lang_code = "ES";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Portuguese:
                    crr_lang_code = "PT";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Italian:
                    crr_lang_code = "IT";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Dutch:   //tiếng Hà Lan
                    crr_lang_code = "NL";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.French:
                    crr_lang_code = "FR";
                    SetLanguage(crr_lang_code);
                    break;
                case SystemLanguage.Japanese:
                    crr_lang_code = "JA";
                    SetLanguage(crr_lang_code);
                    break;
                default:
                    crr_lang_code = default_code;
                    SetLanguage(crr_lang_code);
                    break;
            }
        }
        else
        {
            SetLanguage(crr_lang_code);
        }
    }
    #endregion

    #region Public Methods
    public bool SetLanguage(string lang_code)
    {
        // Debug.Log("SetLanguage: " + lang_code);
        crr_lang_code = lang_code;
        DefaultLanguage = (lang_code == default_code);

        if (dictionaryLang.ContainsKey(lang_code))
        {
            dictHash = dictionaryLang[lang_code];
            foreach (LanguageAsset asset in langs)
                if (asset.lang_code == lang_code)
                {
                    langAsset = asset;
                    //Debug.Log($"SetLanguage: {asset.lang_code}");
                    PlayerPrefs.SetString("SETTING_LANG", asset.lang_code);
                    // evtChangeFont?.Invoke();
                    //evtChangeText?.Invoke();
                    break;
                }
        }
        return false;
    }

    public static string Get(string key)
    {
        if (dictHash != null && dictHash.ContainsKey(key))
        {
            return (string)dictHash[key];
        }
        else
        {
            Debug.Log("[GameLanguage]: key(" + key + ") is empty value");
            return "empty!!";
        }
    }

    public static Font Font
    {
        get
        {
            if (langAsset != null)
                return langAsset.font;
            else
                return null;
        }
    }

    public static TMP_FontAsset TMFont
    {
        get
        {
            if (langAsset != null && langAsset.tm_font != null)
                return langAsset.tm_font;
            else
                return null;
        }
    }

    #endregion

    #region Private Methods

    private string UnicodeToUTF8(string unicodeString)
    {
        // Create two different encodings.
        Encoding ascii = Encoding.UTF8;
        Encoding unicode = Encoding.Unicode;
        // Convert the string into a byte array.
        byte[] unicodeBytes = unicode.GetBytes(unicodeString);
        // Perform the conversion from one encoding to the other.
        byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

        // Convert the new byte[] into a char[] and then into a string.
        char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
        ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
        string asciiString = new string(asciiChars);
        return (asciiString);
    }

    private string UTF8TOUnicode(string utf8)
    {
        // Create two different encodings.
        Encoding ascii = Encoding.UTF8;
        Encoding unicode = Encoding.Unicode;
        // Convert the string into a byte array.
        byte[] asciiBytes = ascii.GetBytes(utf8);
        // Perform the conversion from one encoding to the other.
        byte[] unicodeBytes = Encoding.Convert(ascii, unicode, asciiBytes);

        // Convert the new byte[] into a char[] and then into a string.
        char[] unicodeChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
        unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);
        string unicodeString = new string(unicodeChars);
        return (unicodeString);
    }

    private void PrepareLanguageData()
    {

        if (asset != null || !string.IsNullOrEmpty(asset.text))
        {
            ReadLocalData(asset.text);
        }
        else
        {
            LoadData(docID);
        }
    }

    private void LoadData(string dataID)
    {
        var data = CSVOnlineReader.ReadGSheet(dataID, sheetID);
        if (data != null && data.Count > 0)
        {
            string sData = JsonConvert.SerializeObject(data);
            File.WriteAllText("Assets/MyTools/Multilang/Resources/Languages/languages.txt", sData);

            List<string> headers = new List<string>();
            foreach (string key in data[0].Keys)
            {
                if (key != "KEY" && !headers.Contains(key))
                {
                    headers.Add(key);
                }
            }

            for (int i = 0; i < headers.Count; i++)
            {
                if (!dictionaryLang.ContainsKey(headers[i]))
                    dictionaryLang.Add(headers[i], new Hashtable());
            }

            foreach (var dict in data)
            {
                string id_text = dict["KEY"];
                foreach (string key in dict.Keys)
                {
                    if (key != "KEY")
                    {
                        if (dictionaryLang.ContainsKey(key))
                        {
                            if (!dictionaryLang[key].ContainsKey(id_text))
                                dictionaryLang[key].Add(id_text, UTF8TOUnicode(dict[key]));
                        }
                        else
                        {
                            Hashtable hashtable = new Hashtable();
                            hashtable.Add(id_text, UTF8TOUnicode(dict[key]));
                            dictionaryLang.Add(key, hashtable);
                        };
                    }
                }
            }
        }
    }

    private void ReadLocalData(string str)
    {
        List<Dictionary<string, string>> data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(str);
        if (data != null && data.Count > 0)
        {
            string sData = JsonConvert.SerializeObject(data);
            //File.WriteAllText("Assets/Game/Resources/Data/LanguageData.txt", sData);

            List<string> headers = new List<string>();
            foreach (string key in data[0].Keys)
            {
                if (key != "KEY")
                {
                    headers.Add(key);
                }
            }

            for (int i = 0; i < headers.Count; i++)
            {
                if (dictionaryLang.ContainsKey(headers[i])) return;

                dictionaryLang.Add(headers[i], new Hashtable());
            }

            foreach (var dict in data)
            {
                string id_text = dict["KEY"];
                foreach (string key in dict.Keys)
                {
                    if (key != "KEY")
                    {
                        if (dictionaryLang.ContainsKey(key))
                        {
                            if (!dictionaryLang[key].ContainsKey(id_text))
                                dictionaryLang[key].Add(id_text, UTF8TOUnicode(dict[key]));
                        }
                        else
                        {
                            Hashtable hashtable = new Hashtable();
                            hashtable.Add(id_text, UTF8TOUnicode(dict[key]));
                            dictionaryLang.Add(key, hashtable);
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Custom Inspector   
#if UNITY_EDITOR
    [CustomEditor(typeof(GameLanguage))]
    public class LoadDataFromGSheet : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GameLanguage control = (GameLanguage)target;
            if (GUILayout.Button("Load All Data from GSheet"))
            {
                control.LoadData(control.docID);
            }
        }
    }
#endif
    #endregion
}

[System.Serializable]
public class LanguageAsset
{
    public string name;
    public string lang_code;
    public Font font;
    public TMP_FontAsset tm_font;
}


