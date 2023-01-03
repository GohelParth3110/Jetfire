using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walllMoveMent : MonoBehaviour
{


    [SerializeField] private float flt_WallMovementSpeed;
    [SerializeField] private float flt_ChangeSpeed;

   
    [SerializeField] private float flt_MaxTimeChangeWallSpeed = 8;
    [SerializeField] private float flt_CuurentTimeChangeWallSpeed;

    // Update is called once per frame

   
    void Update()
    {
        if (GameManager.instance.GetIsPlayerLive())
        {
            WallMovement();
            flt_CuurentTimeChangeWallSpeed += Time.deltaTime;
            if (flt_CuurentTimeChangeWallSpeed > flt_MaxTimeChangeWallSpeed)
            {
                flt_WallMovementSpeed += flt_ChangeSpeed;
                flt_CuurentTimeChangeWallSpeed = 0;
            }
        }
       
    }
    private void WallMovement()
    {
       
            transform.Translate(Vector3.right * flt_WallMovementSpeed * Time.deltaTime);
        
            
    }
}
