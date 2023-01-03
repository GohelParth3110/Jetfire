using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField]private  UiGamePlay uiGamePlay;
    [SerializeField] private UiGameOver uiGameOver;
    [SerializeField] private UiRewive uiRewive;
    [SerializeField] private UiHomeScreen uiHomeScreen;
    [SerializeField] private UiPlayerSelection uiPlayerSelection;
    [SerializeField] private UiPurchaseScreen uiPurchaseScreen;
    [SerializeField] private CoinUI uiCoinPanel;
    [SerializeField] private UiSetingScreen uiSetingScreen;
    [SerializeField] private UiPausePanel uiPausePanel;
    [SerializeField] private UiLoadIngScreen uiLoadingScreen;
    private void Awake()
    {
        instance = this;
    }

   public UiLoadIngScreen GetUiLoadingScreen()
    {
        return uiLoadingScreen;
    }

    public UiHomeScreen GetUiHomeScreen()
    {
        return uiHomeScreen;
    }

    public UiGamePlay GetUiGamePlayScreen()
    {
        uiGamePlay.StartGamePlayUiAnimation();
        return uiGamePlay;
        
    }
    public UiGameOver GetUiGameOverScreen()
    {
        return uiGameOver;
    }
    public UiRewive GetUiReWive()
    {
        return uiRewive;
    }
    public UiPlayerSelection GetUiPlayerSelection()
    {
        return uiPlayerSelection;
    }
    public UiPurchaseScreen GetUiPurchaseScreen()
    {
        return uiPurchaseScreen;
    }
    public CoinUI GetCoinPanel()
    {
        return uiCoinPanel;
    }
    public UiSetingScreen GetUiSetingScreen()
    {
        return uiSetingScreen;
    }
    public UiPausePanel GetUiPausePanel()
    {
        return uiPausePanel;
;    }


}
