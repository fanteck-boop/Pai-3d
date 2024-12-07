using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    public AudioClip monsterSound;   // Sound clip for the monster
    public float interval = 5f;     // Interval between sounds in seconds
    private AudioSource audioSource;
    private bool isAlive = true;    // Tracks if the monster is alive

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the clip and play the sound at intervals
        audioSource.clip = monsterSound;
        InvokeRepeating(nameof(PlayMonsterSound), 0f, interval);
    }

    void PlayMonsterSound()
    {
        if (isAlive && audioSource != null && monsterSound != null)
        {
            audioSource.PlayOneShot(monsterSound);
        }
    }

    // Public method to stop the sound when the monster dies
    public void StopMonsterSound()
    {
        isAlive = false;
        CancelInvoke(nameof(PlayMonsterSound));
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
