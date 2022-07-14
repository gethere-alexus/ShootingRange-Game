using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class GunManager : MonoBehaviour
{
    public enum GunType
    {
        rifle,
        pistol,
        None
    }
    [SerializeField] public GameObject _scopeedOffset;
    [SerializeField] public GameObject _unscopeedOffset;
    [SerializeField] public TextMeshProUGUI bulletsText;
    [SerializeField] public TextMeshProUGUI magazineText;
    [SerializeField] public TextMeshProUGUI messageText;
    [SerializeField] public Camera _camera;
    [SerializeField] public GunType currentType;
    [HideInInspector] public string changingGun;
    [SerializeField] private ShootingManager RifleShootManager;
    [SerializeField] private ShootingManager PistolShootManager;
    [HideInInspector] public int magazinesCount = 0;
    
    [SerializeField] public List<GunType> takedGuns = new List<GunType>();
    
    [HideInInspector]public string atempYouHaveGun = "You already have this gun !";


    void Start() 
    {
        changingGun = GunType.None.ToString();
    }
    void Update() 
    {
        HandSettings();

        MagazineManage();

        TakeGun();
        SwitchGun();
    }
    void MagazineManage()
    {
        if(magazinesCount > 0  && currentType != GunType.None)
        {
            if(currentType == GunType.pistol)
            {
                PistolShootManager.magazinesCount += magazinesCount;
                magazinesCount = 0;
            }
            else if(currentType == GunType.rifle)
            {
                RifleShootManager.magazinesCount += magazinesCount;
                magazinesCount = 0;
            }
        }
        else
        {
            magazinesCount = 0;
        }
    }
    void TakeGun()
    {
        if(changingGun != currentType.ToString())
        {
           if(changingGun == GunType.pistol.ToString() && !takedGuns.Contains(GunType.pistol))
           {
                takedGuns.Add(GunType.pistol);
                SwitchToPistol();
           }
           if(changingGun == GunType.rifle.ToString() && !takedGuns.Contains(GunType.rifle))
           {
                takedGuns.Add(GunType.rifle); 
                SwitchToRifle();
            }
        }
    }
    void SwitchGun()
    {
        bool isRifleChoosed = Input.GetKeyUp(KeyCode.Alpha1) && takedGuns.Contains(GunType.rifle);
        bool isPistolChoosed = Input.GetKeyUp(KeyCode.Alpha2) && takedGuns.Contains(GunType.pistol);
        bool isHandChoosed = Input.GetKeyUp(KeyCode.Alpha3);

        if(isRifleChoosed)
        {
            SwitchToRifle();
        }
        if(isPistolChoosed)
        {
            SwitchToPistol();
        }
        if(isHandChoosed)
        {

        }
    }
    void SwitchToPistol()
    {
        currentType = GunType.pistol;
        RifleShootManager.gameObject.SetActive(false);
        PistolShootManager.gameObject.SetActive(true);  
    }
    void SwitchToRifle()
    {
        currentType = GunType.rifle;
        RifleShootManager.gameObject.SetActive(true);
        PistolShootManager.gameObject.SetActive(false);
    }
    void SwitchToHand()
    {
        currentType = GunType.None;
        RifleShootManager.gameObject.SetActive(false);
        PistolShootManager.gameObject.SetActive(false);  
    }
    void HandSettings()
    {
        if(currentType == GunType.None)
        {
            bulletsText.enabled = false;
            magazineText.enabled = false;
        }
        else
        {
            bulletsText.enabled = true;
            magazineText.enabled = true; 
        }
    }
}
