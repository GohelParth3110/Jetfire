using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform bombRotation;  // Bomb Rotation
    [SerializeField] private Transform transform_HealthUI;
    [SerializeField] private Animator anim_Bomber;
    [SerializeField] private SpriteRenderer bomber_SpriteRenderer;

    [Header("Bomber Properites")]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float[] all_FireRange;
    [SerializeField] private float[] all_FireRate;
    [SerializeField] private bool isIdle = false;
    [SerializeField] private float delayofEnemyShootingTime;
    [SerializeField] private Transform[] all_MovePoints;

    [Header("Bullet")]
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bombSpawnPosition;
    [SerializeField] private float SpawnBombDelay;

    [Header("Bomber Properites")]
    [SerializeField] private Sprite idleBomberSprite;

    //runtime Data
    private float fireRange ;
     private float fireRate;
     private bool isEnmyStopMovment = false;
     private float enmyShootingStartTime = 0;
     private float currentShootWaitTime = 0f;
     private bool moveRight = true;
     private Vector3 leftPosition;
     private Vector3 rightPosition;
   
   
    private void Start()
    {
        fireRange = all_FireRange[GameManager.instance.GetCurrentGameLevel()];
        fireRate = all_FireRate[GameManager.instance.GetCurrentGameLevel()];
        currentShootWaitTime = fireRate;
        if (!isIdle)
        {
            SetBomberAnimationState(true);
            SetMovePosition();
        }
    }

    private void SetMovePosition()
    {
        leftPosition = new Vector3(all_MovePoints[0].position.x,transform.position.y,transform.position.z);
        rightPosition = new Vector3(all_MovePoints[1].position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
            anim_Bomber.enabled = false;
            return;
        }
        MoveCharacter();
        FacePlayer();
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        TimerForEnemyShootingStop();
        currentShootWaitTime += Time.deltaTime;

        if (currentShootWaitTime > fireRate)
        {
            
            float range = Vector3.Distance(transform.position, PlayerManager.Instance.Player().transform.position);

            if (Mathf.Abs(range) < fireRange)
            {
                currentShootWaitTime = 0f;
                ShootBombTowordsPlayer();
                SetBomberAnimationState(false);
                isEnmyStopMovment = true;
                bomber_SpriteRenderer.sprite = idleBomberSprite;

            }
        }
    }

    private void ShootBombTowordsPlayer()
    {

        bombRotation.LookAt(PlayerManager.Instance.Player().transform.position);

        StartCoroutine(SpawnBomb());

       

    }
    IEnumerator SpawnBomb()
    {
        yield return new WaitForSeconds(SpawnBombDelay);
        GameObject currentBullet = Instantiate(bomb, bombSpawnPosition.position, bomb.transform.rotation);
        currentBullet.GetComponent<Bomb>().SetBulletDirection(bombRotation.forward);
    }
    private void TimerForEnemyShootingStop()
    {
        if (!isEnmyStopMovment)
        {
           
            enmyShootingStartTime = 0;
        }

       
        enmyShootingStartTime += Time.deltaTime;
        if (enmyShootingStartTime> delayofEnemyShootingTime)
        {
            isEnmyStopMovment = false;
            SetBomberAnimationState(true);


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
       
        if (isEnmyStopMovment)
        {
           
            return;
        }
        if (isIdle)
        {
            
            SetBomberAnimationState(false);
            return;
        }
       
           
        if (moveRight)
        {
                transform.position = Vector3.MoveTowards(transform.position, rightPosition, moveSpeed * Time.deltaTime);

                if (transform.position.x == rightPosition.x)
                {
                    moveRight = false;
                }
        }
        else
        {
                transform.position = Vector3.MoveTowards(transform.position, leftPosition, moveSpeed * Time.deltaTime);
                if (transform.position.x == leftPosition.x)
                {
                    moveRight = true;
                }
        }
    }

    private void SetBomberAnimationState(bool State)
    {
        anim_Bomber.enabled = State;
    }
}
