using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private GameObject coinCollectedVFX;
    private string tag_Player = "PlayerBody";
    private bool shouldMoveTowardsTarget = false;
    [SerializeField] private float coinMoveTowardsTargetSpeed = 2;
    [SerializeField] private Transform  targetCoinPosition;
    
    
    [SerializeField] private AudioClip coinCollcetionSFX;
   
    private void Update()
    {
        CoinAnimation();
       
    }

    private void CoinAnimation()
    {
        if (!shouldMoveTowardsTarget)
        {
            return;
        }


        transform.position = Vector3.MoveTowards(transform.position, targetCoinPosition.position, coinMoveTowardsTargetSpeed * Time.deltaTime);
        float distnce = Vector3.Distance(transform.position, targetCoinPosition.position);
        if (distnce==0)
        {
            shouldMoveTowardsTarget = false;
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(tag_Player))
        {
            
            shouldMoveTowardsTarget = true;
            GameManager.instance.UpdateCoin();
            AudioManager.instance.PlayPickUpSound(coinCollcetionSFX);
            Instantiate(coinCollectedVFX, transform.position, transform.rotation);
        }
    }

   
}
