using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCastHandler : MonoBehaviour
{
    [SerializeField] private Transform[] all_ForwardRayCastStartPositions;
    [SerializeField] private Transform[] all_BakwardRayCastStartPositions;
    [SerializeField] private Transform[] all_UpWordRayCastStartPosition;
    private float rayCastLength = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask spaceLayer;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isTopGround;

    private RaycastHit hit;


    public bool IsGroundTouch()
    {
        if (Physics.Raycast(transform.position, Vector3.down, rayCastLength, groundLayer))
        {
            
            isGround = true;
        }
        else
        {
            isGround = false;
        }
        return isGround;
    }
       
     
    
    public bool IsSomethingBlockingPathForward()
    {
        int hitCounter = 0;

        for (int i = 0; i < all_ForwardRayCastStartPositions.Length; i++)
        {
            if (Physics.Raycast(all_ForwardRayCastStartPositions[i].position, Vector3.right, out hit, rayCastLength, groundLayer))
            {
                hitCounter += 1;
            }
        }

        
        if(hitCounter == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
            
    }



    public bool isSomethingBlockingPathBackwards()
    {
        int hitCounter = 0;

        for (int i = 0; i < all_BakwardRayCastStartPositions.Length; i++)
        {
            if (Physics.Raycast(all_BakwardRayCastStartPositions[i].position, Vector3.left, out hit, rayCastLength, groundLayer))
            {
                hitCounter += 1;
            }
        }
       
        if (hitCounter == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public bool isUPwordRayCastTouchToGround()
    {
        int hitCounter = 0;

        for (int i = 0; i < all_UpWordRayCastStartPosition.Length; i++)
        {
            if (Physics.Raycast(all_UpWordRayCastStartPosition[i].position, Vector3.up, out hit, rayCastLength, groundLayer))
            {
               
                hitCounter += 1;
            }
        }

        if (hitCounter == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool isUPwordRayCastTouchToSpace()
    {
        int hitCounter = 0;

        for (int i = 0; i < all_UpWordRayCastStartPosition.Length; i++)
        {
            if (Physics.Raycast(all_UpWordRayCastStartPosition[i].position, Vector3.up, out hit, rayCastLength, spaceLayer))
            {
                hitCounter += 1;
            }
        }

        if (hitCounter == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

}
