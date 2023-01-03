using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Rigidbody rb_Bomb;
    [SerializeField] private float bombSpeed;
    [SerializeField] private Transform bombTransfom;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float[] All_BombToPlayerDamage;
    [SerializeField] private ParticleSystem distroy_ParticalVFX;
    [SerializeField] private GameObject bodyBomd;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bombSpawn;
    [SerializeField] private AudioClip bombDestroy;
    private float damage;
    public Vector3 shootDir;
    private string tag_Boundry = "Boundry";
    private string tag_Enemy = "Enemy";
    private string tag_Player = "PlayerBody";
    private string tag_PickUp = "PickUp";
    private string tag_Spike = "Spike";
    private string tag_Path = "Path";
    public void SetBulletDirection(Vector3 forwarDir)
    {
        shootDir = forwarDir;
    }

    private void Start()
    {
        damage = All_BombToPlayerDamage[GameManager.instance.GetCurrentGameLevel()];
         rb_Bomb.velocity = shootDir * bombSpeed;
        HandleAudioSource(bombSpawn);
       
    }

    private void FixedUpdate()
    {
       

        bombTransfom.Rotate(0, 0, rotationSpeed);

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
                Debug.Log(damage + "Bomb Damage") ;
                playerhealth.TakeDamage(damage);
            }
        }
        bodyBomd.SetActive(false);
        rb_Bomb.velocity = new Vector3(0, 0, 0);
        if (distroy_ParticalVFX!=null)
        {
            distroy_ParticalVFX.Play();
            HandleAudioSource(bombDestroy);
            
        }
        Destroy(gameObject, 2);
      
       
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
