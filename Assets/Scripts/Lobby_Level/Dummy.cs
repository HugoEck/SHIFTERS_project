using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;

    private Vector2 _xVelocity = new Vector2(200,0);

    private bool _bFacingRight = false;
    private void Update()
    {       
        if(_bFacingRight)
        {            
            _rigidBody.velocity = -_xVelocity * Time.deltaTime;

            if(_rigidBody.position.x <= -5)
            {
                _bFacingRight= false;
            }
        }
        else if(!_bFacingRight)
        {
            
            _rigidBody.velocity = _xVelocity * Time.deltaTime;
            if(_rigidBody.position.x >= 9)
            {
                _bFacingRight = true;
            }
        }
    }
}
