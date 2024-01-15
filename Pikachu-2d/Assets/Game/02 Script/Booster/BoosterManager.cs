using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{
    public ChangeSpriteBooster boosterTileTexture;

    public ShuffleTileBooster boosterShuffle;

    public FindTileBooster boosterFindMatch;

    public void Start()
    {
        boosterTileTexture.setCount = (value) => PlayerData.Instance.BoosterSwap = value;
        boosterTileTexture.getCount = () => PlayerData.Instance.BoosterSwap;
        SetupBoosterView(boosterTileTexture);

        boosterShuffle.setCount = (value) => PlayerData.Instance.BoosterShuffle = value;
        boosterShuffle.getCount = () => PlayerData.Instance.BoosterShuffle;
        SetupBoosterView(boosterShuffle);

        boosterFindMatch.setCount = (value) => PlayerData.Instance.BoosterFindMatch = value;
        boosterFindMatch.getCount = () => PlayerData.Instance.BoosterFindMatch;
        SetupBoosterView(boosterFindMatch);
    }

    private void SetupBoosterView(BoosterController boosterView)
    {
        var button = boosterView.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => boosterView.UseBooster());

        boosterView.StartAction();
    }
}
