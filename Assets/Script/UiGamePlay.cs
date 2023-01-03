using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiGamePlay : MonoBehaviour
{
    [Header("UiGamePlayAnimation SetUp")]
    
    [SerializeField] private RectTransform panel_HealthBar;
    [SerializeField] private RectTransform panel_JetBar;
    [SerializeField] private RectTransform btn_PlayorPause;
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAniamtionTime;
    [SerializeField] private float startLevelUpAniamtion;
    [SerializeField] private float waitLevelUpAniamtion;
    [SerializeField] private float endLevelUpAnimation;
    [Header("uiGamePlaySceen Setup")]
    [SerializeField] private Slider slider_Health;
    [SerializeField] private Slider slider_Jet;
    [SerializeField] private TextMeshProUGUI txt_ScoreValue;
    [SerializeField] private TextMeshProUGUI txt_CoinValue;
    [SerializeField] private float CompletejetSliderFillupTime;
    [SerializeField] private float CompletejetSliderFillDownTime;
    [SerializeField] private float changerateSlider;
    [SerializeField] private bool isPause = false;
    [SerializeField] private GameObject panel_Pause;
    [SerializeField] private GameObject panel_Levelup;

    public bool isleftbuttonClick;
    public bool isRightbuttonClick;
    public bool isJetButtonClick;
    private void OnEnable()
    {
        
        StartGamePlayUiAnimation();
    }
    private void Start()
    {
        slider_Health.maxValue = PlayerManager.Instance.SetPlayerHelth();
        slider_Health.value = slider_Health.maxValue;
       
    }
    public void StartGamePlayUiAnimation()
    {
        
        panel_HealthBar.DOAnchorPos(new Vector2(0, 0), startAnimationTime);
        panel_JetBar.DOAnchorPos(new Vector2(0, 0), startAnimationTime);
        btn_PlayorPause.DOAnchorPos(new Vector2(0, 0), startAnimationTime);
    }
    private void CloseGamePlayUiAnimation()
    {
       // panelCoin.DOAnchorPos(new Vector2(0, 250), endAniamtionTime);
        panel_HealthBar.DOAnchorPos(new Vector2(0, 278), endAniamtionTime);
        panel_JetBar.DOAnchorPos(new Vector2(0, 278), endAniamtionTime);
        btn_PlayorPause.DOAnchorPos(new Vector2(0, 278), endAniamtionTime);
    }

    public Slider GetSlider_Jet()
    {
        return slider_Jet;
    }
    
    public void UpdateHealthSlider(float currentHealthValue)
    {
        slider_Health.value = currentHealthValue;
    }

    public void UpdateJetpackFuelSlider(float currentJetpackValue)
    {
        slider_Jet.value = currentJetpackValue;
    }

    public void UpdateScoreText(int currentScore)
    {
        txt_ScoreValue.text = currentScore.ToString();
    }
   
    public void OnclickOnPauseButton()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseGamePlayUiAnimation).AppendInterval(endAniamtionTime).
            AppendCallback(OnClickPauseButtonProcedure);

    }
    private void OnClickPauseButtonProcedure()
    {

        UiManager.instance.GetUiGamePlayScreen().gameObject.SetActive(false);
        UiManager.instance.GetUiPausePanel().gameObject.SetActive(true);
        UiManager.instance.GetUiPausePanel().StratUiPausePanelAnimation();
       
        GameManager.instance.SetIsPlayerLive(false);
    }

    public void LeftButtonPressDown()
    {
        isleftbuttonClick = true;
       
    }
    public void LeftButtonPressUP()
    {
        isleftbuttonClick = false;
       
    }
    public void RightButtonPressDown()
    {
        isRightbuttonClick = true;

    }
    public void RightButtonPressUP()
    {
        isRightbuttonClick = false;

    }
    public void JetButtonPressDown()
    {
        isJetButtonClick = true;

    }
    public void JetButtonPressUP()
    {
        isJetButtonClick = false;

    }

    public void LevelUpPanelAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(panel_Levelup.transform.DOScale(new Vector3(1, 1, 1), startAnimationTime)).
            AppendInterval(waitLevelUpAniamtion).
            Append(panel_Levelup.transform.DOScale(new Vector3(0, 0, 0), endAniamtionTime));
       
    }
   

}
