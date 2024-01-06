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
        var boosterData = UserData.current.boosterData;

        boosterTileTexture.SetCount = (value) => boosterData.swapTexCount = value;
        boosterTileTexture.GetCount = () => boosterData.swapTexCount;
        SetupBoosterView(boosterTileTexture);

        boosterShuffle.SetCount = (value) => boosterData.shuffleCount = value;
        boosterShuffle.GetCount = () => boosterData.shuffleCount;
        SetupBoosterView(boosterShuffle);

        boosterFindMatch.SetCount = (value) => boosterData.findMatchCount = value;
        boosterFindMatch.GetCount = () => boosterData.findMatchCount;
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
