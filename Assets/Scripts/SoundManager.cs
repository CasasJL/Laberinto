using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundManager", menuName = "Sound Manager", order = 2)]
public class SoundManager : ScriptableObject
{
    private AudioSource audioSource;
    public AudioClip openDoor;
    public AudioClip lockedDoor;
    public AudioClip victory;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
