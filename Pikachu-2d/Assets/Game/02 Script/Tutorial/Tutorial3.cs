using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    [SerializeField] Transform _trans;

    public void StartTut()
    {
        if (PlayerData.Instance.IsShowTutLevel3 == true) return;

        this.gameObject.SetActive(true);
        HandleTut();
        GameController.Instance.uiGamePlayManager._mask.SetActive(true);
    }

    public void HandleTut()
    {
        var data = BoosterManager.Instance.boosterTileTexture;
        _trans.transform.localPosition = new Vector2(BoosterManager.Instance.boosterTileTexture.transform.localPosition.x, BoosterManager.Instance.boosterTileTexture.transform.localPosition.y + 100);

        data.GetComponent<Canvas>().sortingLayerName = "Ui1";//set layer cua time object cao hon layer cua canvas
        GameController.Instance.camController._canvas.sortingLayerName = "Ui";// set layer cho canvas
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        GameController.Instance.uiGamePlayManager._mask.SetActive(false);
        GameController.Instance.camController._canvas.sortingLayerName = "Default";// set layer cho canvas
        BoosterManager.Instance.boosterTileTexture.GetComponent<Canvas>().sortingLayerName = "Default";

        PlayerData.Instance.IsShowTutLevel3 = true;
    }
}
