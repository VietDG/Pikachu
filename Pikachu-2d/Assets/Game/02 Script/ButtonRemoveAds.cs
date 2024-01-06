using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRemoveAds : MonoBehaviour
{
    private void Start()
    {
        CheckVisible();

        EventDispatcher.Instance.RegisterEvent("remove_ads", CheckVisible);
    }

    private void Destroy()
    {
        EventDispatcher.Instance.RemoveEvent("remove_ads", CheckVisible);
    }

    private void CheckVisible(object param = null)
    {
        if (UserData.current.rewardData.removedAds)
        {
            gameObject.SetActive(false);
        }
    }

    public void ButtonPress()
    {
    }
}
