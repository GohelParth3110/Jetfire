using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

    [SerializeField] private List<EnemyGroup> list_EnemyGroups = new List<EnemyGroup>();
    private int numberOfEnemiesToSpawn = 0;
    [SerializeField] private List<TreesGroup> list_TreesGroups = new List<TreesGroup>();
    private int numberOfTreesToSpawn = 0;
    [SerializeField] private List<GameObject> list_SpawnPickUpPoint;

    private int totalPickupPointsAvailable;

    public Transform pathRevivePosition;

    private void Start()
    {
        CalculateNumberOfEnemiesToSpawn();
        SpawnEnemiesFromDifferentGroups();
        CalculateNumberOfTreesSpawn();
        SpawnTreesFromDiffrentGroup();

        SpawnPickups();

       

    }

    private void CalculateNumberOfEnemiesToSpawn()
    {
        if (GameManager.instance.GetCurrentGameLevel()<12)
        {
            numberOfEnemiesToSpawn = Random.Range(list_EnemyGroups.Count - 2, list_EnemyGroups.Count);
        }
        else
        {
            numberOfEnemiesToSpawn = list_EnemyGroups.Count;
        }
       
    }

    private void CalculateNumberOfTreesSpawn()
    {
        numberOfTreesToSpawn = Random.Range(list_TreesGroups.Count - 2, list_TreesGroups.Count);
    }

    private void SpawnEnemiesFromDifferentGroups()
    {

        for (int i = 0; i < list_EnemyGroups.Count; i++)
        {
            int randomEnemyGroupIndex = Random.Range(0, list_EnemyGroups.Count);

            SpawnEnemyFromGroup(randomEnemyGroupIndex);

            list_EnemyGroups.RemoveAt(randomEnemyGroupIndex);
        }

    }
    private void SpawnTreesFromDiffrentGroup()
    {

        for (int i = 0; i < list_TreesGroups.Count; i++)
        {
            int randomTreeSpawnIndex = Random.Range(0, list_TreesGroups.Count);

            SpawnTreesFromGroup(randomTreeSpawnIndex);

            list_TreesGroups.RemoveAt(randomTreeSpawnIndex);
        }

    }

    private void SpawnEnemyFromGroup(int enemyGroupIndex)
    {
        int enemySpawnIndex = Random.Range(0, list_EnemyGroups[enemyGroupIndex].GetTotalEnemiesInsideGroup());
        list_EnemyGroups[enemyGroupIndex].ActivateEnemy(enemySpawnIndex);
    }
    private void SpawnTreesFromGroup(int treesGroupIndex)
    {
        int treesSpawnIndex = Random.Range(0, list_TreesGroups[treesGroupIndex].TotalTreesInGroup());
        list_TreesGroups[treesGroupIndex].ActiveTreeInHirachy(treesSpawnIndex);
    }

    private void SpawnPickups()
    {
        totalPickupPointsAvailable = list_SpawnPickUpPoint.Count;

        SpawnCoinPickup();
        SpawnHealPickup();
        SpawnMultyShotPowerUpPickUp();
        SpawnRatePowerPickUp();
        SpawnJetPackFuelPickUp();
        SpawnBulletPowerUpPickUp();
        SpawnSpeedPowerUpPickUp();
    }

    private void SpawnCoinPickup()
    {
        if (!ShouldSpawnPickUp(50))
        {
            return; // should not spawn coins in this path
        }

        int noOfCoinTOSpawnCurrentPath = Random.Range(0, list_SpawnPickUpPoint.Count - 5);

        for (int i = 0; i < noOfCoinTOSpawnCurrentPath; i++)
        {

            int randomSpawnPositionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
            Vector3 position = list_SpawnPickUpPoint[randomSpawnPositionIndex].transform.position;
            GameManager.instance.GetPickupPoolManager().SpawnCoinPickUp(position);
            list_SpawnPickUpPoint.RemoveAt(randomSpawnPositionIndex);

        }

        totalPickupPointsAvailable -= noOfCoinTOSpawnCurrentPath;
    }

    private void SpawnHealPickup()
    {
        if(totalPickupPointsAvailable == 0)
        {
            return;
        }

        if (!ShouldSpawnPickUp(20))
        {
            return; // should not spawn
        }

        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnHealPickUp(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);

        totalPickupPointsAvailable--;
    }

    private void SpawnMultyShotPowerUpPickUp()
    {
        if (totalPickupPointsAvailable == 0)
        {
            return;
        }
        if (!ShouldSpawnPickUp(8))
        {
            return; // should not spawn coins in this path
        }

        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnMultyShotPickup(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);
        totalPickupPointsAvailable--;

    }

    private void SpawnRatePowerPickUp()
    {
        if (totalPickupPointsAvailable == 0)
        {
            return;
        }
        if (!ShouldSpawnPickUp(5))
        {
            return; // should not spawn coins in this path
        }

        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnRatePowerUpPickup(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);
        totalPickupPointsAvailable--;
       

    }
    private void SpawnBulletPowerUpPickUp()
    {
        if (totalPickupPointsAvailable == 0)
        {
            return;
        }
        if (!ShouldSpawnPickUp(8))
        {
            return; // should not spawn coins in this path
        }

        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnBulletPowerUpPickUp(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);
        totalPickupPointsAvailable--;
        
    }
    private void SpawnJetPackFuelPickUp()
    {
        if (totalPickupPointsAvailable == 0)
        {
            return;
        }
        if (!ShouldSpawnPickUp(12))
        {
            return; // should not spawn coins in this path
        }
        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnJetPackFuelPickUp(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);
        totalPickupPointsAvailable--;
    }
    private void SpawnSpeedPowerUpPickUp()
    {
        if (totalPickupPointsAvailable == 0)
        {
            return;
        }
        if (!ShouldSpawnPickUp(8))
        {
            return; // should not spawn coins in this path
        }

        int currentPoistionIndex = Random.Range(0, list_SpawnPickUpPoint.Count);
        Vector3 position = list_SpawnPickUpPoint[currentPoistionIndex].transform.position;
        GameManager.instance.GetPickupPoolManager().SpawnPlayerSpeedPowerUpPickup(position);
        list_SpawnPickUpPoint.RemoveAt(currentPoistionIndex);
        totalPickupPointsAvailable--;
        
    }

    private bool ShouldSpawnPickUp(int chances)
    {
        int randomRange = Random.Range(0, 100);

        if(randomRange <= chances)
        {
            return true; // should spawn pickup
        }

        return false; // should not spawn anything
    }

}
