using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private ParticleSystem Particle_Jet;
    
    //animatiton
    [SerializeField] private Animator player;
    private string is_Walk = "is_Walk";
    private string is_Fly = "is_Fly";

    [Header("Scripts")]
    [SerializeField] private PlayerRayCastHandler rayCastHandler;
    [SerializeField] private PlayerVFX playerVFX;

    [Header("Movement Data")]
    public float horizontalMoveSpeed;
    [SerializeField] private float gravityModifier;
    [SerializeField] private float currentGravityModifier;
    public float targetVerticalSpeed;
    public float currentVerticalSpeed = 0;
    [SerializeField] private float speedChangeRate = 4;
    private bool isForwardBlocked = false;
    private bool isBackSideBlocked = false;
    [SerializeField] private Transform SetOffset;
    

    [Header("Clamp")]
    [SerializeField] private float currentAirSpeed;
    [SerializeField] private float currentGroundSpeed;
    [SerializeField] private float currentJetPackSpeed;

    [SerializeField] float currneMaxJetPackTime;
    [SerializeField] float CurrentJetpackRefillTime;
    [SerializeField] private float currentJetpackFillPercent = 100;
    [SerializeField] private bool isUsingJetpack = false;

   
   
    // user input
    private float horizontalInput;
    private float verticalInput;
    

    // normal speed properties

    [Header("Speed Powerup")]
    [SerializeField] private float maxSpeedPowerUpRunningTime = 5;
    [SerializeField] private float currentSpeedPowerRunningTime = 0f;
    [SerializeField] private bool isSpeedPowerUpRunning = false;
    public float SpeedPowerUpAirSpeed;
    public float SpeedPowerUpGroundSpeed;
    public float speedPowerUpJetpackSpeed;

    public float normalAirSpeed;
    public float normalGroundSpeed;
    public float normaljetpackSpeed;
    public float runTimeJetPackSpeed;
    

    // jetpack Pickup Bonus Value;
    private float JetpackBonus;

    [Header("All in One Asset")]
    [SerializeField] private Material mat_Sprite;
    [SerializeField] private Material mat_SpeedPickupMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;



    private void Start()
    {
        FetchPlayerProperties();
    }

    public Transform SetOfssetForCamera()
    {
        return SetOffset;
    }
    private void FetchPlayerProperties()
    {

        normalAirSpeed = PlayerManager.Instance.SetAirSpeed();
        normalGroundSpeed = PlayerManager.Instance.SetGroundSpeed();
        normaljetpackSpeed = PlayerManager.Instance.SetJetPackSpeed();
        SpeedPowerUpAirSpeed = PlayerManager.Instance.SetPickupTimeAirSpeed();
        SpeedPowerUpGroundSpeed = PlayerManager.Instance.SetPickUpTimeGroundSpeed();
        speedPowerUpJetpackSpeed = PlayerManager.Instance.SetPickUpJetPackSpeed();
        currentJetPackSpeed = normaljetpackSpeed;
        currneMaxJetPackTime = PlayerManager.Instance.SetJetPackFuelTime();
        CurrentJetpackRefillTime = PlayerManager.Instance.SetJetPackReFillTime(); 
        currentAirSpeed = normalAirSpeed;
        currentGroundSpeed = normalGroundSpeed;
        JetpackBonus = PlayerManager.Instance.SetPickUpTimeJetPackFuel();
        spriteRenderer.material = mat_Sprite;
       
    }

   
    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
            return;
        }

       
        UserInput();      

        

        CheckIfGettingBlocked();     
        RotatePlayer();
        UpadetHorizontalSpeed();
        UpdateVerticalSpeed();
        
        HandleSpeedPowerUp();
        PlayerAnimation();
        UpdateJetPack(); // when using jetpack, reduce the jet fuel
        FiilJetPack(); // fill jet fuel over time

        Movement();
      
        UiManager.instance.GetUiGamePlayScreen().UpdateJetpackFuelSlider(currentJetpackFillPercent);
    }

   

    private void HorizontalButtonCliCkInput()
    {
        if (UiManager.instance.GetUiGamePlayScreen().isleftbuttonClick)
        {
            horizontalInput = -1;
           
        }
        else if (UiManager.instance.GetUiGamePlayScreen().isRightbuttonClick)
        {
            horizontalInput = 1;
           
        }
        else
        {
            horizontalInput = 0;
        }
       
    }
    private void VerticaleButtonInput()
    {
        if (UiManager.instance.GetUiGamePlayScreen().isJetButtonClick)
        {
            verticalInput = 1;
        }
        else
        {
            verticalInput = 0;
        }
    }

    private void UpdateVerticalSpeed()
    {
        if (rayCastHandler.isUPwordRayCastTouchToGround())
        {
            runTimeJetPackSpeed = 0;
        }
        else if (rayCastHandler.isUPwordRayCastTouchToSpace())
        {
            runTimeJetPackSpeed = 0;
        }
        else
        {
            runTimeJetPackSpeed = currentJetPackSpeed;
        }

        
        
    }
 
   
  
    private void UserInput()
    {

#if UNITY_EDITOR

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            verticalInput = 1;
        }
        else
        {
            verticalInput = 0;
        }

#endif
        //VerticaleButtonInput();
        //HorizontalButtonCliCkInput();
        //if (horizontalInput > 0)
        //{
        //    if (isForwardBlocked)
        //    {
        //        horizontalInput = 0;
        //    }
        //}
        //else if (horizontalInput < 0)
        //{
        //    if (isBackSideBlocked)
        //    {
        //        horizontalInput = 0;
        //    }
        //}
       

        if (verticalInput>0 && currentJetpackFillPercent > 0)
        {
            targetVerticalSpeed = runTimeJetPackSpeed;
            isUsingJetpack = true;
            playerVFX.SetMaxEmmision();

        }
 
        
        else
        {
            targetVerticalSpeed = -currentGravityModifier;
            isUsingJetpack = false;
            playerVFX.SetMinimumEmmision();

           
        }

    }
    private void PlayerAnimation()
    {
        if (!isUsingJetpack)
        {
            player.SetBool(is_Fly, false);
        }
        else
        {
            player.SetBool(is_Fly, true);
        }
        if (horizontalInput != 0 && rayCastHandler.IsGroundTouch())
        {
            player.SetBool(is_Walk, true);
        }
        else
        {
            player.SetBool(is_Walk, false);
        }
    }
    private void RotatePlayer()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
    private void UpadetHorizontalSpeed()
    {

        if (rayCastHandler.IsGroundTouch())
        {
            horizontalMoveSpeed = currentGroundSpeed;
            currentGravityModifier = 0;
           
        }
        else
        {
            horizontalMoveSpeed = currentAirSpeed;
            currentGravityModifier = gravityModifier;
        }
        
    }
    private void Movement()
    {
        currentVerticalSpeed = Mathf.Lerp(currentVerticalSpeed, targetVerticalSpeed, speedChangeRate * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalMoveSpeed * horizontalInput, currentVerticalSpeed, 0)*Time.deltaTime;

       

        transform.Translate(direction);
    }
   



    private void CheckIfGettingBlocked()
    {
        isForwardBlocked = rayCastHandler.IsSomethingBlockingPathForward();
        isBackSideBlocked = rayCastHandler.isSomethingBlockingPathBackwards();
       
        
    }

    public void CollectedSpeedPickUp()
    {
        isSpeedPowerUpRunning = true;
       currentSpeedPowerRunningTime = 0f;

        spriteRenderer.material = mat_SpeedPickupMaterial;
        currentAirSpeed = SpeedPowerUpAirSpeed;
        currentGroundSpeed = SpeedPowerUpGroundSpeed;
        currentJetPackSpeed = speedPowerUpJetpackSpeed;
    }
   

    private void HandleSpeedPowerUp()
    {
        if (!isSpeedPowerUpRunning)
        {
            return;
        }
       

        currentSpeedPowerRunningTime += Time.deltaTime;

        if(currentSpeedPowerRunningTime >= maxSpeedPowerUpRunningTime)
        {
           
            isSpeedPowerUpRunning = false;
            spriteRenderer.material = mat_Sprite;
            currentAirSpeed = normalAirSpeed;
            currentGroundSpeed = normalGroundSpeed;
            currentJetPackSpeed = normaljetpackSpeed;
        }
    }

    private void UpdateJetPack()
    {
        if (!isUsingJetpack)
        {
            return;
        }

        currentJetpackFillPercent -= (100 / currneMaxJetPackTime) * Time.deltaTime;
    }
    private void FiilJetPack()
    {
        if (isUsingJetpack)
        {
            return;
        }
        if (currentJetpackFillPercent >= 100)
        {
            return;
        }
        currentJetpackFillPercent += (100/CurrentJetpackRefillTime) * Time.deltaTime;
    }

    public void AddJetPackFuelPickUp()
    {
        
        currentJetpackFillPercent += JetpackBonus;
        if (currentJetpackFillPercent>100)
        {
            currentJetpackFillPercent = 100;
        }
    }




}



