using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private bool isItOneTimeThreeBullet = false;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform transform_Gun;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private float angleRange;  
   
    private bool isBulletFire = true;
    private float normalDamage;
  
    private float bullerStartTime = 0;
    [Header("MultyShot")]
    [SerializeField] private Transform bulletSpawnOnePosition;
    [SerializeField] private Transform bullleSpawnTwoPosition;
    private bool ismultyShotPickUp = false;
    private float currentMultyShotTime = 0;
    private float MaxMultyShotTime = 5;// this time get No of Second howmany second Multy BulletShoot;
   
    [Header("FireRate PickUp")]
    private bool isFireRatePickUpCollected = false;
    private float currentFireRateTime = 0;
    private float maxFireRatePickUpTime = 5;
    [SerializeField] private float currentFireRate;
  
   
    [Header("PowerUpPickup")]
    [SerializeField] private float currentBulletPowerUpPickupTime = 0;
    [SerializeField] private float maxBulletPowerPickupTime;
    [SerializeField] private bool isBulletDamageIncreased = false;
    private float currentBulletDamage;
    private float increasedDamage;

    [Header("All in One Asset")]
    [SerializeField] private SpriteRenderer spriteRenderer_Player;
    [SerializeField] private Material mat_Sprite;
    [SerializeField] private Material mat_FireRatePickup;
    private void Start()
    {
        normalDamage = PlayerManager.Instance.SetBulletDamage();
        currentFireRate = PlayerManager.Instance.SetPlayerFireRate();
        
        increasedDamage = PlayerManager.Instance.SetpickUpTimeBulletDamage();
        currentBulletDamage = normalDamage;
    }

    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
            return;
        }
        //GetInput();
        StraightLineBullet();
        HandelerMultyShotBulletPickup();
        HamdlerToRateIncreasingPickup();
        HandlerBulletPowerUpPickup();
        SetBulletDamage();
    }

    

    public void BulletDamagePowerUpCollected()
    {
        isBulletDamageIncreased = true;
        currentBulletPowerUpPickupTime = 0;
    }
   


    public void CollectedMultyShotPickUp()
    {
        ismultyShotPickUp = true;
        currentMultyShotTime = 0;
    }
    public void CollectedSpeedPickUp()
    {
        isFireRatePickUpCollected = true;
        spriteRenderer_Player.material = mat_FireRatePickup;
        currentFireRateTime = 0;
        currentFireRate = PlayerManager.Instance.SetPickupTimePlayerFireRate();
    }


    private void StraightLineBullet()
    {
        TimerForBullet();
        if (!isBulletFire)
        {
            return;
        }

       

        GameObject bull =  Instantiate(bullet, bulletSpawnPosition.position, bullet.transform.rotation);
       
        
       SpawnMultyBullet(); // if multyShotPickUpCollcted
        if (transform.localScale.x == -1)
        {
            bull.GetComponent<PlayerBullet>().SetBulletData(Vector3.left, currentBulletDamage,isBulletDamageIncreased);
            bull.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
           
        }
        else 
        { 
            bull.GetComponent<PlayerBullet>().SetBulletData(Vector3.right, currentBulletDamage, isBulletDamageIncreased);

        }
        if (isItOneTimeThreeBullet)
        {
            StartCoroutine(SpawnMultiPleBullet(bulletSpawnPosition.position));
        }

        isBulletFire = false;
       

    }

    IEnumerator SpawnMultiPleBullet(Vector3 position)
    {
       
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject bull = Instantiate(bullet, position, bullet.transform.rotation);
           
            if (transform.localScale.x == -1)
            {
                bull.GetComponent<PlayerBullet>().SetBulletData(Vector3.left, currentBulletDamage, isBulletDamageIncreased);
                bull.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);

            }
            else
            {
                bull.GetComponent<PlayerBullet>().SetBulletData(Vector3.right, currentBulletDamage, isBulletDamageIncreased);

            }
           
        }
      
    }

    private void SpawnMultyBullet()
    {
        if (!ismultyShotPickUp)
        {
            return;
        }
        GameObject bullOne = Instantiate(bullet, bulletSpawnOnePosition.position, bullet.transform.rotation);
        GameObject bullTwo = Instantiate(bullet, bullleSpawnTwoPosition.position, bullet.transform.rotation);
        if (transform.localScale.x == -1)
        { 
            bullOne.GetComponent<PlayerBullet>().SetBulletData(Vector3.left, currentBulletDamage, isBulletDamageIncreased);
            bullOne.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            bullTwo.GetComponent<PlayerBullet>().SetBulletData(Vector3.left, currentBulletDamage, isBulletDamageIncreased);
            bullTwo.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
        else
        { 
            bullOne.GetComponent<PlayerBullet>().SetBulletData(Vector3.right, currentBulletDamage, isBulletDamageIncreased);
            bullTwo.GetComponent<PlayerBullet>().SetBulletData(Vector3.right,currentBulletDamage, isBulletDamageIncreased);
        }
        if (isItOneTimeThreeBullet)
        {
            StartCoroutine(SpawnMultiPleBullet(bulletSpawnOnePosition.position));
            StartCoroutine(SpawnMultiPleBullet(bullleSpawnTwoPosition.position));
        }
       

    }
    private void TimerForBullet()
    {
        if (isBulletFire)
        {
            return;
        }
       
        bullerStartTime += Time.deltaTime;
        if (bullerStartTime> currentFireRate)
        {
            isBulletFire = true;
            bullerStartTime = 0;
        }
    }
    private void HandlerBulletPowerUpPickup()
    {
        if (!isBulletDamageIncreased)
        {
            return;
        }

        currentBulletPowerUpPickupTime += Time.deltaTime;
       
        
        if (currentBulletPowerUpPickupTime > maxBulletPowerPickupTime)
        {
            isBulletDamageIncreased = false;
        }
    }
    private void SetBulletDamage()
    {
        if (isBulletDamageIncreased)
        {
            currentBulletDamage = increasedDamage;
        }
        else
        {
            currentBulletDamage = normalDamage;
        }
    }
    private void HandelerMultyShotBulletPickup()
    {
        if (!ismultyShotPickUp)
        {
            return;
        }

        currentMultyShotTime += Time.deltaTime;
        if (currentMultyShotTime > MaxMultyShotTime)
        {
            ismultyShotPickUp = false;
           
        }
    }
    private     void HamdlerToRateIncreasingPickup()
    {
        if (!isFireRatePickUpCollected)
        {
            return;
        }
        currentFireRateTime += Time.deltaTime;
        if (currentFireRateTime>maxFireRatePickUpTime)
        {
            isFireRatePickUpCollected = false;
            spriteRenderer_Player.material = mat_Sprite;
            currentFireRate  = PlayerManager.Instance.SetPlayerFireRate();
        }
    }




    //private void GetInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 screen = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    //        Vector2 targetDirection = (screen - (Vector2)transform.position);

    //        transform_Gun.right =  targetDirection;
    //        if (transform.localScale.x == -1)
    //        {

    //            if (transform_Gun.localEulerAngles.z > 90 && transform_Gun.localEulerAngles.z <= 180)
    //            {
    //                transform_Gun.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(transform_Gun.localEulerAngles.z, 180-angleRange, 180 ));
    //            }
    //            else if (transform_Gun.localEulerAngles.z > 180 && transform_Gun.localEulerAngles.z <= 270)
    //            {
    //                transform_Gun.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(transform_Gun.localEulerAngles.z, 181, 180 + angleRange));
    //            }
    //            else
    //            {
    //                if (transform_Gun.localEulerAngles.z >0 && transform_Gun.localEulerAngles.z <90)
    //                {
    //                    transform_Gun.localEulerAngles = new Vector3(0, 0, 180 - angleRange);
    //                }
    //                else
    //                {
    //                    transform_Gun.localEulerAngles = new Vector3(0, 0, 180 + angleRange);
    //                }
    //            }
                   
               

    //            GameObject blr = Instantiate(bullet, bulletSpawnPosition.position, bullet.transform.rotation);
    //            blr.transform.eulerAngles = new Vector3(transform_Gun.localEulerAngles.x, transform_Gun.localEulerAngles.y + 180,
    //                transform_Gun.localEulerAngles.z);
    //            blr.GetComponent<PlayerBullet>().SetBulletData(transform_Gun.right,currentBulletDamage);
    //        }
    //        else
    //        {

    //            if (transform_Gun.localEulerAngles.z >= 0 && transform_Gun.localEulerAngles.z <= 90)
    //            {
    //                transform_Gun.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(transform_Gun.localEulerAngles.z, 1, angleRange));
    //            }

    //            else if(transform_Gun.localEulerAngles.z > 270 && transform_Gun.localEulerAngles.z <360)
    //            {
    //                transform_Gun.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(transform_Gun.localEulerAngles.z, 360-angleRange, 360));
    //            }
    //            else
    //            {
    //                if (transform_Gun.localEulerAngles.z >90 && transform_Gun.localEulerAngles.z <180)
    //                {
    //                    transform_Gun.localEulerAngles = new Vector3(0, 0, angleRange);
    //                }
    //                else
    //                {
    //                    transform_Gun.localEulerAngles = new Vector3(0, 0,360- angleRange);
    //                }
                   
    //            }

    //            GameObject bll = Instantiate(bullet, bulletSpawnPosition.position, bullet.transform.rotation);
    //            bll.transform.localEulerAngles = transform_Gun.localEulerAngles;
    //            bll.GetComponent<PlayerBullet>().SetBulletData(transform_Gun.right,currentBulletDamage);
    //        }
              
          

    //    }


    //}

}
