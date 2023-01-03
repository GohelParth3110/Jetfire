using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiPausePanel : MonoBehaviour
{
    
    [SerializeField] private RectTransform btn_Play;
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAniamtionTime;
    [SerializeField] private TextMeshProUGUI txt_Counter;
    [SerializeField] private float maxCounterTime = 3;
    private float currentCounterTime;
    
    [SerializeField] private bool isCounterStart = false;   

    
    private void OnEnable()
    {
        StratUiPausePanelAnimation();
    }

    private void Update()
    {
        startCounting();
    }
    public void StratUiPausePanelAnimation()
    {
        UiManager.instance.GetCoinPanel().gameObject.SetActive(false);
        btn_Play.DOScale(new Vector3(1, 1, 1), startAnimationTime);
    }
    private void CloseUiAnimation()
    {
        
        btn_Play.DOScale(new Vector3(0, 0, 0), endAniamtionTime);
    }    
    private void startCounting()
    {
        if (!isCounterStart)
        {
           
            return;
        }

        currentCounterTime -=   Time.deltaTime;
        txt_Counter.text = ((int)currentCounterTime).ToString();
        if (currentCounterTime < 1)
        {
            currentCounterTime = maxCounterTime;
            txt_Counter.text = (currentCounterTime-1).ToString();
            txt_Counter.gameObject.SetActive(false);
            isCounterStart = false;
            UiManager.instance.GetUiPausePanel().gameObject.SetActive(false);
            
            GameManager.instance.SetIsPlayerLive(true);
            UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(true);
            UiManager.instance.GetCoinPanel().gameObject.SetActive(true);
            UiManager.instance.GetUiGamePlayScreen().StartGamePlayUiAnimation();
        }
    }

    public void OnClick_PlayButton()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiAnimation).AppendInterval(endAniamtionTime).
            AppendCallback(OnClick_PlayButtonProcedure);
    }
    private void OnClick_PlayButtonProcedure()
    {
       
        txt_Counter.gameObject.SetActive(true);
        currentCounterTime = maxCounterTime;
        isCounterStart = true;
        
    }

   
    
}
