using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxTapController : MonoBehaviour
{
    public void OnClickButtonUI()
    {
        SoundManager.Instance.PlayUIButtonClick();
    }
}
