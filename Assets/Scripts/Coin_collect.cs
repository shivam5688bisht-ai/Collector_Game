using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // Optional: Reference to a sound effect to play upon collection
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // 1. Play audio if assigned
            if (collectSound != null)
            {
                // PlaySoundAtPoint prevents the sound from cutting off when the coin is destroyed
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            if (ScoreManager.Instance != null)
            {
            ScoreManager.Instance.AddScore(1);
            }
        }
    

        Destroy(gameObject);

    }
}


