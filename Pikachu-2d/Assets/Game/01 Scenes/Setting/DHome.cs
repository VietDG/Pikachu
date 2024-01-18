using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DHome : Controller
{
    public const string SCENE_NAME = Const.SCENE_HOME;

    public override string SceneName()
    {
        return SCENE_NAME;
    }

    public override void OnActive(object data)
    {
        base.OnActive(data);
    }

    public override void OnHidden()
    {
    }

    public override void OnKeyBack()
    {
    }

    public override void OnReFocus()
    {
    }

    public override void OnShown()
    {
    }
}