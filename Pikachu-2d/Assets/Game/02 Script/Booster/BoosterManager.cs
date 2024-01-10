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
        var boosterData = PlayerData.playerData.dataBooster;

        boosterTileTexture.setCount = (value) => boosterData.swap = value;
        boosterTileTexture.getCount = () => boosterData.swap;
        SetupBoosterView(boosterTileTexture);

        boosterShuffle.setCount = (value) => boosterData.shuffle = value;
        boosterShuffle.getCount = () => boosterData.shuffle;
        SetupBoosterView(boosterShuffle);

        boosterFindMatch.setCount = (value) => boosterData.findMatch = value;
        boosterFindMatch.getCount = () => boosterData.findMatch;
        SetupBoosterView(boosterFindMatch);
    }

    private void SetupBoosterView(BoosterView boosterView)
    {
        var button = boosterView.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => boosterView.UseBooster());

        boosterView.StartAction();
    }
}
