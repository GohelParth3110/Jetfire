using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb_Bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float[] allBulletToPlayerDamage;
    private float playerDamage;
    private Vector3 shootDir;

    [SerializeField] private ParticleSystem bulletVFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletSpawnSFX;
    [SerializeField] private AudioClip bulletExplotionSFX;
    [SerializeField] private GameObject bodyRocket;
   
    [SerializeField] private GameObject bodyBullet;
    private bool isStopBullet = false;

    private string tag_Boundry = "Boundry";
     private string tag_Enemy = "Enemy";
    private string tag_Path = "Path";
    private string tag_Player = "PlayerBody";
    private string tag_PickUp = "PickUp";
    private string tag_Spike = "Spike";

    public void SetBulletDirection(Vector3 forwarDir)
    {
        shootDir = forwarDir;

    }

    private void Start()
    {
        playerDamage = allBulletToPlayerDamage[GameManager.instance.GetCurrentGameLevel()];
        HandleAudioSource(bulletSpawnSFX);
       
    }

    private void FixedUpdate()
    {
        if (isStopBullet)
        {
            return;
        }
        rb_Bullet.velocity = shootDir * bulletSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (HasHitIgnoredObjects(other.gameObject.tag))
        {
            return;
        }
        else if (other.gameObject.CompareTag(tag_Player))
        {
            Playerhealth playerhealth = other.GetComponentInParent<Playerhealth>();
            if (playerhealth != null)
            {
                playerhealth.TakeDamage(playerDamage);
            }

            
        }
       
        isStopBullet = true;
        rb_Bullet.velocity = new Vector3(0, 0, 0);
        bodyBullet.SetActive(false);
        Destroy(gameObject,2);
        if (bulletVFX!=null)
        {
            bulletVFX.Play();
            HandleAudioSource(bulletExplotionSFX);
           
           
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
        if (objectTag.Equals(tag_Boundry) || objectTag.Equals(tag_PickUp) || objectTag.Equals(tag_Path) || objectTag.Equals(tag_Enemy)
            || objectTag.Equals(tag_Spike))
        {
            return true;
        }

        return false;
    }


}
