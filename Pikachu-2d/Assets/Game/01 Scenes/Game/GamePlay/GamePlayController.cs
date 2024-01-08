using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GamePlayController : Controller
{
    public const string GAMEPLAY_SCENE_NAME = "GamePlay";

    public override string SceneName()
    {
        return GAMEPLAY_SCENE_NAME;
    }
}