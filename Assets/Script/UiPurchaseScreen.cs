using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiPurchaseScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel_Noads;
    [SerializeField] private RectTransform[] all_product;
    [SerializeField] private RectTransform[] all_Price;
    [SerializeField] private RectTransform[] all_TxtCoin;
    [SerializeField] private float startAnimatonTime;
    [SerializeField] private float endAnimationTime;
    [SerializeField] private RectTransform panel_Coin;
    [SerializeField] private RectTransform btn_Close;

   

   

    public void StartShopAnimation()
    {
        if (DataManager.instance.hasPurchaseNoads)
        {
            panel_Noads.gameObject.SetActive(false);
        }
        else
        {
            panel_Noads.gameObject.SetActive(true);
        }
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(AllProductAnimation).AppendInterval(startAnimatonTime).
            AppendCallback(All_PriceAnimation).AppendInterval(startAnimatonTime).
            AppendCallback(All_TxtCoinAnimation).AppendInterval(startAnimatonTime).
            Append(panel_Coin.DOAnchorPos(new Vector2(0,0),startAnimatonTime)).
            Append(btn_Close.DOScale(new Vector3(1,1,1),startAnimatonTime));
    }
    private void closeUiAnimation()
    {
        for (int i = 0; i < all_product.Length; i++)
        {
            all_product[i].DOScale(new Vector3(0, 0, 0), endAnimationTime);
            all_TxtCoin[i].DOScale(new Vector3(0, 0, 0), endAnimationTime);
            all_Price[i].DOScale(new Vector3(0, 0, 0), endAnimationTime);
        }
        panel_Coin.DOAnchorPos(new Vector2(0, 250), endAnimationTime);
        btn_Close.DOScale(new Vector3(0, 0, 0), endAnimationTime);
    }
    private void AllProductAnimation()
    {
        for (int i = 0; i < all_product.Length; i++)
        {
            all_product[i].DOScale(new Vector3(1, 1, 1), startAnimatonTime);
        }
    }
    private void All_TxtCoinAnimation()
    {
        for (int i = 0; i < all_TxtCoin.Length; i++)
        {
            all_TxtCoin[i].DOScale(new Vector3(1, 1, 1), startAnimatonTime);
        }
    }
    private void All_PriceAnimation()
    {
        for (int i = 0; i < all_Price.Length; i++)
        {
            all_Price[i].DOScale(new Vector3(1, 1, 1), startAnimatonTime);
        }
    }
    public void OnCliCkOnCloseButton()
    {

        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(closeUiAnimation).AppendInterval(endAnimationTime).
            AppendCallback(CloseButonProcedure);
    }

    private void CloseButonProcedure()
    {
        this.gameObject.SetActive(false);
        Destroy(PlayerManager.Instance.Player());
        PlayerManager.Instance.StartGameManager();
    }
    public void OnClickOnAddCoinButton(int value)
    {
        AudioManager.instance.BtnSoundPlay();
       
        if (value == 250)
        {
            IAPManager.Instance.BuyConsumable(1);
        }
        else if (value == 1000)
        {
            IAPManager.Instance.BuyConsumable(2);
          
          
        }
        else if (value == 2000)
        {
            IAPManager.Instance.BuyConsumable(3);
           
          
        }
        
    }
    
    
    public void OnClickOnNoAds()
    {
        AudioManager.instance.BtnSoundPlay();
        IAPManager.Instance.BuyConsumable(0);
      
        
        panel_Noads.gameObject.SetActive(false);
       

    }
}
