using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float currentMissilSpeed = 5;
    [SerializeField] private GameObject MissileSFX;
    [SerializeField] private int currentMissilDamage = 5;
    [SerializeField] private int[] all_MissileDamage;
    [SerializeField] private float[] all_MissileSpeed;
   

    
     
    private string tag_Boundry = "Boundry";
    private string tag_Enemy = "Enemy";
    private string tag_Path = "Path";
    private string tag_Player = "PlayerBody";
    private string tag_PickUp = "PickUp";
    private string tag_Spike = "Spike";

    private void Start()
    {
        currentMissilSpeed = all_MissileSpeed[GameManager.instance.GetCurrentGameLevel()];
        currentMissilDamage = all_MissileDamage[GameManager.instance.GetCurrentGameLevel()];
    }

    void Update()
    {
        if (GameManager.instance.GetIsPlayerLive())
        {
            transform.Translate(Vector3.left * Time.deltaTime * currentMissilSpeed);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (HasHitIgnoredObjects(other.gameObject.tag))
        {
            return;
        }


       else if (other.gameObject.CompareTag(tag_Player))
        {
            
          Playerhealth currentPlayerHealth =   other.GetComponentInParent<Playerhealth>();

            if (currentPlayerHealth!=null)
            {
                currentPlayerHealth.TakeDamage(currentMissilDamage);
            }
        }
        Destroy(gameObject);
         GameObject current =  Instantiate(MissileSFX, transform.position, transform.rotation);
        Destroy(current, 1);
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
