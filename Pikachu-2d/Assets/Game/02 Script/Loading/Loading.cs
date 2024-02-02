using DG.Tweening;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] TMP_Text _loadingtxt;
    [SerializeField] Slider _slider;
    private int percent;
    [SerializeField] Image _bgLoading;
    [SerializeField] Image _logo;
    [SerializeField] TMP_Text _version;

    private void Awake()
    {
        _bgLoading.sprite = BackGroundManager.Instance._themeList.GetBg();
    }


    void Start()
    {
        StartCoroutine(Load());
        StartCoroutine(LoadingText());
        InitInfor();
    }

    private void InitInfor()
    {
        _version.text = GameLanguage.Get("txt_version") + $" {Application.version} - " + GameLanguage.Get("txt_build_number") + $" {GlobalSetting.Instance.GetBuildNumber()}";
    }

    IEnumerator Load()
    {
        int ran = UnityEngine.Random.Range(75, 90);
        while (percent < 150)
        {
            percent++;
            _slider.value = (float)percent / 100;

            if (percent == ran)
            {
                yield return new WaitForSecondsRealtime(1f);
            }
            // _loadingtxt.text = (percent >= 100) ? "100%" : $"{percent}%";
            yield return new WaitForSeconds(0.01f);
        }
        if (PlayerData.Instance.HighestLevel > 5)
            Manager.Load(DHome.SCENE_NAME);
        else
            Manager.Load(DGame.SCENE_NAME);

        SoundManager.Instance.PlayGameMusic();
    }

    private IEnumerator LoadingText()
    {
        while (true)
        {
            _loadingtxt.text = "Loading.";
            yield return new WaitForSeconds(0.3f);
            _loadingtxt.text = "Loading..";
            yield return new WaitForSeconds(0.3f);
            _loadingtxt.text = "Loading...";
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void SetAnimLogo()
    {
        //var sequence = DOTween.Sequence();
        ////var t1 = _logo.transform.DORotate(new Vector3(0f, 90f, 0), 1f);
        ////var t2 = _logo.transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
        //_logo.transform.localScale = Vector3.zero;
        ////  _logo.color = new Color(0, 0, 0);

        //var t1 = _logo.DOFade(0.6f, 0.5f);
        //var t2 = _logo.DOFade(1, 0.5f);
        //Tween tween = _logo.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetDelay(0.3f);

        //sequence.Append(t1);
        //sequence.Append(t2);
    }
}
