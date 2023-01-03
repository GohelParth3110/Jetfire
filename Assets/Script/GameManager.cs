using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static  GameManager instance;

    [Header("Game Information")]
    [SerializeField] private float[] totalGameLevel;
    [SerializeField] private int currentGameLevel = 0;
    [SerializeField] private int currentGameScore = 0;
    [SerializeField] private int CurrentGameCoin;
    [SerializeField] private PickUpPoolManager pickUpPoolManager;
   
    [SerializeField] private bool isPlayerTakeRewive = false;
    [SerializeField] private bool isplayerAlive = false;
    [SerializeField] private ParticleSystem Partical_LevelVfX;

   
   
   

    public PickUpPoolManager GetPickupPoolManager()
    {
        return pickUpPoolManager;
    }

  
    private void Awake()
    {
        instance = this;
    }

    public bool GetIsPlayerLive()
    {
        return isplayerAlive;
    }
    public void SetIsPlayerLive(bool value)
    {
        isplayerAlive = value;
    }


    public void UpdateScore( int value)
    {
        currentGameScore += value;

        DataManager.instance.SetBestScore(currentGameScore);
        UiManager.instance.GetUiGamePlayScreen().UpdateScoreText(currentGameScore);
        if (currentGameLevel == totalGameLevel.Length - 1)
        {
            return;
        }

        if (currentGameScore > totalGameLevel[currentGameLevel])
        {
            currentGameLevel++;
            Partical_LevelVfX.Play();
            UiManager.instance.GetUiGamePlayScreen().LevelUpPanelAnimation();

        }
    }
    public int GetCuurentScore()
    {
        return currentGameScore;
    }

    public void UpdateCoin()
    {
        CurrentGameCoin++;
        DataManager.instance.AddCoin(1);

    }

    public int GetCurrentGameCoin()
    {
        return CurrentGameCoin;
    }
   


    public void GameOver()
    {
        GameManager.instance.SetIsPlayerLive(false);
        if (!isPlayerTakeRewive)
        {
            
                UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(false);
                UiManager.instance.GetUiReWive().gameObject.SetActive(true);
                UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(false);
                isPlayerTakeRewive = true;
            
           
           
        }
        else
        {
            UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(false);
            UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(false);
            UiManager.instance.GetUiReWive().gameObject.SetActive(false);
            UiManager.instance.GetUiGameOverScreen().gameObject.SetActive(true);
            PlayerManager.Instance.Player().SetActive(false);
            isPlayerTakeRewive = false;
        }
        

    }

    public int GetCurrentGameLevel()
    {
        return currentGameLevel;
    }

}
