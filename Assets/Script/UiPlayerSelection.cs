using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class UiPlayerSelection : MonoBehaviour
{
    [Header(" UIAnimation Data")]
    [SerializeField] private RectTransform[] All_PanelOfPlayer;
    [SerializeField] private RectTransform panel_Coin;
    [SerializeField] private RectTransform btn_Close;
    [SerializeField] private float startAniamtionTime;
    [SerializeField] private float endAnimationTime;

    private int currentPlayerSelectedIndex = 0;

    [Header("Player Purchase/ UpGrade Data")]
    [SerializeField] private TextMeshProUGUI[] all_TxtCoinValue;
    [SerializeField] private GameObject [] all_Buy_UpgradeButon;
    [SerializeField] private GameObject[] all_SelectedScreen;
    [SerializeField] private GameObject[] all_SelectionButton;
    [SerializeField] private GameObject[] all_LockedScreen;
    [SerializeField] private Image[] all_ImgOFPlayerIcon;
    [SerializeField] private Material mat_Sprite;
    [SerializeField] private Material mat_SelectedPlayerIcon;
   

    [Header("Player Properites Data")]
    [SerializeField] private Slider[] all_SliderHealth;
    [SerializeField] private TextMeshProUGUI[] all_TxtHealthValue;
    [SerializeField] private Slider[] all_SliderDamage;
    [SerializeField] private TextMeshProUGUI[] all_TxtDamageValue;
    [SerializeField] private Slider[] all_SliderSpeed;
    [SerializeField] private TextMeshProUGUI[] all_TxtSpeed;
    [SerializeField] private Slider[] all_Firerate;
    [SerializeField] private TextMeshProUGUI[] all_TxtFireRate;
    [SerializeField] private Slider[] all_SliderMaxJetPackTime;
    [SerializeField] private TextMeshProUGUI[] all_TxtMaxJetPackTime;
    [SerializeField] private PlayerData[] all_PlayerData;

   
    private void OnEnable()
    {
        currentPlayerSelectedIndex = PlayerManager.Instance.GetCurrentPlayerIndex();

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(SetPlayerUi).AppendCallback(StartPlayerUiAnimation).
            AppendInterval(startAniamtionTime).
            Append(panel_Coin.DOAnchorPos(new Vector2(0, 0), startAniamtionTime)).
            Append(btn_Close.DOScale(new Vector3(1,1,1),startAniamtionTime));
    }

    public  void StartPlayerUiAnimation()
    {
        for (int i = 0; i < All_PanelOfPlayer.Length; i++)
        {
            All_PanelOfPlayer[i].DOScale(new Vector3(1, 1, 1), startAniamtionTime);
        }
    }

    public  void ClosePLayerUIAnimation()
    {
        for (int i = 0; i < All_PanelOfPlayer.Length; i++)
        {
            All_PanelOfPlayer[i].DOScale(new Vector3(0, 0, 0), endAnimationTime);
        }
        panel_Coin.DOAnchorPos(new Vector2(0, 250), endAnimationTime);
        btn_Close.DOScale(new Vector3(1, 1, 1), endAnimationTime);
    }
   

    private void SetPlayerUi()
    {
       
        for (int i = 0; i < all_PlayerData.Length; i++)
        {
          //  SetValueSlider(i);
             all_SliderHealth[i].value = all_PlayerData[i].CurrentVertionHealth();
            all_TxtHealthValue[i].text = all_PlayerData[i].CurrentVertionHealth().ToString();
            all_SliderDamage[i].value = all_PlayerData[i].CurrentVertionDamage();
            all_TxtDamageValue[i].text = all_PlayerData[i].CurrentVertionDamage().ToString();
            all_SliderSpeed[i].value = all_PlayerData[i].CurrentVertionSpeed();
            all_TxtSpeed[i].text = all_PlayerData[i].CurrentVertionSpeed().ToString();
            all_Firerate[i].value = (all_Firerate[i].maxValue - all_PlayerData[i].CurrentVertionFireRate() ) * 1.24f;  ///  two point to line equation;
            all_TxtFireRate[i].text = all_PlayerData[i].CurrentVertionFireRate().ToString();
            all_SliderMaxJetPackTime[i].value = all_PlayerData[i].CurrentVertionMaxJetPackFuel();
            all_TxtMaxJetPackTime[i].text = all_PlayerData[i].CurrentVertionMaxJetPackFuel().ToString();
            all_TxtCoinValue[i].text = all_PlayerData[i].CurrentCoinShowInScreen().ToString();
            all_LockedScreen[i].gameObject.SetActive(!all_PlayerData[i].GetUnLockStatus());
            all_SelectionButton[i].gameObject.SetActive(all_PlayerData[i].GetUnLockStatus());

            if (all_PlayerData[i].GetPlayerCurrentLevel() == 9)
            {
                all_Buy_UpgradeButon[i].gameObject.SetActive(false);
            }
         
            if (i == currentPlayerSelectedIndex)
            {
                all_SelectedScreen[i].gameObject.SetActive(true);
                all_ImgOFPlayerIcon[i].material = mat_SelectedPlayerIcon;
            }
            else
            {
                all_SelectedScreen[i].gameObject.SetActive(false);
            }
           
        }
    }

    private void SetValueSlider(int i)
    {
        all_SliderHealth[i].minValue = 50;
        all_SliderHealth[i].maxValue = 1000;
        all_SliderDamage[i].minValue = 30;
        all_SliderDamage[i].maxValue = 135;
        all_SliderSpeed[i].minValue = 4;
        all_SliderSpeed[i].maxValue = 9;
        all_Firerate[i].minValue = 0.2f;
        all_Firerate[i].maxValue = 1.5f;
        all_SliderMaxJetPackTime[i].minValue = 4.5f;
        all_SliderMaxJetPackTime[i].maxValue = 10;

    }
    public void OnCliCk_CloseButonClick()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(ClosePLayerUIAnimation).AppendInterval(endAnimationTime).
            AppendCallback(CloseButonProcedure);
        
    }
    private void CloseButonProcedure()
    {
        this.gameObject.SetActive(false);
        Destroy(PlayerManager.Instance.Player());
        PlayerManager.Instance.StartGameManager();
    }

    public void OnClick_OnPlayerSelection(int Index)
    {
        Debug.Log(Index);
       
        AudioManager.instance.BtnSoundPlay();
        if (currentPlayerSelectedIndex==Index)
        {
            return;
        }
        all_SelectedScreen[currentPlayerSelectedIndex].gameObject.SetActive(false);
        all_ImgOFPlayerIcon[currentPlayerSelectedIndex].material = mat_Sprite;
        PlayerManager.Instance.SetPlayerIndex(Index);
        currentPlayerSelectedIndex = Index;
        Destroy(PlayerManager.Instance.Player());
        PlayerManager.Instance.SetPlayer();
        PlayerManager.Instance.Player().gameObject.SetActive(false);
        DataManager.instance.SetCurrentPlayerIndex(Index);
        all_SelectedScreen[Index].gameObject.SetActive(true);
        all_ImgOFPlayerIcon[Index].material = mat_SelectedPlayerIcon;
        
    }

   

    public void OnCliCk_BuyOrUpGradeButton( int index)
    {
        AudioManager.instance.BtnSoundPlay();
        if (DataManager.instance.totalCoin>= all_PlayerData[index].CurrentCoinShowInScreen())
        {
            if (!all_PlayerData[index].GetUnLockStatus())
            {
                DataManager.instance.SubTractCoin(all_PlayerData[index].CurrentCoinShowInScreen());
                all_PlayerData[index].SetUnlockStatus(true);
                DataManager.instance.SetCurrentPlayerUnlockedStatus(index);
                SetPlayerPanel(index);
            }
            else if (all_PlayerData[index].GetPlayerCurrentLevel()<9)
            {
                DataManager.instance.SubTractCoin(all_PlayerData[index].CurrentCoinShowInScreen());
                all_PlayerData[index].SetCurrentVertionofUpGrade(all_Buy_UpgradeButon[index]);
                SetPlayerPanel(index);
                DataManager.instance.SetCurrentPlayerLevel(index, all_PlayerData[index].GetPlayerCurrentLevel());

            }
           
        }
        
    }

    private void  SetPlayerPanel(int index)
    {
        all_SliderHealth[index].value = all_PlayerData[index].CurrentVertionHealth();
        all_TxtHealthValue[index].text = all_PlayerData[index].CurrentVertionHealth().ToString();
        all_SliderDamage[index].value = all_PlayerData[index].CurrentVertionDamage();
        all_TxtDamageValue[index].text = all_PlayerData[index].CurrentVertionDamage().ToString();
        all_SliderSpeed[index].value = all_PlayerData[index].CurrentVertionSpeed();
        all_TxtSpeed[index].text = all_PlayerData[index].CurrentVertionSpeed().ToString();
        all_Firerate[index].value = (all_Firerate[index].maxValue - all_PlayerData[index].CurrentVertionFireRate())*1.24f;
        all_TxtFireRate[index].text = all_PlayerData[index].CurrentVertionFireRate().ToString();
        all_SliderMaxJetPackTime[index].value = all_PlayerData[index].CurrentVertionMaxJetPackFuel();
        all_TxtMaxJetPackTime[index].text = all_PlayerData[index].CurrentVertionMaxJetPackFuel().ToString();
        all_TxtCoinValue[index].text = all_PlayerData[index].CurrentCoinShowInScreen().ToString();
        all_LockedScreen[index].gameObject.SetActive(!all_PlayerData[index].GetUnLockStatus());
        all_SelectionButton[index].gameObject.SetActive(all_PlayerData[index].GetUnLockStatus());

    }

   
    

}
