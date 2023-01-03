using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private GameObject btn_Coin;
    [SerializeField] private TextMeshProUGUI txt_CoinValue;
    [SerializeField] private float currenttotalCoin = 0;
    [SerializeField] private float targetCoin = 0;
    [SerializeField] private float changeRate = 2;


   

    public GameObject GetCoinPurChaseBtn()
    {
        return btn_Coin;
    }
    public void OnCliCkOnAddCoinBtn()
    {
        UiManager.instance.GetUiHomeScreen().OnCliCkOnPurchaseScreen();
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiGameOverScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiPlayerSelection().gameObject.SetActive(false);

    }


    private void Start()
    {
        currenttotalCoin = DataManager.instance.totalCoin;
        targetCoin = DataManager.instance.totalCoin;
        txt_CoinValue.text = currenttotalCoin.ToString("F0");
    }



    private void Update()
    {
        
        if (currenttotalCoin != targetCoin)
        {
            currenttotalCoin = Mathf.Lerp(currenttotalCoin, targetCoin, changeRate * Time.deltaTime);
           

            if (targetCoin-currenttotalCoin <10)
            {
                 
                currenttotalCoin = targetCoin;
            }

            txt_CoinValue.text = currenttotalCoin.ToString("F0");
        }
    }

    public void SetTargetCoin()
    {
       
        targetCoin =  DataManager.instance.totalCoin;
    }
}
