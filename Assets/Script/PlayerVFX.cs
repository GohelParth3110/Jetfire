using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem jet_Partical;


    private float targetEmissionRate = 0;
    [SerializeField] private float CurrentRateOverTime;

    private ParticleSystem.EmissionModule testing;

    private float maxEmmisionRate = 50;
    private float minEmmisionRate = 0;


    private void Start()
    {
        var emission = jet_Partical.emission;
        CurrentRateOverTime = targetEmissionRate;
        var emit = jet_Partical.emission;
        emit.rateOverTime = CurrentRateOverTime;

    }
    private void Update()
    {

        if (UiManager.instance.GetUiGamePlayScreen().GetSlider_Jet().value > 0)
        {
            CurrentRateOverTime = Mathf.Lerp(CurrentRateOverTime, targetEmissionRate, 0.2f);
            var emission = jet_Partical.emission;
            emission.rateOverTime = CurrentRateOverTime;
        }
    }

    public void SetMaxEmmision()
    {
        targetEmissionRate = maxEmmisionRate;
    }
    public void SetMinimumEmmision()
    {
        targetEmissionRate = minEmmisionRate;
    }

}
