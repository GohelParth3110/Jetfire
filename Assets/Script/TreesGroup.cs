using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesGroup : MonoBehaviour
{
    [SerializeField] private GameObject[] all_TreesInGroup;


    public int TotalTreesInGroup()
    {
        return all_TreesInGroup.Length;
    }

    public void ActiveTreeInHirachy(int allTreesGroupArrayIndex)
    {
        all_TreesInGroup[allTreesGroupArrayIndex].SetActive(true);
    }
 }
