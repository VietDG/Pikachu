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
    }

    public override void OnReFocus()
    {
    }

    public override void OnShown()
    {
        //GameController.Instance.InitLevel();
        m_Camera = cam;
    }

    public override void OnHidden()
    {
    }
}