using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultyShootPowerUpPickUp : MonoBehaviour
{
    private string tag_Player = "PlayerBody";
    [SerializeField] private GameObject multyShotCollectedVFX;
    [SerializeField] private GameObject body;
   
    [SerializeField] private AudioClip multyShotCollected;
    [SerializeField] private float delaySecondofgameObject = 3;
    [SerializeField] private SphereCollider sphereCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Player))
        {
            sphereCollider.enabled = false;
            body.gameObject.SetActive(false);
            StartCoroutine(DelayDisbleGameObject());

            AudioManager.instance.PlayPickUpSound(multyShotCollected);
            Instantiate(multyShotCollectedVFX, transform.position, transform.rotation);
            PlayerManager.Instance.Player().GetComponent<PlayerShooting>().CollectedMultyShotPickUp();

        }
    }
    IEnumerator DelayDisbleGameObject()
    {
        yield return new WaitForSeconds(delaySecondofgameObject);
        this.gameObject.SetActive(false);
        sphereCollider.enabled = true;
    }
}
