using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _bodyPerson;
    public float sensetivity;
    private float _xRotation = 0f;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate() 
    {
        var horizontalInput = Input.GetAxis("Mouse X") * sensetivity; 
        var verticalInput = Input.GetAxis("Mouse Y") * sensetivity;

        _xRotation -= verticalInput;
        _xRotation = Mathf.Clamp(_xRotation , -90 , 90);

        transform.localRotation = Quaternion.Euler(_xRotation , 0f , 0f);
        _bodyPerson.transform.Rotate(Vector3.up * horizontalInput);


    }
}
