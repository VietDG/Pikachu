using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterManager : SingletonMonoBehaviour<BoosterManager>
{
    public ChangeSpriteBooster boosterTileTexture;

    public ShuffleTileBooster boosterShuffle;

    public FindTileBooster boosterFindMatch;

    public void Start()
    {
        boosterTileTexture.setBoosterCount = (value) => PlayerData.Instance.BoosterSwap = value;
        boosterTileTexture.boosterCount = () => PlayerData.Instance.BoosterSwap;
        SetupBoosterView(boosterTileTexture);

        boosterShuffle.setBoosterCount = (value) => PlayerData.Instance.BoosterShuffle = value;
        boosterShuffle.boosterCount = () => PlayerData.Instance.BoosterShuffle;
        SetupBoosterView(boosterShuffle);

        boosterFindMatch.setBoosterCount = (value) => PlayerData.Instance.BoosterFindMatch = value;
        boosterFindMatch.boosterCount = () => PlayerData.Instance.BoosterFindMatch;
        SetupBoosterView(boosterFindMatch);
    }

    private void SetupBoosterView(BoosterController boosterController)
    {
        var b = boosterController.GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(() => boosterController.UseBooster());

        boosterController.StartAction();
    }
}
