using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeDescribe : MonoBehaviour
{
    public UnityEvent nextAction;
    public float _delayTime;
    void Start()
    {
        StartCoroutine(StartAction());
    }

    private IEnumerator StartAction()
    {
        yield return new WaitForSeconds(_delayTime);
        nextAction?.Invoke();
    }
}
