using SS.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private int percent;
    //float loadTime = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        int ran = UnityEngine.Random.Range(75, 90);
        while (percent < 150)
        {
            percent++;
            // _slider.value = (float)percent / 100;

            yield return new WaitForSeconds(0.01f);
        }
        //  Manager.Load(DGame.SCENE_NAME);
        SceneManager.LoadScene(Const.SCENE_GAME);
    }
}
