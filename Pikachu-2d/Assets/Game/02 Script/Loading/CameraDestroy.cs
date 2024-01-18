using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraDestroy : SingletonMonoBehaviour<CameraDestroy>
{
    public void SetCam(bool isActive)
    {
        this.GetComponent<Camera>().gameObject.SetActive(isActive);
    }
}
