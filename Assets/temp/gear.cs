using UnityEngine;

public class gear : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // rb.MoveRotation(Quaternion.Euler(Vector3.up * 15f));
        // transform
    }

    void FixedUpdate()
    {
        // rb.AddForce(Vector3.forward * 25f, ForceMode.Force);
        // rb.AddTorque(Vector3.up * 25f, ForceMode.Acceleration);
    }
}
