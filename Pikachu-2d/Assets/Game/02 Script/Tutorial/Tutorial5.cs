using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial5 : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Transform _trans;

    public void StartTut()
    {
        if (PlayerData.Instance.IsShowTutLevel5 == true) return;

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
        var data = BoosterManager.Instance.boosterFindMatch;
        _trans.transform.localPosition = new Vector2(BoosterManager.Instance._findTrans.localPosition.x, BoosterManager.Instance._findTrans.localPosition.y + 100);

        data.GetComponent<Canvas>().sortingLayerName = "Ui1";//set layer cua time object cao hon layer cua canvas
        GameController.Instance.camController._canvas.sortingLayerName = "Ui";// set layer cho canvas
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GameController.Instance.uiGamePlayManager._mask.SetActive(false);
        GameController.Instance.camController._canvas.sortingLayerName = "Default";// set layer cho canvas
        BoosterManager.Instance.boosterFindMatch.GetComponent<Canvas>().sortingLayerName = "Default";

        PlayerData.Instance.IsShowTutLevel5 = true;
    }
}
