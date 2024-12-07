using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    public AudioClip monsterSound;   // Sound clip for the monster
    public float interval = 5f;     // Interval between sounds in seconds
    private AudioSource audioSource;

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
        if (audioSource != null && monsterSound != null)
        {
            audioSource.PlayOneShot(monsterSound);
        }
    }
}
