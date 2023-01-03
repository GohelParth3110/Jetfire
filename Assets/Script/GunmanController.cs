using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunmanController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform gunRotation; // rotate according to player position
    [SerializeField] private Transform transform_HealthUI; // transform component of ui 
    [SerializeField] private Animator anim_Gunman;
    [SerializeField] private SpriteRenderer sprite_GunmanBody;
    [SerializeField] private GameObject body_Sprite;
    [SerializeField] private GameObject body_45Degree;

    [Header("Gunman Properties")]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float[] all_FireRange;
    [SerializeField] private float[] all_FireRate;
    [SerializeField] private float AngleRange = 45;
    [SerializeField] private float enemyShootingTimeDelay;
    [SerializeField] private bool isIdle;
    [SerializeField] private Transform[] all_MovePoints;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private float shootingDelay;
    // Runtime Data
    private Vector3 leftMovePosition;
    private Vector3 rightMovePosition;
    private float isStopMoveStartTime = 0;  // this time for when playshoot startttime counting
    private bool isEnemyMoveShootingTime = false; // this bool for when playershooting time movement
    private float fireRange; // current range
    private float fireRate; // current rate
    private float currentShootWaitTime = 0f; // 
    private bool moveRight = true;

    [Header("Gunman Properties")]
   
    [SerializeField] private Sprite idlePositionSprite;
      
    private void Start()
    {
        fireRate = all_FireRate[GameManager.instance.GetCurrentGameLevel()];
        fireRange = all_FireRange[GameManager.instance.GetCurrentGameLevel()];
        currentShootWaitTime = fireRate;
        
        if (!isIdle)
        {
            SetMovementPositions();
            SetGunmanAnimationState(true);
        }
    }

    private void SetMovementPositions()
    {
        leftMovePosition = new Vector3(all_MovePoints[0].position.x, transform.position.y, transform.position.z);
        rightMovePosition = new Vector3(all_MovePoints[1].position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
            anim_Gunman.enabled = false;
            return;
        }
        MoveCharacter();
        FacePlayer();
        ShootPlayer();
        IsStopMovement();
    }

    private  void ShootPlayer()
    {
        currentShootWaitTime += Time.deltaTime;

       
        if (currentShootWaitTime > fireRate)
        {
            float range = Vector3.Distance(transform.position, PlayerManager.Instance.Player().transform.position);
           
            if (Mathf.Abs(range) > fireRange)
            {
                return; 
            }

            float playerYPos = transform.position.y - PlayerManager.Instance.Player().transform.position.y;

            if (playerYPos > 1)
            {
                return; // player below enemy;
            }

          
            currentShootWaitTime = 0f;
            ShootBulletTowardsPlayer();
               
        }
    }

    private void ShootBulletTowardsPlayer()
    {
       

        sprite_GunmanBody.sprite = idlePositionSprite;
        Vector3 direction = (PlayerManager.Instance.Player().transform.position - transform.position);

        gunRotation.right = direction;


        if (gunRotation.transform.eulerAngles.z < AngleRange && gunRotation.transform.eulerAngles.z > 0)
        {

            isEnemyMoveShootingTime = true;
            SetGunmanAnimationState(false);


            if (gunRotation.transform.eulerAngles.z>30f)
            {
                body_Sprite.gameObject.SetActive(false);
                body_45Degree.gameObject.SetActive(true);
            }
            StartCoroutine(SpawnBullet());
        }


        else if (gunRotation.transform.eulerAngles.z > 180 - AngleRange && gunRotation.transform.eulerAngles.z < 180)
        {
            isEnemyMoveShootingTime = true;
            SetGunmanAnimationState(false);

            if (gunRotation.transform.eulerAngles.z < 160)
            {
                body_Sprite.SetActive(false);
                body_45Degree.SetActive(true); 
            }
            StartCoroutine(SpawnBullet());
        }
       
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(shootingDelay);
        GameObject currentBullet = Instantiate(bullet, bulletSpawnPosition.position, gunRotation.rotation);

        currentBullet.GetComponent<EnemyBullet>().SetBulletDirection(gunRotation.right);
       

    }
    private void  IsStopMovement()
    {
        if (isEnemyMoveShootingTime == false)
        {
           
            isStopMoveStartTime = 0;
            return;
        }

       
        isStopMoveStartTime += Time.deltaTime;
        if (isStopMoveStartTime > enemyShootingTimeDelay)
        {
            body_45Degree.SetActive(false);
            body_Sprite.SetActive(true);

            isEnemyMoveShootingTime = false;
            SetGunmanAnimationState(true);

        }
    }

    private void FacePlayer()
    {
        float checkingPlayerPos = transform.position.x - PlayerManager.Instance.Player().transform.position.x;


        if (checkingPlayerPos > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform_HealthUI.localScale = new Vector3(1, 1, 1);
            
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            transform_HealthUI.localScale = new Vector3(-1, 1, 1);
        }
    }

  
    private void MoveCharacter()
    {
        if (isIdle)
        {
            SetGunmanAnimationState(false);
            return;
        }

        if (isEnemyMoveShootingTime)
        {       
            return;
        }

      
        if (moveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftMovePosition, moveSpeed * Time.deltaTime);

            if (transform.position.x == leftMovePosition.x)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, rightMovePosition, moveSpeed * Time.deltaTime);

            if (transform.position.x == rightMovePosition.x)
            {
                moveRight = true;
            }

        }

    }

    private void SetGunmanAnimationState(bool state)
    {
        anim_Gunman.enabled = state;
    }
}
