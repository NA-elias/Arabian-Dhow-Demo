using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
     public float boostAmount = 20f;
    public float boostDuration = 3f;
    public AudioClip boostSound;
    public ParticleSystem boostEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (boostSound)
                AudioSource.PlayClipAtPoint(boostSound, transform.position);

            if (boostEffect)
                Instantiate(boostEffect, transform.position, Quaternion.identity);

            other.GetComponent<PlayerMovement>().ApplyBoost(boostAmount, boostDuration);
        }
    }
}
