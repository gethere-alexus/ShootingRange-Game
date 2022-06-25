using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 1f;
    void Start() 
    {
        StartCoroutine(DeleteAfterSceondDelay());    
    }
    void FixedUpdate() 
    {
        transform.Translate(Vector3.forward * speed);
    }
    IEnumerator DeleteAfterSceondDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
