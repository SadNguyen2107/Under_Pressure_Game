using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip menuButtonProgressSound;
    [SerializeField] private AudioClip menuButtonEndSound;
    [SerializeField] private AudioClip hitBall;

    private AudioSource audioSource;

    private void Awake()
    {
        InitializeComponents();
    }

    //! Initialization
    private void InitializeComponents()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void StopWaterWalkingSound()
    {
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    public void PlayMenuButtonProgressSound()
    {
        audioSource.PlayOneShot(menuButtonProgressSound);
    }

    public void PlayMenuButtonEndSound()
    {
        audioSource.PlayOneShot(menuButtonEndSound);
    }

    public void PlayHitBallSound()
    {
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(hitBall);
    }
}
