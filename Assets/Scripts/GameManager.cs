using System;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "NewGameManager", menuName = "Game Manager", order = 1)]
public class GameManager : ScriptableObject
{
    public event Action<int> OnOpenDoorRemotelly;
    public event Action<int> OnCloseDoorRemotelly;

    internal void OpenDoorRemotelly(int doorId)
    {
        OnOpenDoorRemotelly?.Invoke(doorId);
    }

    internal void CloseDoorRemotelly(int doorId)
    {
        OnCloseDoorRemotelly?.Invoke(doorId);
    }
}
