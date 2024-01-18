using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private void Awake()
    {
        CameraDestroy.Instance.SetCam(true);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(Const.SCENE_GAME);
    }
}
