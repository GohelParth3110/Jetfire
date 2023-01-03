using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerPath : MonoBehaviour
{
    [SerializeField] SpawnPath spawnPath;
     private GameObject player;
    [SerializeField] private float offset;
    private string tag_Path = "Path";


    public  void setPlayer(GameObject currentPlayer)
    {
        player = currentPlayer;
    }
    private void Update()
    {
        if (GameManager.instance.GetIsPlayerLive())
        {
            transform.position = new Vector3(player.transform.position.x - offset, player.transform.position.y, player.transform.position.z);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
       
        
        if (other.gameObject.CompareTag(tag_Path))
        {
            Destroy(other.gameObject);
            spawnPath.SpawnNewPath();
        }
    }


}
