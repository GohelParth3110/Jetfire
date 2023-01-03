using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private  PlayerPrefSKey userKeys = new PlayerPrefSKey();
    [SerializeField] private GameObject loadingpanel;

    [SerializeField] private PlayerData[] all_PlayerData;
    public int totalCoin;
    public int bestScore;
    public int currentPlayerIndex;
    public int currentPlayerLevel;
    public int musicValue;
    public int soundValue;
    public bool hasPurchaseNoads;
    public bool isfirsttimeStart = true;

   

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        
        if (PlayerPrefs.HasKey(userKeys.key_TotalCoins))
        {
            GetUserData();
        }
        else
        {
            SaveFirstTimeData();
        }


        SetPlayerForStartGame();


    }

    private void SetPlayerForStartGame()
    {
        if (FindObjectOfType<AdsManager>().isFirstTime)
        {
            loadingpanel.gameObject.SetActive(true);
            FindObjectOfType<AdsManager>().isFirstTime = false;
        }
        else
        {
           
            UiManager.instance.GetUiLoadingScreen().LoadScene();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    
    private void SaveFirstTimeData()
    {
        PlayerPrefs.SetInt(userKeys.Key_Noads, 0);

        for (int i = 0; i < all_PlayerData.Length; i++)
        {
            PlayerPrefs.SetInt(userKeys.key_CurrentPlayerLevel + i, 0);
            if (all_PlayerData[i].GetUnLockStatus())
            {
                PlayerPrefs.SetInt(userKeys.key_UnLockedPlayer + i, 1);
               
            }
            else
            {
                PlayerPrefs.SetInt(userKeys.key_UnLockedPlayer + i, 0);
            }
        }

        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoin);
        PlayerPrefs.SetInt(userKeys.key_BestScore, bestScore);
        PlayerPrefs.SetInt(userKeys.key_CurrentPlayerSelectedIndex, currentPlayerIndex);
        PlayerPrefs.SetInt(userKeys.key_Music, 1);
        PlayerPrefs.SetInt(userKeys.key_Sound, 1);


        FindObjectOfType<AdsManager>().CallDefaultAds();
      
    }

    private void GetUserData()
    {
        totalCoin = PlayerPrefs.GetInt(userKeys.key_TotalCoins);
        bestScore = PlayerPrefs.GetInt(userKeys.key_BestScore);
        currentPlayerIndex = PlayerPrefs.GetInt(userKeys.key_CurrentPlayerSelectedIndex);
        musicValue = PlayerPrefs.GetInt(userKeys.key_Music);
        soundValue = PlayerPrefs.GetInt(userKeys.key_Sound);

        if (PlayerPrefs.GetInt(userKeys.Key_Noads) ==1)
        {
            hasPurchaseNoads = true;
        }
        else
        {
            hasPurchaseNoads = false;
            FindObjectOfType<AdsManager>().CallDefaultAds();
        }

        for (int i = 0; i < all_PlayerData.Length; i++)
        {
            all_PlayerData[i].SetCurrentLevelForStartGame(PlayerPrefs.GetInt((userKeys.key_CurrentPlayerLevel + i)));

            if (PlayerPrefs.GetInt(userKeys.key_UnLockedPlayer + i) == 0)
            {
                all_PlayerData[i].SetUnlockStatus(false);
            }
            else
            {
                all_PlayerData[i].SetUnlockStatus(true);
            }
        }

        

    }

    public void AddCoin(int AddCoin)
    {
        totalCoin += AddCoin;
        UiManager.instance.GetCoinPanel().SetTargetCoin();
        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoin);
    }
    public void SubTractCoin(int SubTractCoin)
    {
        
        totalCoin -= SubTractCoin;
        UiManager.instance.GetCoinPanel().SetTargetCoin();
        PlayerPrefs.SetInt(userKeys.key_TotalCoins, totalCoin);
    }

    public void SetBestScore( int Score)
    {
        if (Score>bestScore)
        {
            bestScore = Score;
            PlayerPrefs.SetInt(userKeys.key_BestScore, bestScore);
        }
       
    }

    public void SetNoads()
    {
        hasPurchaseNoads = true;
        PlayerPrefs.SetInt(userKeys.Key_Noads, 1);
    }
    public void SetMusicValue(bool cuurent)
    {
        if (cuurent)
        {
            PlayerPrefs.SetInt(userKeys.key_Music, 1);
            musicValue = 1;
        }
        else
        {
            PlayerPrefs.SetInt(userKeys.key_Music, 0);
            musicValue = 0;
        }
    }
    public void SetSoundValue(bool cuurent)
    {
        if (cuurent)
        {
            PlayerPrefs.SetInt(userKeys.key_Sound, 1);
            soundValue = 1;
        }
        else
        {
            PlayerPrefs.SetInt(userKeys.key_Sound, 0);
            soundValue = 0;
        }
    }
    public void SetCurrentPlayerLevel(int index, int value)
    {
        PlayerPrefs.SetInt(userKeys.key_CurrentPlayerLevel + index, value);
       
    }
    public void SetCurrentPlayerIndex(int Index)
    {
        PlayerPrefs.SetInt(userKeys.key_CurrentPlayerSelectedIndex, Index);
       
    }
    public void SetCurrentPlayerUnlockedStatus(int index)
    {
        PlayerPrefs.SetInt(userKeys.key_UnLockedPlayer + index, 1);

       
    }
}
