using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject jetFuelCollectedVFX;
    private string tag_Player = "PlayerBody";
    [SerializeField] private GameObject body;
   
    [SerializeField] private AudioClip jetpackCollectedSFX;
    [SerializeField] private float delaySecondofgameObject = 3;
    [SerializeField] private SphereCollider sphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Player))
        {
            sphere.enabled = false;
            body.gameObject.SetActive(false);
            StartCoroutine(DelayDisbleGameObject());
            AudioManager.instance.PlayPickUpSound(jetpackCollectedSFX);
           
            Instantiate(jetFuelCollectedVFX, transform.position, transform.rotation);
            PlayerManager.Instance.Player().GetComponent<PlayerMovement>().AddJetPackFuelPickUp();
        }
    }

    IEnumerator DelayDisbleGameObject()
    {
        yield return new WaitForSeconds(delaySecondofgameObject);
        this.gameObject.SetActive(false);
        sphere.enabled = true;
    }
}
