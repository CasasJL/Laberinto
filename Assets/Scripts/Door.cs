using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] private int doorId;
    [SerializeField] private bool canOpenManually;
    [SerializeField] private bool initiallyOpen;

    private Vector3 initialPosition;
    private bool isOpen;
    private bool isOpening;
    private bool isClosing;

    private void Start()
    {
        initialPosition = transform.position;

        if (initiallyOpen) Open(); 
    }

    private void Update()
    {
        if (isOpening)
        {
            Vector3 targetPosition = initialPosition - new Vector3(3.45f, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);

            if (transform.position.x <= targetPosition.x)
            {
                transform.position = targetPosition;
                isOpening = false;
                isOpen = true;
            }
        }

        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, 2 * Time.deltaTime);

            if (transform.position.x >= initialPosition.x)
            {
                transform.position = initialPosition;
                isClosing = false;
                isOpen = false;
            }
        }
    }

    void OnEnable()
    {
        gameManager.OnCloseDoorRemotelly += CloseRemotelly;
        gameManager.OnOpenDoorRemotelly += OpenRemotelly;
    }

    void OnDisable()
    {
        gameManager.OnCloseDoorRemotelly -= CloseRemotelly;
        gameManager.OnOpenDoorRemotelly -= OpenRemotelly;
    }

    private void Open() {
        if (!isOpen && !isOpening)
        {
            isOpening = true;
            soundManager.PlaySound(soundManager.openDoor, transform.position);
        }
    }

    public void OpenManually() {
        if (canOpenManually)
        {
            Open();
        }
        else
        {
            soundManager.PlaySound(soundManager.lockedDoor, transform.position);
        }
    }

    public void OpenRemotelly(int targetId)
    {
        if (doorId == targetId) Open();
    }

    public void CloseRemotelly(int targetId)
    {
        if (doorId == targetId) Close();
    }

    private void Close()
    {
        if (isOpen && !isClosing && !isOpening)
        {
            isClosing = true;
            soundManager.PlaySound(soundManager.openDoor, transform.position);
        }
    }
}
