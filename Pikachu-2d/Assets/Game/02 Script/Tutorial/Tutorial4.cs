using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4 : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Transform _trans;

    public void StartTut()
    {
        if (PlayerData.Instance.IsShowTutLevel4 == true) return;

        FunctionCommon.DelayTime(1f, () =>
        {
            this.gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 1f);

            HandleTut();
            GameController.Instance.uiGamePlayManager._mask.SetActive(true);
        });
    }

    public void HandleTut()
    {
        var data = BoosterManager.Instance.boosterShuffle;
        //  _trans.transform.localPosition = new Vector2(BoosterManager.Instance._shuffleTrans.localPosition.x, BoosterManager.Instance._shuffleTrans.localPosition.y + 100);
        _trans.gameObject.SetActive(true);

        data.GetComponent<Canvas>().sortingLayerName = "Ui1";//set layer cua time object cao hon layer cua canvas
        GameController.Instance.camController._canvas.sortingLayerName = "Ui";// set layer cho canvas
    }

    public void Close()
    {
        _trans.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        GameController.Instance.uiGamePlayManager._mask.SetActive(false);
        GameController.Instance.camController._canvas.sortingLayerName = "Default";// set layer cho canvas
        BoosterManager.Instance.boosterShuffle.GetComponent<Canvas>().sortingLayerName = "Default";

        PlayerData.Instance.IsShowTutLevel4 = true;
    }
}
