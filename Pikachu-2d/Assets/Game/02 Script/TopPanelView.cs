using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelView : MonoBehaviour
{
    public TMP_Text levelText;

    public TMP_Text coinText;

    [Header("Time")]
    public Image timeProgressImage;

    public TMP_Text timeText;

    public GameObject timeObject;

    [Header("Star")]
    public Image starProgressImage;

    public GameObject[] starObjects;

    public Func<float> GetTimeAction;

    private float levelTime;

    private int remainingTime;

    private StringBuilder stringBuilder = new StringBuilder();

    private Tween starProgressTween;

    private Tween oldTween;

    private Vector3 starCollectBasePosition;

    private float starProgressLength;

    private float maxCollectStarAmount = 100;

    private float collectedStarAmount;

    private bool shouldRecalculateCollectBasePosition = true;

    private float[] starProgressMilestone;

    private void Start()
    {
        UpdateCoin(UserData.current.userStatus.coinCount);

        EventDispatcher.Instance.RegisterEvent("coin_update", UpdateCoin);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveEvent("coin_update", UpdateCoin);
    }

    public void SetLevel(int level)
    {
        levelText.text = "Level " + UserData.current.userStatus.level.ToString();

        shouldRecalculateCollectBasePosition = true;
    }

    public void SetLevelTime(float time)
    {
        levelTime = time;
    }

    public void SetRemainingTime(float time)
    {
        timeProgressImage.fillAmount = time / levelTime;// slider time 

        time += 1f;

        if (remainingTime != (int)time)//thời gian còn lại
        {
            remainingTime = (int)time;
            DateTimeUtility.ToMinuteSecond(stringBuilder, remainingTime);// thời gian theo giây,phút
            timeText.text = stringBuilder.ToString();
        }
        // timer giảm dần ;
    }

    public void SetCollectedStarProgressMilestone(float[] milestoneValue)
    {
        for (int i = 0; i < starObjects.Length; i++)
        {
            starObjects[i].SetActive(false);
        }

        starProgressMilestone = milestoneValue;
    }

    public void SetCollectedStar(float collectedAmount, float maxAmount)
    {
        collectedStarAmount = collectedAmount;
        maxCollectStarAmount = maxAmount;

        starProgressImage.fillAmount = collectedStarAmount / maxCollectStarAmount;
    }

    public void GetCollectedStar(out float collectedAmount, out float maxAmount)
    {
        collectedAmount = collectedStarAmount;
        maxAmount = maxCollectStarAmount;
    }

    public void PauseButtonPress()
    {
    }

    public void OnStarsCollected(float amount, float delayTime)
    {
        collectedStarAmount = Mathf.Min(maxCollectStarAmount, collectedStarAmount + amount);

        starProgressTween = oldTween = starProgressImage.DOFillAmount(collectedStarAmount / maxCollectStarAmount, 0.5f).SetDelay(delayTime).
            SetEase(Ease.OutCubic).OnUpdate(() =>
            {
                for (int i = 0; i < starObjects.Length; i++)
                {
                    starObjects[i].SetActive(starProgressImage.fillAmount >= starProgressMilestone[i]);
                }
            }).
            OnStart(() =>
            {
                if (oldTween != null && !oldTween.IsActive())
                {
                    oldTween.Kill();
                }

                oldTween = starProgressTween;
            });
    }

    public Vector3 GetCollectStarPosition()
    {
        if (shouldRecalculateCollectBasePosition)
        {
            Vector3[] v = new Vector3[4];
            starProgressImage.rectTransform.GetWorldCorners(v);

            starCollectBasePosition = (v[0] + v[1]) * 0.5f;
            starProgressLength = Mathf.Abs(v[2].x - v[1].x);

            shouldRecalculateCollectBasePosition = false;
        }

        return starCollectBasePosition + starProgressLength * collectedStarAmount / maxCollectStarAmount * Vector3.right;
    }

    public void SetTimeCount(bool flag)
    {
        timeObject.SetActive(flag);
    }

    private void UpdateCoin(object param)
    {
        coinText.text = param.ToString();
    }

    public void OpenShop()
    {
    }
}
