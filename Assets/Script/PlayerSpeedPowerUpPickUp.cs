using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedPowerUpPickUp : MonoBehaviour
{
    private string tag_Player = "PlayerBody";
    [SerializeField] private GameObject SpeedPoweUpCollectedVFX;
    [SerializeField] private GameObject body;
    [SerializeField] private AudioClip speedPickupCollected;
    [SerializeField] private float delaySecondofgameObject = 3;
    [SerializeField] private SphereCollider sphereCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Player))
        {
            sphereCollider.enabled = false;
            body.gameObject.SetActive(false);
            StartCoroutine(DelayDisbleGameObject());
            AudioManager.instance.PlayPickUpSound(speedPickupCollected);
            Instantiate(SpeedPoweUpCollectedVFX, transform.position, transform.rotation);
            PlayerManager.Instance.Player().GetComponent<PlayerMovement>().CollectedSpeedPickUp();
        }
    }
    IEnumerator DelayDisbleGameObject()
    {
        yield return new WaitForSeconds(delaySecondofgameObject);
        this.gameObject.SetActive(false);
        sphereCollider.enabled = true;
    }
}
