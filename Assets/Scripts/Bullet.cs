using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 2.5f;
    private float deleteTimeInterval = 2f;
    void Start() 
    {
        StartCoroutine(DeleteAfterSceondDelay());    
    }
    void FixedUpdate() 
    {
        transform.Translate(Vector3.forward * speed);
    }
    void LateUpdate() 
    {
        Ray ray = new Ray(transform.localPosition , transform.forward);
        Debug.DrawLine(transform.localPosition , transform.forward);     
    }
    IEnumerator DeleteAfterSceondDelay()
    {
        yield return new WaitForSeconds(deleteTimeInterval);
        Destroy(this.gameObject);
    }

}
