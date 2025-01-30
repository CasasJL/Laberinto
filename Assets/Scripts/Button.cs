using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int targetDoorId;

    private bool isPressed;

    public void Press() {
        if (!isPressed)
        {
            isPressed = true;
            gameManager.OpenDoorRemotelly(targetDoorId);

            transform.position = transform.position - new Vector3(-0.1f, 0, 0);
        }
    }
}
