using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private Transform rocketSpawnPosition;
    [SerializeField] private float[] all_RocketRange;
    [SerializeField] private float range_Rocket = 10;
    [SerializeField] private float[] all_RocketLauncherFirerate;
    [SerializeField] private float rocketLaucherFireRate = 2;

    private bool IsRocketLaunch = true;

    private void Start()
    {
        rocketLaucherFireRate = all_RocketLauncherFirerate[GameManager.instance.GetCurrentGameLevel()];
        range_Rocket = all_RocketRange[GameManager.instance.GetCurrentGameLevel()];
    }

    private void Update()
    {
        if (!GameManager.instance.GetIsPlayerLive())
        {
              return;
        }
        float range = Vector3.Distance(PlayerManager.Instance.Player().transform.position, transform.position);
        if (range > range_Rocket)
        {
            return;
        }
        if (IsRocketLaunch)
        {
            StartCoroutine(SpawnRocket());
        }
       
    }

    IEnumerator  SpawnRocket()
    {
       
        Instantiate(rocket, rocketSpawnPosition.position, rocket.transform.rotation);
        IsRocketLaunch = false;
        yield return new WaitForSeconds(rocketLaucherFireRate);
        IsRocketLaunch = true;
    }
}
