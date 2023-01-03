using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float rocketSpeed = 5;
    [SerializeField] private float[] all_Rocketdamage;
    [SerializeField] private ParticleSystem rocketVFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rocketSpawnSFX;
    [SerializeField] private AudioClip rocketExplotionSFX;
    [SerializeField] private GameObject bodyRocket;
    private bool isStopMove = false;
   
    private float damage;

    // tags
    private string tag_Boundry = "Boundry";
    private string tag_Enemy = "Enemy";
    private string tag_Player = "PlayerBody";
    private string tag_PickUp = "PickUp";
    private string tag_Spike = "Spike";
    private string tag_Path = "Path";

    private void Start()
    {
       
        damage = all_Rocketdamage[GameManager.instance.GetCurrentGameLevel()];
        HandleAudioSource(rocketSpawnSFX);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isStopMove)
        {
            return;
        }
        transform.Translate(Vector3.left * Time.deltaTime * rocketSpeed);      
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (HasHitIgnoredObjects(other.gameObject.tag))
        {
            return;
        }

        if (other.gameObject.CompareTag(tag_Player))
        {

         Playerhealth playerhealth =    other.GetComponentInParent<Playerhealth>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject,2);
        bodyRocket.SetActive(false);
        isStopMove = true;
        if (rocketVFX != null)
        {
            rocketVFX.Play();
            HandleAudioSource(rocketExplotionSFX);
            
        }
       
        
    }
    private void HandleAudioSource(AudioClip audioClip)
    {
        if (DataManager.instance.soundValue == 0)
        {
            return;
        }
        audioSource.PlayOneShot(audioClip, 1);
    }
    private bool HasHitIgnoredObjects(string objectTag)
    {
        if (objectTag.Equals(tag_Boundry) || objectTag.Equals(tag_PickUp) || objectTag.Equals(tag_Enemy) || objectTag.Equals(tag_Spike)
             || objectTag.Equals(tag_Path))
        {
            return true;
        }

        return false;
    }
}
