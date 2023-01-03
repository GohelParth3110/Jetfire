using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchEnemy : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    private string tag_PlayerBody = "PlayerBody";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_PlayerBody))
        {
            Playerhealth playerhealth = other.GetComponent<Playerhealth>();
            if (playerhealth!=null)
            {
                playerhealth.TakeDamage(playerDamage);
            }
        }
    }
}
