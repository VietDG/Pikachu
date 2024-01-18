using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _cam;

    [SerializeField] Canvas _canvas;

    [SerializeField] RectTransform _topTrans;

    [SerializeField] RectTransform _botTrans;

    //  [SerializeField] SpriteRenderer _bg;

    public void InitCam()
    {
        var gameManager = GameManager.Instance;
        var gameSize = gameManager.GetSizeTile() + new Vector2(gameManager.space * 1.2f, gameManager.space * 1.2f);
        var canvasSize = SetCanvasSize();

        float topSizeY = canvasSize.y + _topTrans.anchoredPosition.y;
        float BotSizeY = _botTrans.anchoredPosition.y;
        float gameSizeY = (topSizeY - BotSizeY) / canvasSize.y;
        Vector2 camSpace = new Vector2(_cam.aspect * _cam.orthographicSize * 2f, _cam.orthographicSize * 2f * gameSizeY);

        float size = camSpace.x / camSpace.y;
        Vector2 bigSize = gameSize;
        Vector2 smallSize = new Vector2(bigSize.x, bigSize.x / size);

        if (smallSize.y < bigSize.y)
        {
            smallSize *= bigSize.y / smallSize.y;
        }
        _cam.orthographicSize *= smallSize.x / camSpace.x;

        float gamePos = (topSizeY + BotSizeY) * 0.5f / canvasSize.y;
        Vector3 camPos = _cam.transform.localPosition;
        _cam.transform.localPosition = new Vector3(camPos.x, -(gamePos - 0.5f) * _cam.orthographicSize * 2f, camPos.z);

        Vector2 camSize = new Vector2(_cam.aspect * _cam.orthographicSize * 2f, _cam.orthographicSize * 2f);

        SetBgSize(camSize);
    }

    private Vector2 SetCanvasSize()
    {
        var canvasSize = _canvas.GetComponent<CanvasScaler>();

        float x = Screen.width / canvasSize.referenceResolution.x;
        float y = Screen.height / canvasSize.referenceResolution.y;
        float matchSize = canvasSize.matchWidthOrHeight;
        float scaleSize = Mathf.Pow(x, 1f - matchSize) * Mathf.Pow(y, matchSize);
        return new Vector2(Screen.width / scaleSize, Screen.height / scaleSize);
    }

    private void SetBgSize(Vector2 cameraSize)
    {
        //Vector2 bgSize = _bg.bounds.size;

        // float spriteSize = bgSize.x / bgSize.y;
        Vector2 bigSize = cameraSize;
        Vector2 smallSize = new Vector2(bigSize.x, bigSize.x);

        if (smallSize.y < bigSize.y)
        {
            smallSize *= bigSize.y / smallSize.y;
        }

        // _bg.transform.localScale *= smallSize.x / bgSize.x;

        //  Vector3 bgPos = _bg.transform.localPosition;
        Vector3 camPos = _cam.transform.localPosition;
        // _bg.transform.localPosition = new Vector3(camPos.x, camPos.y, bgPos.y);
    }
}
