using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb_Bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float[] allBulletToEnemyDamage;
    [SerializeField] private ParticleSystem maxDamageEffect;
   
    private float bulletDamage;
    private Vector3 shootDir;

    [SerializeField] private ParticleSystem bulletVFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip BulletSpawn;
    [SerializeField] private AudioClip bulletDistroy;
    [SerializeField] private GameObject bodyBullet;
    [SerializeField] private bool isBulletTrigger = false;
    [SerializeField] private float currentBulletTime = 0;
    [SerializeField] private float normalMaxBulletTime = 5;
    [SerializeField] private float SpecialCaseBulletTime = 10;

    private string tag_Boundry = "Boundry";
     private string tag_Enemy = "Enemy";
    private string tag_Path = "Path";
    private string tag_Player = "PlayerBullet";
    private string tag_PickUp = "PickUp";
    private string tag_Spike = "Spike";
    private string tag_Missile = "Missile";
   [Header("PowerUpPickup")]
   
  
    private bool IsStopBullet = false;

    private void Start()
    {
        HandleSoundEffect(BulletSpawn);
       
        if (isBulletTrigger)
        {
            Destroy(gameObject, SpecialCaseBulletTime);
            
        }
    }




    public void SetBulletData(Vector3 forwarDir, float damage,bool isDamageMax)
    {
        shootDir = forwarDir;
        bulletDamage = damage;
      
        if (isDamageMax)
        {
            maxDamageEffect.gameObject.SetActive(true);
            maxDamageEffect.Play();
        }
        else
        {
            maxDamageEffect.gameObject.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (!isBulletTrigger)
        {
            currentBulletTime += Time.deltaTime;
            if (currentBulletTime>normalMaxBulletTime)
            {
                HandleSoundEffect(bulletDistroy);
                Destroy(gameObject);
                
            }
        }
    }


    private void FixedUpdate()
    {
        if (IsStopBullet)
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

       
       else if (other.gameObject.CompareTag(tag_Enemy))
        {
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
            }
           
        }
        else if (other.gameObject.CompareTag(tag_Missile))
        {
            GameManager.instance.UpdateScore(GameManager.instance.GetCurrentGameLevel());

        }
        if (!isBulletTrigger)
        {
            if (other.gameObject.CompareTag("PlayerBody"))
            {
                return;
            }
            
            Destroy(gameObject, 2);
            IsStopBullet = true;
            rb_Bullet.velocity = Vector3.zero;
            bodyBullet.SetActive(false);
            bulletVFX.Play();
            HandleSoundEffect(bulletDistroy);

        }      
    }


    private void HandleSoundEffect(AudioClip clip)
    {
        if (DataManager.instance.soundValue == 1)
        {
            audioSource.PlayOneShot(clip, 1);
        }
    }

    private bool HasHitIgnoredObjects(string objectTag)
    {
        if (objectTag.Equals(tag_Boundry) || objectTag.Equals(tag_PickUp) || objectTag.Equals(tag_Path) || objectTag.Equals(tag_Player)
            || objectTag.Equals(tag_Spike))
        {
            return true;
        }

        return false;
    }
}
