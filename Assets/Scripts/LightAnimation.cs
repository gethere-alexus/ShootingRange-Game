using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    private Light lightComponent;

    void Start() 
    {
        lightComponent = this.gameObject.GetComponent<Light>();
        StartCoroutine(OfOnAnimation());
    }
    IEnumerator OfOnAnimation()
    {
        float timeDelay = Random.Range(5 , 10);
        int blincCount = Random.Range(4 , 8);

        yield return new WaitForSeconds(timeDelay);

        for (int i = 0; i < blincCount; i++)
        {
            lightComponent.enabled = !lightComponent.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        
        lightComponent.enabled = true;
        StartCoroutine(OfOnAnimation());
    }
}
