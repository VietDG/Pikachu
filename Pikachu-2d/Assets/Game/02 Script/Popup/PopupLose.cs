using PopupSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupLose : SingletonPopup<PopupLose>
{
    public void Show()
    {
        base.Show();
        //StateGame.PauseGame();
    }

    public void Close()
    {
        base.Hide();
    }

    public void OnClickReplay()
    {
        base.Hide(() =>
        {
            SceneManager.LoadScene(Const.SCENE_GAME);
        });
    }

    public void OnClickConfirlm()
    {

    }
}
