using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public StateScence state;

    private void Awake()
    {
        CameraDestroy.Instance.SetCam(true);
    }
    void Start()
    {
        SettingData.Instance.StateScence = StateScence.Home;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickPlay()
    {
        //SceneManager.LoadScene(Const.SCENE_GAME);
        Manager.Load(DGame.SCENE_NAME);
    }

    public void OnClickSettting()
    {
        PopupSetting.Instance.Show();
    }

}
