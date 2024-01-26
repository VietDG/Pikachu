using PopupSystem;
using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupSetting : SingletonPopup<PopupSetting>
{
    [SerializeField]
    private GameObject _sfxOn, _sfxOff, _musicOn, _musicOff, _vibrateOn, _vibrateOff;
    [SerializeField]
    private GameObject _tutorialBtn, _replayBtn, _langueBtn, _homeBtn;

    public void Show()
    {
        base.Show();
    }

    public void Close()
    {
        base.Hide(() =>
        {
            StateGame.Play();
        });
    }

    private void Start()
    {
        SetUpIcon();
        SetUpShow();
    }

    public void OnClickSfx()
    {
        if (SettingData.Instance.SOUND)
        {
            _sfxOn.SetActive(false);
            _sfxOff.SetActive(true);
        }
        else
        {
            _sfxOff.SetActive(false);
            _sfxOn.SetActive(true);
        }

        SettingData.Instance.SOUND = !SettingData.Instance.SOUND;
        SoundManager.Instance.SoundHandle(SettingData.Instance.SOUND);
    }

    public void OnClickMusic()
    {
        if (SettingData.Instance.MUSIC)
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
        }
        else
        {
            _musicOff.SetActive(false);
            _musicOn.SetActive(true);
        }
        SettingData.Instance.MUSIC = !SettingData.Instance.MUSIC;
        SoundManager.Instance.MusicHandle(SettingData.Instance.MUSIC);
    }

    public void OnClickVibrate()
    {
        if (SettingData.Instance.VIBRATE)
        {
            _vibrateOn.SetActive(false);
            _vibrateOff.SetActive(true);
        }
        else
        {
            _vibrateOff.SetActive(false);
            _vibrateOn.SetActive(true);
        }
        SettingData.Instance.VIBRATE = !SettingData.Instance.VIBRATE;
    }

    public void SetUpIcon()
    {
        if (SettingData.Instance.SOUND)
        {
            _sfxOn.SetActive(true);
            _sfxOff.SetActive(false);
        }
        else
        {
            _sfxOff.SetActive(true);
            _sfxOn.SetActive(false);
        }

        if (SettingData.Instance.MUSIC)
        {
            _musicOn.SetActive(true);
            _musicOff.SetActive(false);
        }
        else
        {
            _musicOff.SetActive(true);
            _musicOn.SetActive(false);
        }

        if (SettingData.Instance.VIBRATE)
        {
            _vibrateOn.SetActive(true);
            _vibrateOff.SetActive(false);
        }
        else
        {
            _vibrateOff.SetActive(true);
            _vibrateOn.SetActive(false);
        }
    }

    public void OnClickReplay()
    {
        // StopAllCoroutines();
        base.Hide(() =>
        {
            // SceneManager.LoadScene(Const.SCENE_GAME);
            Manager.Load(DGame.SCENE_NAME);
        });
    }

    public void OnClickTutorial()
    {
        //PopupTUtorial Show
        Debug.LogError("ShowTutorial");
    }

    public void OnClickLangue()
    {
        base.Hide(() =>
        {
            PopupLanguage.Instance.Show();
        });
    }

    public void OnClickMap()
    {
        base.Hide(() =>
        {
            // SceneManager.LoadScene(Const.SCENE_HOME);
            Manager.Load(DHome.SCENE_NAME);
        });
    }

    public void SetUpShow()
    {
        if (SettingData.Instance.StateScence == StateScence.Home)
        {
            _tutorialBtn.SetActive(true);
            _replayBtn.SetActive(false);

            _langueBtn.SetActive(true);
            _homeBtn.SetActive(false);
        }
        else
        {
            _replayBtn.SetActive(true);
            _tutorialBtn.SetActive(false);
            _homeBtn.SetActive(true);
            _langueBtn.SetActive(false);
        }
    }
}
