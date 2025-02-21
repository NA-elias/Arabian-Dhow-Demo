using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
   public float normalSpeed = 10f;
    public float turnSpeed = 50f;
    [SerializeField] private float currentSpeed;
    private Rigidbody rb;
    private bool isBoosted = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate() // More efficient for physics updates
    {
        // Move forward
        rb.linearVelocity = transform.forward * currentSpeed;

        // Turn left/right
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
    }

    public void ApplyBoost(float boostAmount, float duration)
    {
        if (!isBoosted)
            StartCoroutine(BoostCoroutine(boostAmount, duration));
    }

    private System.Collections.IEnumerator BoostCoroutine(float boostAmount, float duration)
    {
        isBoosted = true;
        currentSpeed += boostAmount;
        yield return new WaitForSeconds(duration);
        currentSpeed = normalSpeed;
        isBoosted = false;
    }
}
