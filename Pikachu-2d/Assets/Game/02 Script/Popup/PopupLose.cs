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

    public void OnClickReplay()
    {
        base.Hide(() =>
        {
            SceneManager.LoadScene(Const.SCENE_GAME);
        });
    }

    public void OnClickHome()
    {
        base.Hide(() =>
        {
            SceneManager.LoadScene(Const.SCENE_HOME);
        });
    }

    public void OnClickRevive()
    {
        // StateGame.Play();
        StartCoroutine(GameController.Instance.UpdateTime());
        base.Hide(() =>
        {
            GameController.Instance.time = LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime;
            GameController.Instance.uiGamePlayManager._totalTime = LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime;
            GameController.Instance.uiGamePlayManager.InitTimeToLevel(LevelData.Instance.GetLevelConfig(PlayerData.Instance.HighestLevel).leveltime);
        });
    }
}
