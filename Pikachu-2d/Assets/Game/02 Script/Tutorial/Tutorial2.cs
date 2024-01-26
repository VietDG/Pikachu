using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private RectTransform _handTrans;

    public static GameController Current => GameController.Instance;

    public void StartTut()
    {
        FunctionCommon.DelayTime(1f, () =>
        {
            this.gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 1f);

            StateGame.PauseGame();
            HandleTut();
            GameController.Instance.uiGamePlayManager._mask.SetActive(true);
        });
    }

    private void HandleTut()
    {
        _handTrans.transform.localPosition = Current.uiGamePlayManager._timeTrans.transform.localPosition;// vị trí bàn tay

        var data = Current.uiGamePlayManager._timeObj.AddComponent<Canvas>();//add canvas cho time object

        data.overrideSorting = true;
        data.sortingLayerName = "Ui1";//set layer cua time object cao hon layer cua canvas
        Current.camController._canvas.sortingLayerName = "Ui";// set layer cho canvas
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        Current.uiGamePlayManager.SetMask(false);
        Current.camController._canvas.sortingLayerName = "Default";
    }
}
