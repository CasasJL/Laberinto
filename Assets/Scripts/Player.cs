using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float interactionMaxDistance;

    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 movement;
    private float moveX, moveY, moveZ, mouseX, mouseY;
    private float verticalRotation = 0f;
    private const float gravity = -9.81f;
    private const float groundedGravity = -2f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.E)) HandleInteraction();
    }

    private void HandleMovement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        moveY = GetVerticalMovement();

        movement = (moveZ * playerCamera.transform.forward + moveX * playerCamera.transform.right).normalized * movementSpeed;
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
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        transform.Rotate(0f, mouseX, 0f);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
    }

    private void HandleInteraction()
    {
        //Debug.DrawRay(transform.position, Vector3.forward * interactionMaxDistance, Color.red, 3);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionMaxDistance))
        {
            if (hit.transform.TryGetComponent(out Door door)) door.OpenManually();
            if (hit.transform.TryGetComponent(out Button button)) button.Press();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}