using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public AudioClip checkpointSound;
    public ParticleSystem checkpointEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // // Save checkpoint position
            // other.GetComponent<PlayerCheckpoint>().SetCheckpoint(transform.position);

            // Play sound and visual effect
            if (checkpointSound)
                AudioSource.PlayClipAtPoint(checkpointSound, transform.position);

            if (checkpointEffect)
                Instantiate(checkpointEffect, transform.position, Quaternion.identity);
        }
    }
}
