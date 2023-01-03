using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoolManager : MonoBehaviour
{
  
    [SerializeField] GameObject[] all_CoinPickup; // array of Coin Insepector
    [SerializeField] private int currentCoinPickupIndex = 0;
    [SerializeField] private GameObject[] all_HealPickUp;
    [SerializeField] private int currentHealPickUpIndex = 0;
    [SerializeField] private GameObject[] all_MultyShotPickUp;
    [SerializeField] private int currentMultyShotPickupInDex;
    [SerializeField] private GameObject[] all_RatePowerUpPickUp;
    [SerializeField] private int currentRatePowerUpPipckUpindex;
    [SerializeField] private GameObject[] all_BulletDamagePowerUpPickUp;
    [SerializeField] private int currentBulletDamagePowerupIndex;
    [SerializeField] private GameObject[] all_JetPackFuelPickUp;
    [SerializeField] private int currentJetPackFuelPickUpIndex;
    [SerializeField] private GameObject[] all_SpeedPowerUpPickupIndex;
    [SerializeField] private int currentSpeedPowerUpPickUpindex;

    public void SpawnCoinPickUp(Vector3 Position)
    {
        all_CoinPickup[currentCoinPickupIndex].SetActive(true);
        all_CoinPickup[currentCoinPickupIndex].transform.position = Position;
        currentCoinPickupIndex++;
        if (currentCoinPickupIndex == all_CoinPickup.Length-1)
        {
            currentCoinPickupIndex = 0;
        }
    }
    public void SpawnHealPickUp(Vector3 Position)
    {
        all_HealPickUp[currentHealPickUpIndex].SetActive(true);
        all_HealPickUp[currentHealPickUpIndex].transform.position = Position;
        currentHealPickUpIndex++;
        if (currentHealPickUpIndex == all_HealPickUp.Length-1)
        {
            currentHealPickUpIndex = 0;
        }
    }
    public void SpawnMultyShotPickup(Vector3 Position)
    {
        all_MultyShotPickUp[currentMultyShotPickupInDex].SetActive(true);
        all_MultyShotPickUp[currentMultyShotPickupInDex].transform.position = Position;
        currentMultyShotPickupInDex++;
        if (currentMultyShotPickupInDex == all_MultyShotPickUp.Length - 1)
        {
            currentMultyShotPickupInDex = 0;
        }
    }
    public void SpawnRatePowerUpPickup(Vector3 Position)
    {
        all_RatePowerUpPickUp[currentRatePowerUpPipckUpindex].SetActive(true);
        all_RatePowerUpPickUp[currentRatePowerUpPipckUpindex].transform.position = Position;
        currentRatePowerUpPipckUpindex++;
        if (currentRatePowerUpPipckUpindex == all_RatePowerUpPickUp.Length - 1)
        {
            currentRatePowerUpPipckUpindex = 0;
        }
    }
    public void SpawnBulletPowerUpPickUp(Vector3 Position)
    {
        all_BulletDamagePowerUpPickUp[currentBulletDamagePowerupIndex].SetActive(true);
        all_BulletDamagePowerUpPickUp[currentBulletDamagePowerupIndex].transform.position = Position;
        currentBulletDamagePowerupIndex++;
        if (currentBulletDamagePowerupIndex == all_BulletDamagePowerUpPickUp.Length - 1)
        {
            currentBulletDamagePowerupIndex = 0;
        }
    }
    public void SpawnJetPackFuelPickUp (Vector3 Position)
    {
        all_JetPackFuelPickUp[currentJetPackFuelPickUpIndex].SetActive(true);
        all_JetPackFuelPickUp[currentJetPackFuelPickUpIndex].transform.position = Position;
        currentJetPackFuelPickUpIndex++;
        if (currentJetPackFuelPickUpIndex == all_JetPackFuelPickUp.Length - 1)
        {
            currentJetPackFuelPickUpIndex = 0;
        }
    }
    public void SpawnPlayerSpeedPowerUpPickup(Vector3 position)
    {
        all_SpeedPowerUpPickupIndex[currentSpeedPowerUpPickUpindex].SetActive(true);
        all_SpeedPowerUpPickupIndex[currentSpeedPowerUpPickUpindex].transform.position = position;
        currentSpeedPowerUpPickUpindex++;
        if (currentSpeedPowerUpPickUpindex == all_SpeedPowerUpPickupIndex.Length-1)
        {
            currentSpeedPowerUpPickUpindex = 0;
        }
    }
}
