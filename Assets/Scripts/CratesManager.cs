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
        clips,
        rifle,
        pistol
    }
    [SerializeField] private TextMeshProUGUI takeText;
    [SerializeField] private TypeCrate typeCrate;
    [SerializeField] private GunManager gunManager;
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
        takeText.enabled = true;   
    }
    void OnTriggerExit(Collider other) 
    {
        takeText.enabled = false;    
    }
    void OnTriggerStay(Collider other) 
    {
        if(Input.GetKey(KeyCode.E) && other.gameObject.name == "Player")
        {
            if(typeCrate == TypeCrate.clips)
            {
            gunManager.magazinesCount += countOfItemInside;
            }
            else if(typeCrate == TypeCrate.hp)
            {
                healthSystem.health += countOfItemInside;
            }

            takeText.enabled = false;
            Destroy(this.gameObject);
        }
    }
    
}
