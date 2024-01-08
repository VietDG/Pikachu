using System;
//using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    private static bool pluginLoaded = false;

    // public Text textName;

    public static UnityAction<string> ChangeNameAction;

    // public Button test;

    public UserStatus userStatus;
    public UserData userdata;

    public void Start()
    {
        if (!pluginLoaded)
        {
            pluginLoaded = true;
        }

        ChangeName(UserData.current.userStatus.name);
        ChangeNameAction = ChangeName;
    }

    private void ChangeName(string name)
    {
        //    textName.text = name;
        UserData.current.userStatus.name = name;
    }
}
