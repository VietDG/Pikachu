using TMPro;
using UnityEngine;

public class AutoChangeFontTM : MonoBehaviour
{
    private TMP_Text text;
    private TMP_FontAsset df_font;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        df_font = text.font;
        ChangeFont();
    }

    private void OnDestroy()
    {
    }

    private void ChangeFont()
    {
        /*if (GameLanguage.DefaultLanguage)
            text.font = df_font;*/
        if (GameLanguage.TMFont != null)
        {
            if (text == null)
            {
                text = GetComponent<TMP_Text>();
            }
            if (text != null)
            {
                text.font = GameLanguage.TMFont;
            }
        }

    }
}
