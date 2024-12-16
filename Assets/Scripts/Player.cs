using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpSpeed = 5;

    private CharacterController controller;
    private const float gravity = -9.81f;
    private const float groundedGravity = -2f;
    private Vector3 movement;
    private float moveY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveY = GetVerticalMovement();
        movement = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * movementSpeed;
        movement.y = moveY;
        controller.Move(movement * Time.deltaTime);
    }

    private float GetVerticalMovement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump")) return jumpSpeed;
            if (moveY < 0) return groundedGravity;
        }
        
        return moveY + gravity * Time.deltaTime;
    }
}
