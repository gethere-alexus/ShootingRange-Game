using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private TakeGunController takeGunController; 
    void Update()
    {

        Ray ray = new Ray(_camera.position , _camera.forward);
        Debug.DrawRay(_camera.position , _camera.forward*10 , Color.green);

        RaycastHit hit;

        if(Physics.Raycast(ray , out hit))
        {
            if(hit.collider.gameObject.CompareTag("Gun") && hit.distance < 4)
            {
               takeGunController = hit.collider.gameObject.GetComponent<TakeGunController>();
               takeGunController.OnGunEnter();
               takeGunController.OnGunStay();
            }
            else
            {
                if(takeGunController != null)
                {
                    takeGunController.OnGunExit();
                }
            }
        }
    }
}
