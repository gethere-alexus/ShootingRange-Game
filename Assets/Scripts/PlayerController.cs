using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 0.1f;
    private bool isBoostAvaible = true;
    private bool isOnGround = true;
    private float boost = 1.0f;
    private float _coolDown = 0;
    private float boostMultiply = 1.3f;
    private float coolDownCount = 1.5f;
    private Rigidbody _playerRb;
    [SerializeField]private Animator _mainAnimator;
    void Start() 
    {
        _playerRb = GetComponent<Rigidbody>();    
    }
    void FixedUpdate() 
    {
        boost = ReturnCalculatedBoost(boost , boostMultiply  , isBoostAvaible);

        MovePlayer();

    }
    private float ReturnCalculatedBoost(float boost , float boostMultiply , bool isBoostAvaible)
    {
        CoolDownControll();
        if(Input.GetKey(KeyCode.LeftShift) && isBoostAvaible)
        {
            return Mathf.Clamp(boost * boostMultiply , 1 , 1.8f);
        }
        else
        {
            return 0.8f;
        }
    }
    public void MovePlayer()
    {
        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
            _playerRb.AddForce(Vector3.up * 6 , ForceMode.Impulse);
            isBoostAvaible = false;
            isOnGround = false;
        }

        var horizontalInput = ReturnInput("Horizontal" , _speed , boost);
        var verticalInput = ReturnInput("Vertical" , _speed , boost);


        transform.Translate(Vector3.forward * verticalInput);
        transform.Translate(Vector3.right * horizontalInput);
    }
    private float ReturnInput(string type , float _speed , float boost)
    {
        if(type == "Horizontal")
        {
            return Input.GetAxis("Horizontal") * _speed * boost;
        }
        else if (type == "Vertical")
        {
            return Input.GetAxis("Vertical") * _speed * boost;
        }
        else
        {
           Debug.LogError("Invalid type of input (Avaible only : Vertical , Horizontal) \n 0 returned");
           return 0;
        }
    }
    private void CoolDownControll()
    {
        if(boost > 1)
        {
            _coolDown += 0.01f;
        }
        else 
        {
            _coolDown -= 0.002f;
        }

        if(_coolDown >= coolDownCount)
        {
            isBoostAvaible = false;
        }
        else if(_coolDown <= 0)
        {
            _coolDown = 0;
            isBoostAvaible = true;
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isBoostAvaible = true;
        }
    }

}
