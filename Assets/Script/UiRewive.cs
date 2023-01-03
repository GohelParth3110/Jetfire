using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiRewive : MonoBehaviour
{
    [SerializeField] private Image img_ReWive;
    [SerializeField] private float maxRewiveScreenTime;
    [SerializeField] private float currentReWiveScreenTime;
    [SerializeField] private bool isReviveScreeOn;
    [SerializeField] private TextMeshProUGUI txt_Counter;
    [SerializeField] private Transform transform_BoundryGround;
    [SerializeField] private float flt_SetOffsetForBoundry;

    private void Start()
    {
        if (!FindObjectOfType<AdsManager>().IsRewardLoaded())
        {
            
            UiManager.instance.GetUiGameOverScreen().gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        HandlerReWiveScreen();
    }
    private void HandlerReWiveScreen()
    {
        if (!isReviveScreeOn)
        {
            currentReWiveScreenTime = maxRewiveScreenTime;
            return;
        }
        currentReWiveScreenTime -= Time.deltaTime;
        img_ReWive.fillAmount -=  Time.deltaTime/maxRewiveScreenTime;
        txt_Counter.text = currentReWiveScreenTime.ToString("F0");
        if (img_ReWive.fillAmount<=0)
        {
            currentReWiveScreenTime = maxRewiveScreenTime;
            isReviveScreeOn = false;
            GameManager.instance.GameOver();
        }
    }
    public void OnClickOnRewiveButton()
    {
        AudioManager.instance.BtnSoundPlay();
        FindObjectOfType<AdsManager>().ShowRewardAd();
        this.gameObject.SetActive(false);
       

    }  
    
    public void ProcessOFReWiveAds()
    {
        PlayerManager.Instance.Player().GetComponent<Playerhealth>().RevivePlayer();
        GameManager.instance.SetIsPlayerLive(true);
        PlayerManager.Instance.Player().GetComponent<Playerhealth>().SetCurrentHealth();
        UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(true);
        UiManager.instance.GetUiGamePlayScreen().isJetButtonClick = false;
        UiManager.instance.GetUiGamePlayScreen().isleftbuttonClick = false;
        UiManager.instance.GetUiGamePlayScreen().isRightbuttonClick = false;
        transform_BoundryGround.position = PlayerManager.Instance.Player().transform.position - new Vector3(flt_SetOffsetForBoundry,0,0);
        PlayerManager.Instance.Player().gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        
    }

}
