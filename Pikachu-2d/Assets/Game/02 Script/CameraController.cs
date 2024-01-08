using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    public Canvas canvas;

    public RectTransform topPanelTransform;

    public RectTransform botPanelTransform;

    public SpriteRenderer backgroundRenderer;


    private void Awake()
    {
        // cam = GetComponent<Camera>();
    }

    public void Initialize()
    {
        var gameBoard = GameManager.Instance;
        var boardSize = gameBoard.GetSize() + new Vector2(gameBoard.spacing * 1.2f, gameBoard.spacing * 1.2f);
        var canvasSize = GetCanvasSize();

        float topBoundY = canvasSize.y + topPanelTransform.anchoredPosition.y/* - topPanelTransform.sizeDelta.y * 0.5f*/;
        float bottomBoundY = botPanelTransform.anchoredPosition.y/* + botPanelTransform.sizeDelta.y * 0.5f*/;
        float boardSizeRatioY = (topBoundY - bottomBoundY) / canvasSize.y;
        Vector2 cameraSizeBoardSpace = new Vector2(cam.aspect * cam.orthographicSize * 2f, cam.orthographicSize * 2f * boardSizeRatioY);

        float sizeRatio = cameraSizeBoardSpace.x / cameraSizeBoardSpace.y;
        Vector2 parentSize = boardSize;
        Vector2 predictSize = new Vector2(parentSize.x, parentSize.x / sizeRatio);

        if (predictSize.y < parentSize.y)
        {
            predictSize *= parentSize.y / predictSize.y;
        }

        cam.orthographicSize *= predictSize.x / cameraSizeBoardSpace.x;

        float boardPositionRatioYInCanvasSpace = (topBoundY + bottomBoundY) * 0.5f / canvasSize.y;
        Vector3 cameraPosition = cam.transform.localPosition;
        cam.transform.localPosition = new Vector3(cameraPosition.x, -(boardPositionRatioYInCanvasSpace - 0.5f) * cam.orthographicSize * 2f, cameraPosition.z);

        Vector2 cameraSize = new Vector2(cam.aspect * cam.orthographicSize * 2f, cam.orthographicSize * 2f);

        AlignBgSize(cameraSize);
    }

    private Vector2 GetCanvasSize()
    {
        var canvasScale = canvas.GetComponent<CanvasScaler>();

        float rw = Screen.width / canvasScale.referenceResolution.x;
        float rh = Screen.height / canvasScale.referenceResolution.y;
        float match = canvasScale.matchWidthOrHeight;
        float scale = Mathf.Pow(rw, 1f - match) * Mathf.Pow(rh, match);
        return new Vector2(Screen.width / scale, Screen.height / scale);
    }

    private void AlignBgSize(Vector2 cameraSize)
    {
        Vector2 bgSize = backgroundRenderer.bounds.size;

        float spriteSizeRatio = bgSize.x / bgSize.y;
        Vector2 parentSize = cameraSize;
        Vector2 predictSize = new Vector2(parentSize.x, parentSize.x / spriteSizeRatio);

        if (predictSize.y < parentSize.y)
        {
            predictSize *= parentSize.y / predictSize.y;
        }

        backgroundRenderer.transform.localScale *= predictSize.x / bgSize.x;

        Vector3 backgroundPosition = backgroundRenderer.transform.localPosition;
        Vector3 cameraPosition = cam.transform.localPosition;
        backgroundRenderer.transform.localPosition = new Vector3(cameraPosition.x, cameraPosition.y, backgroundPosition.y);
    }
}
