using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource butonClick;
    [SerializeField] private AudioSource audio_Music;
    [SerializeField] private AudioSource audio_Pickup;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayBackgroundMusic()
    {
        audio_Music.Play();
    }

    public void StopBackgroundMusic()
    {
        audio_Music.Stop();
    }

    public void BtnSoundPlay()
    {
        if (DataManager.instance.soundValue==0)
        {
            return;
        }
        butonClick.Play();
    }

    public void PlayPickUpSound(AudioClip pickupClip)
    {
        if (DataManager.instance.soundValue == 0)
        {
            return;
        }

        audio_Pickup.PlayOneShot(pickupClip, 1);
    }

}
