using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreenController : MonoBehaviour
{
    public Image image;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void StartAnimating(string sceneName, Action PreLoadAction, Action PostLoadAction)
    {
        gameObject.SetActive(true);
        StartCoroutine(Animate(sceneName, PreLoadAction, PostLoadAction));
    }

    private IEnumerator Animate(string sceneName, Action PreLoadAction, Action PostLoadAction)
    {    
        //image.DOColor(Color.black, 0.3f);

        //yield return new WaitForSeconds(0.3f);

        PreLoadAction?.Invoke();

        var operation = SceneManager.LoadSceneAsync(sceneName);
        //while (!operation.isDone)
        //{
        //    yield return null;
        //}

        PostLoadAction?.Invoke();

        //image.DOColor(Color.black, 0.3f);

       //yield return new WaitForSeconds(0.3f);

        yield return null;
        gameObject.SetActive(false);   
    }
}
