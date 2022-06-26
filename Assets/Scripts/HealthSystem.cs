using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start() 
    {
        health = 100;    
    }
    void FixedUpdate() 
    {
        if(health > 100)
        {
            health = 100;
        }
        healthText.text = health.ToString();
    }
}
