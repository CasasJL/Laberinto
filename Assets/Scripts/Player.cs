using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float mouseSensitivity = 500f;

    private CharacterController controller;
    private const float gravity = -9.81f;
    private const float groundedGravity = -2f;
    private Vector3 movement;
    private float moveX, moveY, moveZ, mouseX, mouseY;
    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        moveY = GetVerticalMovement();

        movement = (moveZ * Camera.main.transform.forward + moveX * Camera.main.transform.right).normalized * movementSpeed;
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


    private void HandleRotation()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(0f, mouseX, 0f);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); 

        Camera.main.transform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
    }
}