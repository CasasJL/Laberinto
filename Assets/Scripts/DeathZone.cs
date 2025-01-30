using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FallingCeiling fallingCeiling;

    private bool firstTimeActivation = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (!firstTimeActivation)
            {
                firstTimeActivation = true;
                gameManager.CloseDoorRemotelly(3);
                gameManager.CloseDoorRemotelly(4);
                fallingCeiling.Fall();
            }
        }
    }
}
