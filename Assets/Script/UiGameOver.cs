using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class UiGameOver : MonoBehaviour
{
    [Header("Ui Animation Data")]
    [SerializeField] private RectTransform panel_Bg;
    [SerializeField] private RectTransform panel_Header;
    [SerializeField] private RectTransform panel_CurrentScore;
    [SerializeField] private RectTransform panel_BestScore;
    [SerializeField] private RectTransform panel_CurrentGameCoin;
    [SerializeField] private RectTransform panel_2XButton;
    [SerializeField] private RectTransform btn_Reset;
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAnimationTime;


    [Header("Score & Coin Data")]
    [SerializeField] private TextMeshProUGUI txt_Score;
    [SerializeField] private TextMeshProUGUI txt_BestScore;
    [SerializeField] private TextMeshProUGUI txt_EarnedCoin;
    [SerializeField] private Button btn2x;

    [Header("ReLoad Data")]
    [SerializeField] private GameObject container;
    [SerializeField] private Image img_Fade;
    [SerializeField] private float fadingScreenTime;


    private void Start()
    {
        if (!FindObjectOfType<AdsManager>().IsRewardLoaded())
        {
            panel_2XButton.gameObject.SetActive(false);
        }
        SetTextEarenedCoin(GameManager.instance.GetCurrentGameCoin());
        SetTextScore();
        SetBestSCore();
        StatUiAnimation();
    }

    private  void StatUiAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(panel_Bg.DOScale(new Vector3(1, 1, 1), startAnimationTime)).
            Append(panel_Header.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
            Append(panel_CurrentScore.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
            Append(panel_BestScore.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
            Append(panel_CurrentGameCoin.DOAnchorPos(new Vector2(0, 0), startAnimationTime)).
            Append(btn_Reset.DOScale(new Vector3(1, 1, 1), startAnimationTime)).
            Append(panel_2XButton.DOScale(new Vector3(1, 1, 1), startAnimationTime)).AppendCallback(FindObjectOfType<AdsManager>().ShowInterstitialAd);

    }
    private void CloseUiAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(panel_CurrentScore.DOAnchorPos(new Vector2(-1500, 0), endAnimationTime)).
            Append(panel_BestScore.DOAnchorPos(new Vector2(1500, 0), endAnimationTime)).
            Append(panel_CurrentGameCoin.DOAnchorPos(new Vector2(-1500, 0), endAnimationTime)).
            Append(btn_Reset.DOScale(new Vector3(0, 0, 0), endAnimationTime)).
            Append(panel_2XButton.DOScale(new Vector3(0, 0, 0), endAnimationTime)).
            Append(panel_Bg.DOScale(new Vector3(0, 0, 0), endAnimationTime)).
            Append(panel_Header.DOAnchorPos(new Vector2(0, 600), endAnimationTime)).
            Append(panel_Bg.DOScale(new Vector3(0,0, 0), endAnimationTime));
           
            

    }
    private void SetTextScore()
    {
        txt_Score.text = GameManager.instance.GetCuurentScore().ToString();
    }
    public void SetTextEarenedCoin(int Score)
    {
        txt_EarnedCoin.text = Score.ToString();
    }
    private     void SetBestSCore()
    {
        txt_BestScore.text = DataManager.instance.bestScore.ToString();
    }

    public void OnClick_ReLoadButton()
    {

        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiAnimation).AppendInterval(endAnimationTime*8).
            AppendCallback(ReLoadProcedure);


    }

    private void ReLoadProcedure()
    {
        container.gameObject.SetActive(false);

        Sequence seq = DOTween.Sequence();
        seq.Append(img_Fade.DOFade(1, fadingScreenTime)).AppendCallback(LoadScene);
    }
    public void OnClickOnTwoXButton()
    {
        btn2x.gameObject.SetActive(false);
        AudioManager.instance.BtnSoundPlay();
        FindObjectOfType<AdsManager>().ShowRewardAd();

    }
    private void LoadScene()
    {
        
        SceneManager.LoadScene(0);

    }

}
