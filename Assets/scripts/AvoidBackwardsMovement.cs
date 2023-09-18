using UnityEngine;

public class AvoidBackwardMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calculate the dot product of the collision normal and the object's forward direction
        // to check if the collision is coming from the back of the object
        float dotProduct = Vector3.Dot(collision.contacts[0].normal, transform.forward);

        if (dotProduct < 0f)
        {
            // If collision is from the back, set the object's velocity to zero
            rb.velocity = Vector3.zero;
            // Or apply an opposing force to counteract the collision force
            // rb.AddForce(-collision.impulse, ForceMode.Impulse);
        }
    }
}
