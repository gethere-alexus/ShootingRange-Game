using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script was made by Mousecach , but get some changing from me.
// http://unity3d.ru/distribution/viewtopic.php?f=18&t=23184
public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToDestruct = 10f;
    [SerializeField] private bool RandomDamage = true;
    [SerializeField] private float Damage = 20.0f;
    [SerializeField] private float minRandLimit = -5.0f;
    [SerializeField] private float maxRandLimit = 5.0f;
    [SerializeField] private float StartPointOfDamageReduction = 20;
    [SerializeField] private float FinalDamageInPercent = 20;

    // Разобрать !
    [SerializeField] private AnimationCurve DamageReductionGraph;
    [SerializeField] private int StartSpeed = 50;
    [SerializeField] private GameObject particleHit;

    Vector3 PreviousStep;
    float StartTime;
    float CurrentDamage;
    Rigidbody bulletRb;

    void Awake() 
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();

        Destroy(gameObject , timeToDestruct);

        bulletRb.velocity = transform.TransformDirection(Vector3.forward * StartSpeed);

        PreviousStep = gameObject.transform.position;
        StartTime = Time.time;

        CurrentDamage = Damage;
        if (RandomDamage)
        {
            CurrentDamage += Random.Range(minRandLimit , maxRandLimit);
        }
        
        //Разобрать !
        Keyframe[] ks;
        ks = new Keyframe[3];

        ks[0] = new Keyframe(0 , 1);
        ks[1] = new Keyframe(StartPointOfDamageReduction / 100 , 1);
        ks[2] = new Keyframe(1 , FinalDamageInPercent / 100);

        DamageReductionGraph = new AnimationCurve(ks);
    }
    void FixedUpdate() 
    {
        Quaternion currentStep = gameObject.transform.rotation;

        transform.LookAt(PreviousStep , transform.up);

        RaycastHit hit = new RaycastHit();

        float distance = Vector3.Distance(PreviousStep , transform.position);
        if(distance == 0.0f)
        {
            distance = 1e-05f;
        }
        Debug.DrawLine(PreviousStep , transform.TransformDirection(Vector3.back) * distance * 0.9999f);
        
        if(Physics.Raycast(PreviousStep , transform.TransformDirection(Vector3.back), out hit , distance * 0.9999f) && (hit.transform.gameObject != gameObject))
        {
            Instantiate(particleHit , hit.point , Quaternion.FromToRotation(Vector3.up , hit.normal));
            SendDamage(hit.transform.gameObject);
        }

        gameObject.transform.rotation = currentStep;

        PreviousStep = gameObject.transform.position;
    }
    void SendDamage(GameObject Hit)
    {
    Hit.SendMessage("ApplyDamage" , CurrentDamage * GetDamageCoefficient() , SendMessageOptions.DontRequireReceiver);
    Destroy(gameObject);
    }
    float GetDamageCoefficient()
    {
        float value = 1.0f;
        float currentTime =  Time.time - StartTime;
        value = DamageReductionGraph.Evaluate(currentTime / timeToDestruct);
        print(value);
        return value;
    }
}
