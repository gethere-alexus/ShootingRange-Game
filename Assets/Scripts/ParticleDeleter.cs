using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeleter : MonoBehaviour
{
    [SerializeField] private float TimeToDelay = 0.5f;

    void Start() 
    {
        Destroy(gameObject , TimeToDelay);    
    }
}
