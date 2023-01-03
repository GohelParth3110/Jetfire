using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private bool isUnlocked;    // get,Set;
    [SerializeField] private int buyPrice;
    [SerializeField] private  int currentPlayerLevel; // get,set;
    [SerializeField] private int[] all_UpgradePrice;
    [SerializeField] [Range(50,1000)]private int[] all_Health;
    [SerializeField] [Range(10, 335)] private float[] all_Damage;
    [SerializeField] [Range(3, 10)] private float[] all_Speed;
    [SerializeField] [Range(0.08f, 1.5f)] private float[] all_FireRate;
    [SerializeField] [Range(3, 12)] private float[] all_MaxjetPackTime;

    // in this case player jetPack speed = ground Speed = speed and air speed = 80% speed;


    
    public float  CurrentVertionHealth()
    {
        return all_Health[currentPlayerLevel];
    }
    public float CurrentVertionDamage()
    {
        return all_Damage[currentPlayerLevel];
    }
    public float CurrentVertionSpeed()
    {
        return all_Speed[currentPlayerLevel];
    }
    public float CurrentVertionFireRate()
    {
        return all_FireRate[all_FireRate.Length-1- currentPlayerLevel];
    }
    public float FireRateForSliderCurrentVertion()
    {
        // this method only visulaizetion of slider;

        return all_FireRate[currentPlayerLevel];
    }
    public float CurrentVertionMaxJetPackFuel()
    {
        return all_MaxjetPackTime[currentPlayerLevel];
    }

    public int CurrentCoinShowInScreen()
    {
        if (isUnlocked)
        {
            return all_UpgradePrice[currentPlayerLevel];
            
        }

        return buyPrice;
    }
    public bool GetUnLockStatus()
    {
        return isUnlocked;
    }
    public void SetUnlockStatus(bool setStatus)
    {
        isUnlocked = setStatus;
    }

    public int  GetPlayerCurrentLevel()
    {
        return currentPlayerLevel;
    }

    public void SetCurrentLevelForStartGame(int index)
    {
        currentPlayerLevel = index;
    }

    public void SetCurrentVertionofUpGrade( GameObject gameObject)
    {
        currentPlayerLevel++;
       
        if (currentPlayerLevel == all_Health.Length-1)
        {
            
            gameObject.SetActive(false);
        }
       
    }
}
