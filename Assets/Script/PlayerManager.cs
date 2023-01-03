using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private PlayerData[] all_PlayerData;
    [SerializeField] private GameObject[] all_player;
    [SerializeField] private float playerAnimationTime;
     private int currentPlayerIndex;
    [SerializeField] private RectTransform panel_Coin;
    

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject cmCamera;
    [SerializeField] private DestroyerPath destroyerCollider;
    private GameObject player;

    // player Properites
    private float airSpeed;
    private float GroundSpeed;
    private float JetPackSpeed;
    private float bulletDamage;
    private float playerHealth;
    private float fireRate;
    private float jetPackRefillTime;
    private float JetPackFuelTime;

    private void Awake()
    {
        Instance = this;
       
    }

   
    

   


    public PlayerData[] GetAll_PlayerData()
    {
        return all_PlayerData;
    }
    public int GetCurrentPlayerIndex()  // this Value Get Cuurent Player Selected In game;
    {
        return currentPlayerIndex;
    }

    public void SetPlayerIndex(int Index)
    {
        currentPlayerIndex = Index;
    }
    public void SetPlayer()
    {
        player = Instantiate(all_player[currentPlayerIndex], transform.position, transform.rotation);
        player.transform.position = new Vector3(10, 7, 0);
        
        destroyerCollider.setPlayer(player);
           
        cinemachineVirtualCamera.Follow = player.GetComponent<PlayerMovement>().SetOfssetForCamera();

        player.GetComponentInChildren<PlayerVFX>().SetMaxEmmision();

    }
    public GameObject Player()
    {
        return player; // current player only
    }
    public void StartGameManager()
    {

       
        PlayerManager.Instance.SetPlayer();

        
        Sequence seq = DOTween.Sequence();

        seq.Append(Player().transform.DOMoveY(-4.1f, playerAnimationTime)).AppendCallback(setUiHomeScreen);
    }

   

    private void setUiHomeScreen()
    {     
        player.GetComponentInChildren<PlayerVFX>().SetMinimumEmmision(); 
        UiManager.instance.GetUiHomeScreen().gameObject.SetActive(true);
        cmCamera.gameObject.SetActive(true);    
    }

   

   




    public float SetAirSpeed()
    {
        airSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed() * 0.8f;
        return airSpeed;
    }
    public float SetPickupTimeAirSpeed()
    {
        airSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed() * 0.8f * 1.2f;  //20% increased
        return airSpeed;
    }
    public float SetGroundSpeed()
    {
        GroundSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed();
        return GroundSpeed;
    }
    public float SetPickUpTimeGroundSpeed()
    {
        GroundSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed() * 1.2f; // 20% increased
        return GroundSpeed;
    }
    public float SetJetPackSpeed()
    {
        JetPackSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed();
        return JetPackSpeed;
    }
    public float SetPickUpJetPackSpeed()
    {
        JetPackSpeed = all_PlayerData[currentPlayerIndex].CurrentVertionSpeed() * 1.2f; // 20% increased
        return JetPackSpeed;
    }

    public float SetBulletDamage()
    {
        bulletDamage = all_PlayerData[currentPlayerIndex].CurrentVertionDamage();
        return bulletDamage;
    }


    public float SetpickUpTimeBulletDamage()
    {
        bulletDamage = all_PlayerData[currentPlayerIndex].CurrentVertionDamage() * 1.5f; // 50% increased
        return bulletDamage;
    }
    public float SetPlayerHelth()
    {
        playerHealth = all_PlayerData[currentPlayerIndex].CurrentVertionHealth();

        return playerHealth;
    }
    public float SetPickupTimePlayerHealth()
    {
        playerHealth = all_PlayerData[currentPlayerIndex].CurrentVertionHealth() * 0.2f;  // 20% of max health bonus
        return playerHealth;
    }
    public float SetPlayerFireRate()
    {
        fireRate = all_PlayerData[currentPlayerIndex].CurrentVertionFireRate();
        return fireRate;
    }

    public float SetPickupTimePlayerFireRate()
    {
        fireRate = all_PlayerData[currentPlayerIndex].CurrentVertionFireRate()*0.5f;   //  fire rate 50% decreased
        return fireRate;
    }

    public float SetJetPackFuelTime()
    {
        JetPackFuelTime = all_PlayerData[currentPlayerIndex].CurrentVertionMaxJetPackFuel();
        return JetPackFuelTime;
    }
    public float SetPickUpTimeJetPackFuel()
    {
        JetPackFuelTime = all_PlayerData[currentPlayerIndex].CurrentVertionMaxJetPackFuel() * 0.25f;   // 25% add bonus 
        return JetPackFuelTime;
    }
    public float SetJetPackReFillTime()
    {
        jetPackRefillTime = all_PlayerData[currentPlayerIndex].CurrentVertionMaxJetPackFuel() * 1.5f;
        return jetPackRefillTime;
    }




}
