using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGame : Controller
{
    public const string SCENE_NAME = Const.SCENE_GAME;

    public Camera cam;

    public override string SceneName()
    {
        return SCENE_NAME;
    }

    public override void OnActive(object data)
    {
        m_Canvas.worldCamera = cam;
        // GameController.Instance.InitLevel();
    }

    public override void OnReFocus()
    {
    }

    public override void OnShown()
    {
        if (PlayerData.Instance.HighestLevel == 7)
        {
            PopupTutHamer.Instance.Show();
        }
        EventAction.OnShowBanner?.Invoke();
    }

    public override void OnHidden()
    {
    }
}