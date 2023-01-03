using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Camponant")]
    [SerializeField] private SpriteRenderer enemyColor;
    [SerializeField] private Slider slider_Health;
    [SerializeField] private GameObject die_SFX;
    [SerializeField] private GameObject body_Enemy;
   
    [Header("data")]
    [SerializeField] private float delayBetweenTwoColor;
    [SerializeField] private float[] all_LevelEnemyHealth;
    private float enemy_Health;
    [SerializeField] private int[] all_DamageOfPlayer;
    [SerializeField]private int currentDamageOfPlayer;
    [SerializeField] private int[] all_LevelScore;

    [Header("Audio Data")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip enemyDamageSFX;
    [SerializeField] private AudioClip enemyDieSFX;

   

    

    private void Start()
    {
        enemy_Health = all_LevelEnemyHealth[GameManager.instance.GetCurrentGameLevel()];
        slider_Health.maxValue = enemy_Health;
        slider_Health.value = slider_Health.maxValue;
        
    }
    public void TakeDamage(float damage)
    {
       
        enemy_Health -= damage;
      
        if (enemy_Health <= 0)
        {
            GameManager.instance.UpdateScore(all_LevelScore[GameManager.instance.GetCurrentGameLevel()]);
            if (DataManager.instance.soundValue ==1)
            {
                audioSource.PlayOneShot(enemyDieSFX, 1);

            }
            
           
            Instantiate(die_SFX, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
           

        }
       
        
         enemyColor.color = Color.red;
         slider_Health.value = enemy_Health;
        if (DataManager.instance.soundValue ==1)
        {
            audioSource.PlayOneShot(enemyDamageSFX, 1);
        }
        

        if (this.gameObject.activeSelf)
        {
            StartCoroutine(changecolor());
        }





    }

    IEnumerator changecolor()
    {
            yield return new WaitForSeconds(delayBetweenTwoColor);
            enemyColor.color = Color.white;
        
        
    }
   


}
