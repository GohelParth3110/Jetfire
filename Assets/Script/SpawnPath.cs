using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPath : MonoBehaviour
{
    [SerializeField] private GameObject[] all_Path;  // all Path spawn
    [SerializeField] private Transform PathSpawn;       // game obeject inside pathSpawn;

    [SerializeField] private float[] all_spawnDistance;
    [SerializeField] private float currentSpwnDistance;
    private Transform currentEndPoint;

    private void Start()
    {
        currentSpwnDistance = all_spawnDistance[GameManager.instance.GetCurrentGameLevel()];
        FirstWaveSpawnPath();   // first time path spawn

    }

    [ContextMenu("Demo path spawn")]
    private void SpawnDemo()
    {
        FirstWaveSpawnPath();
    }
    
    private void FirstWaveSpawnPath()
    {

        float pathSpawnStartPosition = 26.15f;

        for (int i = 0; i < 5; i++)
        {
            int Index = Random.Range(0, all_Path.Length);

            GameObject newPath = Instantiate(all_Path[Index], new Vector3(pathSpawnStartPosition, 0, 0), transform.rotation, PathSpawn);

            currentEndPoint = newPath.transform.GetChild(0);

            pathSpawnStartPosition = currentEndPoint.position.x + currentSpwnDistance + Random.Range(0.05f, 2);
         
        }

        // get end point of last path

        
    }

   
    public void SpawnNewPath()
    {

        currentSpwnDistance = all_spawnDistance[GameManager.instance.GetCurrentGameLevel()];
        float pathSpawnStartPosition = currentEndPoint.position.x + currentSpwnDistance + Random.Range(0.05f, 2);
        int Index = Random.Range(0, all_Path.Length);
      
        GameObject newPath = Instantiate(all_Path[Index], new Vector3(pathSpawnStartPosition, 0, 0), transform.rotation, PathSpawn);
        currentEndPoint = newPath.transform.GetChild(0) ;      
    }

  
      
       


    



}
