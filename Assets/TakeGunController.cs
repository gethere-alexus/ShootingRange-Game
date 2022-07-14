using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeGunController : MonoBehaviour
{
    
    [SerializeField]private TextMeshProUGUI pressEText;
    private GunManager gunManager;
    [SerializeField] private GunManager.GunType gunType;

    void Start() 
    {
        gunManager = GameObject.FindGameObjectWithTag("GunManager").GetComponent<GunManager>();
        pressEText = GameObject.FindGameObjectWithTag("PressText").GetComponent<TextMeshProUGUI>();  
    }

    public void OnGunEnter() 
    {
        if(gunManager.takedGuns.Contains(gunType))
        {
            pressEText.text = gunManager.atempYouHaveGun;
        }
        else
        {
            pressEText.text = "Press 'E' to take " + gunType;
        }

        pressEText.enabled = true;
    }
    public void OnGunExit()
    {
        pressEText.enabled = false;
    }
    public void OnGunStay() 
    {
        if(Input.GetKey(KeyCode.E))
        {
            gunManager.changingGun = gunType.ToString();
            Destroy(this.gameObject);
            OnGunExit();
        }    
    }

}
