using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ps4PlayerControll : MonoBehaviour
{
    Vector2 i_movement;
    float moveSpeed = 10f;

    ShapeController shiftShape;

    // Start is called before the first frame update
    void Start()
    {
        shapes[2].SetActive(false);
        shapes[1].SetActive(false);
        shapes[0].SetActive(false);
        shapes[3].SetActive(false);

        randomShapeNumber = Random.Range(0, 4);

        shapes[randomShapeNumber].SetActive(true);

        leftTriggerAction.performed += _ => ShiftToSquare();
        leftShoulderAction.performed += _ => ShiftToCircle();
        rightShoulderAction.performed += _ => ShiftToTriangle();
        rightTriggerAction.performed += _ => ShiftToStar();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector2 movement = new Vector2(i_movement.x, i_movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnMove(InputValue value)
    {
        i_movement = value.Get<Vector2>();
    }

    private void OnJump()
    {
        
    }

    private void OnSquare()
    {
        shiftShape.ShiftToSquare();
    }

    private void OnCircle()
    {
        shiftShape.ShiftToCircle();
    }
    private void OnTriangle()
    {
        shiftShape.ShiftToTriangle();
    }
    private void OnStar()
    {
        shiftShape.ShiftToStar();
    }

    [SerializeField] private GameObject[] shapes;
    private int randomShapeNumber;
    private InputAction leftTriggerAction, leftShoulderAction, rightShoulderAction, rightTriggerAction;

    private void Awake()
    {
        leftTriggerAction = new InputAction("leftTrigger");
        leftShoulderAction = new InputAction("leftShoulder");
        rightShoulderAction = new InputAction("rightShoulder");
        rightTriggerAction = new InputAction("rightTrigger");

        leftTriggerAction.AddBinding("<Gamepad>/leftTrigger");
        leftShoulderAction.AddBinding("<Gamepad>/leftShoulder");
        rightShoulderAction.AddBinding("<Gamepad>/rightShoulder");
        rightTriggerAction.AddBinding("<Gamepad>/rightTrigger");
    }

    private void OnEnable()
    {
        leftTriggerAction.Enable();
        leftShoulderAction.Enable();
        rightShoulderAction.Enable();
        rightTriggerAction.Enable();
    }

    private void OnDisable()
    {
        leftTriggerAction.Disable();
        leftShoulderAction.Disable();
        rightShoulderAction.Disable();
        rightTriggerAction.Disable();
    }

    public void ShiftToSquare()
    {
        shapes[0].SetActive(true);
        shapes[1].SetActive(false);
        shapes[2].SetActive(false);
        shapes[3].SetActive(false);
    }

    public void ShiftToCircle()
    {
        shapes[1].SetActive(true);
        shapes[0].SetActive(false);
        shapes[2].SetActive(false);
        shapes[3].SetActive(false);
    }

    public void ShiftToTriangle()
    {
        shapes[2].SetActive(true);
        shapes[0].SetActive(false);
        shapes[1].SetActive(false);
        shapes[3].SetActive(false);
    }

    public void ShiftToStar()
    {
        shapes[3].SetActive(true);
        shapes[0].SetActive(false);
        shapes[1].SetActive(false);
        shapes[2].SetActive(false);
    }
}
