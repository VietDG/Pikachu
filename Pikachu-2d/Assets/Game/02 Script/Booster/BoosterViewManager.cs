using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterViewManager : MonoBehaviour
{
    public BoosterChangePicture boosterTileTexture;

    public BoosterShuffle boosterShuffle;

    public BoosterFindMatch boosterFindMatch;

    public void Start()
    {
        var boosterData = PlayerData.current.boosterData;

        boosterTileTexture.SetCount = (value) => boosterData.swap = value;
        boosterTileTexture.GetCount = () => boosterData.swap;
        SetupBoosterView(boosterTileTexture);

        boosterShuffle.SetCount = (value) => boosterData.shuffle = value;
        boosterShuffle.GetCount = () => boosterData.shuffle;
        SetupBoosterView(boosterShuffle);

        boosterFindMatch.SetCount = (value) => boosterData.findMatch = value;
        boosterFindMatch.GetCount = () => boosterData.findMatch;
        SetupBoosterView(boosterFindMatch);
    }

    private void SetupBoosterView(BoosterView boosterView)
    {
        var button = boosterView.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => boosterView.OnClick());

        boosterView.UpdateState();
    }
}
