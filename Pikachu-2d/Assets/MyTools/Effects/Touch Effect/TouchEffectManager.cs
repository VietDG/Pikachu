using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    #region Instance

    private static TouchEffectManager instance;

    public static TouchEffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TouchEffectManager>();
                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<TouchEffectManager>("TouchEffectManager"));
                }
            }

            return instance;
        }
    }
    #endregion
    
    #region Inspector Variables
    public GameObject touchEff;
    #endregion

    #region Member Variables
    private Transform mTrans;
    #endregion
    
    #region Unity Methods

    private void Awake()
    {
        if (FindObjectsOfType<TouchEffectManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        mTrans = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = SimplePool.Spawn(touchEff,pos,Quaternion.Euler(Vector3.zero));
            go.transform.parent = mTrans;
            if(go!=null)
                StartCoroutine(DisableTask(go, 0.5f));
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    #endregion

    #region Public Methods
    public void Init()
    {
        SimplePool.Preload(touchEff,1);
    }
    #endregion
    
    #region Private Methods
    private IEnumerator DisableTask(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (go != null)
        {
            go.SetActive(false);
            SimplePool.Despawn(go);
        }
    }
    #endregion
}
