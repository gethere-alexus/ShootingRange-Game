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
    [SerializeField] private Animator _mainAnimator;
    private bool isCoolDCompleted = true;
    private bool isReloading;
    private float bulletCount;

    void Start() 
    {
        _mainAnimator = _gunBodyObj.GetComponent<Animator>();
        bulletCount = 30;
        isReloading = false;
    }
    void Update() 
    {
        bulletsText.text = bulletCount + "/30";

        if(Input.GetMouseButton(1))
        {
            _gunBodyObj.transform.position =  _scopeedOffset.transform.position;
        }
        else
        {
            _gunBodyObj.transform.position = _unscopeedOffset.transform.position;
        }
        if(Input.GetMouseButton(0)  && bulletCount != 0 && isCoolDCompleted && !isReloading)
        {
            Vector3 position = _gunMuzzleObj.transform.position; 
            Instantiate(_bulletPrefab , position , _gunMuzzleObj.transform.rotation);
            bulletCount--;
            isCoolDCompleted = false;
            StartCoroutine(WaitForCoolDown());
        }

        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            _mainAnimator.SetBool("isPlayerReload" , true);
            StartCoroutine(WaitForReloading());
        }
    }
    IEnumerator WaitForCoolDown()
    {
        yield return new WaitForSeconds(0.08f);
        isCoolDCompleted = true;
    }
    IEnumerator WaitForReloading()
    {
        _mainAnimator.CrossFade("ReloadAnim" , 0.1f);
        yield return new WaitForSeconds(3);
        _mainAnimator.SetBool("isPlayerReload" , false);
        bulletCount = 30;
        yield return new WaitForSeconds(0.5f);
        isReloading = false;
    }
}
