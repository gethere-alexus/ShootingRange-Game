using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootingManager : MonoBehaviour
{
    [SerializeField]private GunManager _gunManager;
    [SerializeField]private Animator _mainAnimator;
    [SerializeField]private GameObject _gunBodyObj;
    [SerializeField]private GameObject _gunMuzzleObj;
    [SerializeField]private GameObject _bulletPrefab;
    [SerializeField]private int magazineCount = 5;
    [SerializeField]private int inClipBulletsCount = 30;
    [SerializeField]private int bulletsCount = 30;
    
    [SerializeField]private float shootingCoolDown = 0.08f;
    [SerializeField]private float reloadingTime = 3.0f;

    [HideInInspector]public float magazinesCount;
    private bool isCanScoped;
    private bool isReloading;
    private bool isCoolDCompleted = true;
    private float bulletCount;
    private float afterReloadingTime = 0.5f;

    private int scoppedFOV = 35;
    private int unscoppedFOV = 55;

    private GameObject _scopeedOffset;
    private GameObject _unscopeedOffset;
    private TextMeshProUGUI bulletsText;
    private TextMeshProUGUI magazineText;
    private Camera _camera;

    void Start() 
    {
        _mainAnimator = _gunBodyObj.GetComponent<Animator>();
        _camera = _gunManager._camera;
        magazineText = _gunManager.magazineText;
        bulletsText = _gunManager.bulletsText;
        _unscopeedOffset = _gunManager._unscopeedOffset;
        _scopeedOffset = _gunManager._scopeedOffset;


        bulletCount = bulletsCount;
        magazinesCount = magazineCount;

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

        bulletCount = bulletsCount;
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
            string bulletUIText = bulletCount + "/" + inClipBulletsCount;
            bulletsText.text = bulletUIText;

            magazineText.text = magazinesCount.ToString();

    }

}
