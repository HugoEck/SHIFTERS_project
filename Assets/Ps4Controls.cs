using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ps4Controls : MonoBehaviour
{
    //PlayerControls controls;
    //
    //public float jumpStrength = 10f;
    //public float gravity = -9.81f;
    //Vector2 move;
    //Vector2 jump;
    //
    //
    //
    //void Awake()
    //{
    //    controls = new PlayerControls();
    //
    //    controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
    //    controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    //    controls.Gameplay.Jump.performed += ctx => Jump();
    //    //controls.Gameplay.Jump.canceled += ctx => jump = Vector2.zero;
    //}
    //
    //
    //void Update()
    //{
    //    Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
    //    transform.Translate(m, Space.World);
    //
    //    //Vector2 j = new Vector2(jump.x, jump.y) * Time.deltaTime;
    //    //transform.Translate(j, Space.World);
    //}
    //
    //void Jump()
    //{
    //    // Calculate the jump velocity
    //    Vector3 jumpVelocity = Vector2.up * jumpStrength;
    //
    //    // Apply the jump velocity to the rigidbody
    //    GetComponent<Rigidbody>().velocity = jumpVelocity;
    //
    //    // Apply gravity to the rigidbody
    //    GetComponent<Rigidbody>().AddForce(Vector2.up * gravity, ForceMode.Acceleration);
    //}
    //void OnEnable()
    //{
    //    controls.Gameplay.Enable();
    //}
    //void OnDisable()
    //{
    //    controls.Gameplay.Disable();
    //}

}
