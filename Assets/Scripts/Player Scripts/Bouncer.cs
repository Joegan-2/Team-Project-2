using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Launch Variables")]
    public float bounceForce = 30f; // variable for bouncer force
    public float pushForce= 30f; // variable for pusher force
    private float hitForce;
    private float maxReboundForce = 15f;

    [Header("Object Info")]
    public Transform orientation; // public transform variable to save the player's orientation
    private Rigidbody rb;
    private PlayerMovementAdvanced playerScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        playerScript = gameObject.GetComponent<PlayerMovementAdvanced>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // detect collision between the player and bouncer
        if (collision.gameObject.CompareTag("Bouncer") && collision.gameObject.transform.position.y < orientation.position.y)
        {
            playerScript.UsedBouncer();
            hitForce = collision.impulse.magnitude;

            if(hitForce > maxReboundForce) hitForce = maxReboundForce;

            rb.AddForce(Vector3.up * (bounceForce + (hitForce / 3)), ForceMode.Impulse);
            
        }

        else if (collision.gameObject.CompareTag("Pusher"))
        {
            Vector3 direction = collision.gameObject.transform.up;

            rb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
    }
}