using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private GameObject[] all_EnemiesInsideGroup;


    public int GetTotalEnemiesInsideGroup()
    {
        return all_EnemiesInsideGroup.Length;
    }

    public void ActivateEnemy(int index)
    {
        all_EnemiesInsideGroup[index].SetActive(true);
    }
}
