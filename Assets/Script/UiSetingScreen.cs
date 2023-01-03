using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiSetingScreen : MonoBehaviour
{
    [SerializeField] private RectTransform panel_BG;
    [SerializeField] private RectTransform panel_Music;
    [SerializeField] private RectTransform panel_Sound;
    [SerializeField] private RectTransform panel_ResetButton;
    [SerializeField] private RectTransform panel_Header;
    [SerializeField] private RectTransform panel_Rate;
    [SerializeField] private float startAnimationTime;
    [SerializeField] private float endAnimationTime;
    [SerializeField] private bool isMusicON;
    [SerializeField] private bool isSoundON;

    [SerializeField] private GameObject img_SoundTick;
    [SerializeField] private GameObject img_MusicTick;
    private void OnEnable()
    {
        SetMusicvalue();
        SetSoundValue();
        StartUiSetingAnimtion();
    }

    private void SetMusicvalue()
    {
        if (DataManager.instance.musicValue==1)
        {
            img_MusicTick.gameObject.SetActive(true);
            isMusicON = true;
        }
        else
        {
            isMusicON = false;
            img_MusicTick.gameObject.SetActive(false);
        }
        
    }
    private void SetSoundValue()
    {
        if (DataManager.instance.soundValue == 1)
        {
            img_SoundTick.gameObject.SetActive(true);
            isSoundON = true;
        }
        else
        {
            img_SoundTick.gameObject.SetActive(false);
            isSoundON = false;
        }
       
    }
    private void StartUiSetingAnimtion()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(panel_BG.DOScale(new Vector3(1, 1, 1), startAnimationTime)).
            Append(panel_Header.DOScale(new Vector3(1, 1, 1), startAnimationTime)).
            AppendCallback(StartButtonAnimation);
    }

    private void StartButtonAnimation()
    {
        panel_Music.DOAnchorPos(new Vector2(0,0), startAnimationTime);
        panel_Sound.DOAnchorPos(new Vector2(0, 0), startAnimationTime);
        panel_Rate.DOScale(new Vector3(1, 1, 1), startAnimationTime);
        panel_ResetButton.DOScale(new Vector3(1, 1, 1), startAnimationTime);
    }

    public void OnClick_ReSetButon()
    {
        AudioManager.instance.BtnSoundPlay();
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(CloseUiAnimation).AppendInterval(endAnimationTime*3).
            AppendCallback(ResetButonProcedure);

    }
    private void ResetButonProcedure()
    {
        this.gameObject.SetActive(false);
        Destroy(PlayerManager.Instance.Player());
        PlayerManager.Instance.StartGameManager();
    }
    private void CloseUiAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(StartCloseButonUi).AppendInterval(endAnimationTime).
            Append(panel_BG.DOScale(new Vector3(0, 0, 0), endAnimationTime)).
            Append(panel_Header.DOScale(new Vector3(0, 0, 0), endAnimationTime));
           
    }
    private void StartCloseButonUi()
    {
        panel_Music.DOAnchorPos(new Vector2(-1500, 0), endAnimationTime);
        panel_Sound.DOAnchorPos(new Vector2(1500, 0), endAnimationTime);
        panel_Rate.DOScale(new Vector3(0, 0, 0), endAnimationTime);
        panel_ResetButton.DOScale(new Vector3(0, 0, 0), endAnimationTime);
    }

    public void Onclick_OnMusicValue()
    {
        AudioManager.instance.BtnSoundPlay();
        if (isMusicON)
        {
            img_MusicTick.gameObject.SetActive(false);
            isMusicON = false;
            DataManager.instance.SetMusicValue(false);
        }
        else
        {
            img_MusicTick.gameObject.SetActive(true);
            isMusicON = true;
            DataManager.instance.SetMusicValue(true);
        }
    }
    public void OnClick_OnSoundValue()
    {
        AudioManager.instance.BtnSoundPlay();
        if (isSoundON)
        {
            img_SoundTick.gameObject.SetActive(false);
            isSoundON = false;
            DataManager.instance.SetSoundValue(false);

        }
        else
        {
            img_SoundTick.gameObject.SetActive(true);
            isSoundON = true;
            DataManager.instance.SetSoundValue(true);
        }
    }
}
