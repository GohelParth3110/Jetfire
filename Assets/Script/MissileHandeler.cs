using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissileHandeler : MonoBehaviour
{
    [SerializeField] private int minRange;
    [SerializeField] private int maxRange;
    [SerializeField] private GameObject[] all_Missile;
    [SerializeField] private float[] all_Wavetime;
    [SerializeField] private float[] all_MissileRate;
   
     [SerializeField] private float missileRate;
    [SerializeField] private float offset;
    [SerializeField] private float gameStartDelay = 5;
    private bool isStartDelay = true;
   
    private bool isMissileSpawn = true;
    int i = 0;
   [SerializeField] private int[] nextMissileUnlocked ;


   

    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
            return;
        }
        if (!isMissileSpawn)
        {
            return;
        }
        transform.position = new Vector3(PlayerManager.Instance.Player().transform.position.x + offset,
            transform.position.y, transform.position.z);

        if (!isStartDelay)
        {
            missileRate = all_MissileRate[GameManager.instance.GetCurrentGameLevel()];
           
            StartCoroutine(SpawnMissileEnemy());
        }
        gameStartDelay -= Time.deltaTime;

        if (gameStartDelay<0)
        {
            isStartDelay = false;
        }
        
      
    }

    IEnumerator SpawnMissileEnemy()
    {
        isMissileSpawn = false;

        
        if (i != all_Missile.Length-1)
        {
            if (GameManager.instance.GetCurrentGameLevel() > nextMissileUnlocked[i])
            {

                i++;
            }
        }
        int SpawnMissile = Random.Range(0, i);
        int positionY = Random.Range(minRange, maxRange);

        Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);
        Instantiate(all_Missile[SpawnMissile], position, all_Missile[SpawnMissile].transform.rotation);
        yield return new WaitForSeconds(missileRate);
        isMissileSpawn = true;
    }
}
