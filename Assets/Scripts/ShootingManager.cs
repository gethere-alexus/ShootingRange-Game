using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject _gunBodyObj;
    [SerializeField] private GameObject _gunMuzzleObj;
    [SerializeField] private GameObject _gunMagObj;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _scopeedOffset;
    [SerializeField] private GameObject _unscopeedOffset;
    
    [SerializeField] private TextMeshProUGUI bulletsText;
    [SerializeField] private TextMeshProUGUI magazineText;

    [SerializeField] private Animator _mainAnimator;
    [SerializeField] private Camera _camera;

    private bool isCoolDCompleted = true;
    [SerializeField]private bool isReloading;
    private bool isCanScoped;

    private float bulletCount;
    [HideInInspector]public float magazinesCount;
    private float shootingCoolDown = 0.08f;
    private float reloadingTime = 3.0f;
    private float afterReloadingTime = 0.5f;

    private int scoppedFOV = 35;
    private int unscoppedFOV = 55;
    private int rifleBulletsCount = 30;
    private int defaultMagazineCount = 5;

    void Start() 
    {
        _mainAnimator = _gunBodyObj.GetComponent<Animator>();

        bulletCount = rifleBulletsCount;
        magazinesCount = defaultMagazineCount;

        isReloading = false;
        isCanScoped = true;
    }
    void FixedUpdate() 
    {
        GunControlling();
        TextControlling();
    }
    IEnumerator WaitForCoolDown()
    {
        yield return new WaitForSeconds(shootingCoolDown);
        isCoolDCompleted = true;
    }
    IEnumerator WaitForReloading()
    {
        //If magazines != 0 player will reloading and one magazine will remove
        //If there are no mag player will not reloading , if no bullets magazine will fall from the gun Object;

        if(magazinesCount != 0)
        {
        isCanScoped = false;
        _mainAnimator.CrossFade("ReloadAnim" , 0.1f);

        yield return new WaitForSeconds(reloadingTime);

        _mainAnimator.SetBool("isPlayerReload" , false);
        bulletCount = rifleBulletsCount;
        magazinesCount--;

        yield return new WaitForSeconds(afterReloadingTime);

        isReloading = false;
        isCanScoped = true;
        }
        else
        {
            if(bulletCount == 0)
            {
            _mainAnimator.CrossFade("EmptyMag" , 0.1f);
            }
            else
            {
                isReloading = false;
            }
        }
    }
    private void ReloadController()
    {
        //Player will reloading for 3 seconds , while gun is reloading player cant shooting and scopping
        if(Input.GetKey(KeyCode.R) && !isReloading)
        { 
            isReloading = true;
            _mainAnimator.SetBool("isPlayerReload" , true);
            StartCoroutine(WaitForReloading());
        }
    }
    private void ScoppingController()
    {
        //If rigth mouse dont pressed FOV Camera is deafault(55) and gun position in rigth hand
        //Else every frame FOV++ while FOV != 37 , gun position in middle of screen;

        if(Input.GetMouseButton(1) && isCanScoped)
        {
            _gunBodyObj.transform.position = _scopeedOffset.transform.position;
            if(_camera.fieldOfView != scoppedFOV)
            {
                _camera.fieldOfView--;
            }
        }
        else
        {
            if(_camera.fieldOfView != unscoppedFOV)
            {
                _camera.fieldOfView++;
            }
            _gunBodyObj.transform.position = _unscopeedOffset.transform.position;
        }
    }
    private void ShootingController()
    {
        //When player pressed left mouse button , bullet will instantiate every 0.08 seconds
        //Coroutine is controlling that next bullet will fire after time interval 

        if(Input.GetMouseButton(0)  && bulletCount != 0 && isCoolDCompleted && !isReloading)
        {
            Vector3 position = _gunMuzzleObj.transform.position; 

            Instantiate(_bulletPrefab , position , _gunMuzzleObj.transform.rotation);

            bulletCount--;

            isCoolDCompleted = false;
            StartCoroutine(WaitForCoolDown());
        }       
    }
    private void GunControlling()
    {
        //Complexing all of methods
        ReloadController();
        ScoppingController();
        ShootingController();
    }
    private void TextControlling()
    {
            string bulletUIText = bulletCount + "/30";
            bulletsText.text = bulletUIText;

            magazineText.text = magazinesCount.ToString();

    }

}
