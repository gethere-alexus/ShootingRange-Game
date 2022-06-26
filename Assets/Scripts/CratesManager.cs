using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CratesManager : MonoBehaviour
{
    enum TypeCrate
    {
        hp,
        clips
    }
    [SerializeField] private TextMeshProUGUI takeText;
    [SerializeField] private TypeCrate typeCrate;
    [SerializeField] private ShootingManager shootingManager;
    [SerializeField] private HealthSystem healthSystem;

    private int countOfItemInside;

    void Start() 
    {
        if(typeCrate == TypeCrate.clips)
        {
            countOfItemInside = Random.Range(2 , 4);
        }
        else if (typeCrate == TypeCrate.hp)
        {
            countOfItemInside = Random.Range(10 , 25);
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        string msg = "Press 'E' to take " + countOfItemInside.ToString() + " " + typeCrate;
        takeText.text = msg;
        takeText.gameObject.SetActive(true);   
    }
    void OnTriggerExit(Collider other) 
    {
        takeText.gameObject.SetActive(false);    
    }
    void OnTriggerStay(Collider other) 
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(typeCrate == TypeCrate.clips)
            {
            shootingManager.magazinesCount += countOfItemInside;
            }
            else if(typeCrate == TypeCrate.hp)
            {
                healthSystem.health += countOfItemInside;
            }

            takeText.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    
}
