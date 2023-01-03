using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : MonoBehaviour
{
   
    [SerializeField] private GameObject dieSFX;
    
    [SerializeField] private SpriteRenderer spriteRenderer_Player;
    
    [Header("data")]
    [SerializeField] private float delayBetweenTwoColor;
    [SerializeField] private float damageTime; // when Player stay in Spike at that Time How many seconds PlayerReduce health;
    private float maxHealth;
    private float flt_EnemyTouchDamage = 5;

   
    private float healthIncreaseAmount;
   [SerializeField] private float currentHealth;
    private bool isStandingOnSpike = false;
    [SerializeField] private float currentSpikeCollisionTime;
    [SerializeField] private float spikeDamage = 5;

    private Vector3 currentRevivePosition = new Vector3 (5,0,0);

    [SerializeField] private AudioSource audio_PlayerDamageSFX;
    [SerializeField] private AudioSource audio_PlayerDieSFX;

    // tag
    private string tag_Water = "Water";
    private string tag_Spike = "Spike";
    private string tag_Path = "Path";
    private string tag_Enemy = "Enemy";

    private void Start()
    {
        maxHealth = PlayerManager.Instance.SetPlayerHelth();

        healthIncreaseAmount = PlayerManager.Instance.SetPickupTimePlayerHealth();
        currentHealth = maxHealth;
        
    }

    private void HandleAudioSource( AudioSource audioSource)
    {
        if (DataManager.instance.soundValue==0)
        {
            return;
        }

        audioSource.Play();

    }
    public void SetCurrentHealth()
    {
        currentHealth = maxHealth;
        UiManager.instance.GetUiGamePlayScreen().UpdateHealthSlider(currentHealth);
    }
    private void Update()
    {
        if (isStandingOnSpike)
        {
            currentSpikeCollisionTime += Time.deltaTime;

            if(currentSpikeCollisionTime >= damageTime)
            {
                TakeDamage(spikeDamage);
                currentSpikeCollisionTime = 0;
            }
        }
    }


    public void AddHeal()
    {
        currentHealth += healthIncreaseAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

       
        UiManager.instance.GetUiGamePlayScreen().UpdateHealthSlider(currentHealth);
    }
    public  void TakeDamage(float damage)
    {

       
        currentHealth -= damage;
        Debug.Log(currentHealth + "Take Damage Call");
        if (currentHealth <= 0)
        {
            // dead
           
            currentHealth = 0;
            Instantiate(dieSFX, transform.position, transform.rotation);
            HandleAudioSource(audio_PlayerDieSFX);
            gameObject.SetActive(false);
            spriteRenderer_Player.color = Color.white;
            GameManager.instance.GameOver();

            return;
        }

        HandleAudioSource(audio_PlayerDamageSFX);
        UiManager.instance.GetUiGamePlayScreen().UpdateHealthSlider(currentHealth);
        spriteRenderer_Player.color = Color.red;
        StartCoroutine(changecolor());
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag_Water))
        {
          
            Instantiate(dieSFX, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
            HandleAudioSource(audio_PlayerDieSFX);
            GameManager.instance.GameOver();
        }
    }
    IEnumerator changecolor()
    {
        yield return new WaitForSeconds(delayBetweenTwoColor);
        spriteRenderer_Player.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.gameObject.CompareTag(tag_Path))
        {
           
            currentRevivePosition = other.GetComponent<PathController>().pathRevivePosition.position;
        }
        
      

        if (other.gameObject.CompareTag(tag_Spike))
        {
            TakeDamage(spikeDamage);
        }
        if (other.gameObject.CompareTag(tag_Enemy))
        {
            TakeDamage(20);
        }
       
        
      
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Spike))
        {
            isStandingOnSpike = true;           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tag_Spike))
        {        
            isStandingOnSpike = false;
            currentSpikeCollisionTime = 0;
        }

    }

    public void RevivePlayer()
    {
        transform.position = currentRevivePosition;
        // reset boundary
    }

}
