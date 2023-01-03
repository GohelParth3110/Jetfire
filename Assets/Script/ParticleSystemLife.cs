using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemLife : MonoBehaviour
{
    [SerializeField] private float deathDelay;

    private void Start()
    {
        Destroy(gameObject, deathDelay);
    }
}
