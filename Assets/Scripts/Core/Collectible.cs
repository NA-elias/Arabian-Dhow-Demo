using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Assign in Inspector
    public ParticleSystem collectEffect; // Assign in Inspector
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            if (collectSound)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);

            if (collectEffect)
                Instantiate(collectEffect, transform.position, Quaternion.identity);

            // You can send an event to update the score here
            Destroy(gameObject);
        }
    }
}
