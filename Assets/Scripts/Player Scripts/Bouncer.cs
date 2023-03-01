using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Launch Variables")]
    public float bounceForce = 10f; // variable for bounce force
    public float angle = 90f;
    private Vector3 normalizedOrientation;
    private float hitForce;
    private float maxReboundForce = 15f;

    [Header("Object Info")]
    public Transform orientation; // public transform variable to save the player's orientation
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // detect collision between the player and bouncer
        if (collision.gameObject.CompareTag("Bouncer") && collision.gameObject.transform.position.y < orientation.position.y)
        {
            hitForce = collision.impulse.magnitude;
            if(hitForce > maxReboundForce) hitForce = maxReboundForce;

            normalizedOrientation = orientation.forward.normalized;
            Debug.Log(hitForce);
            Vector3 direction = Vector3.Slerp(normalizedOrientation, Vector3.up, angle / 90f);

            rb.AddForce(direction * (bounceForce + (hitForce / 3)), ForceMode.Impulse);
        }
    }
}