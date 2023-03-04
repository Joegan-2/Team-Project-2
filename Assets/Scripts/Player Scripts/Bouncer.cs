using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Launch Variables")]
    public float bounceForce = 30f; // variable for bounce force
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
            Debug.Log(hitForce);

            rb.AddForce(Vector3.up * (bounceForce + (hitForce / 3)), ForceMode.Impulse);
            
        }
    }
}