using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiHomeScreen : MonoBehaviour
{
    [SerializeField] private RectTransform btn_Play;
    [SerializeField] private RectTransform btn_Riders;
    [SerializeField] private RectTransform btn_Shop;
    [SerializeField] private RectTransform btn_Seting;
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAnimationTime;
    [SerializeField] private RectTransform panel_Coin;


    private void OnEnable()
    {
  
        UiHomeScreenAnimation();
    }

    public void UiHomeScreenAnimation()
    {
       
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(StartScreenSetUp).Append(btn_Play.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
        Append(btn_Riders.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
        Append(btn_Shop.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
        Append(btn_Seting.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
        Append(panel_Coin.DOAnchorPos(new Vector2(0, 0), startAnimationTime));
            
    }
    private void CloseUiHomeScreen()
    {

        btn_Play.DOAnchorPos(new Vector2(-2000, 0),endAnimationTime);
        btn_Riders.DOAnchorPos(new Vector2(950, 0),endAnimationTime);
        btn_Shop.DOAnchorPos(new Vector2(-2000, 0),endAnimationTime);
        btn_Seting.DOAnchorPos(new Vector2(950, 0),endAnimationTime);
        panel_Coin.DOAnchorPos(new Vector2(0, 250),endAnimationTime);
    }
    private void StartScreenSetUp()
    {
        GameManager.instance.SetIsPlayerLive(false);
      
        UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(true);
    }
    public void OnClickonPlayButton()
    {

        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiHomeScreen).AppendInterval(endAnimationTime).AppendCallback(OnclickPlayButtonProcedure);
      
    }
    private void OnclickPlayButtonProcedure()
    {
       
        panel_Coin.DOAnchorPos(new Vector2(0, 0),endAnimationTime);
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(false);
        GameManager.instance.SetIsPlayerLive(true);
        PlayerManager.Instance.Player().SetActive(true);
        UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(true);
        UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(false);
       
    }
    public void OnCliCkOnPlayerSelectionScreen()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiHomeScreen).
            AppendInterval(endAnimationTime).AppendCallback(OnClickPLayerSelectionButtonProcedure);
        
    }

    private void OnClickPLayerSelectionButtonProcedure()
    {
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiPlayerSelection().gameObject.SetActive(true);
        UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(true);
    }
    public void OnCliCkOnPurchaseScreen()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiHomeScreen).AppendInterval(endAnimationTime).
            AppendCallback(OnclickPurchaseButoonClickProcedure);
       
    }
    private void OnclickPurchaseButoonClickProcedure()
    {
        
        UiManager.instance.GetUiPurchaseScreen().gameObject.SetActive(true);
        UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(false);
        UiManager.instance.GetUiPurchaseScreen().StartShopAnimation();
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(false);

    }
    public void OnClickOnSetingButton()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiHomeScreen).AppendInterval(endAnimationTime).
            AppendCallback(SetingBtnProcedure);
    }
    private void SetingBtnProcedure()
    {
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiSetingScreen().gameObject.SetActive(true);
        UiManager.instance.GetCoinPanel().GetCoinPurChaseBtn().gameObject.SetActive(true);
    }
}
