using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Launch Variables")]
    public float bounceForce = 30f; // variable for bouncer force
    public float pushForce= 30f; // variable for pusher force
    public float enemyPushForce = 30f; //projectile knockback force


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
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerScript.UsedBouncer();
        }
        else if (collision.gameObject.CompareTag("Pusher"))
        {
            Vector3 direction = collision.gameObject.transform.up;

            rb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
        else if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Debug.Log("Hit");
            Vector3 direction = collision.contacts[0].point - (Vector3)transform.position;
            direction = -direction.normalized;
            rb.AddForce(direction * enemyPushForce, ForceMode.Impulse);
            Destroy(collision.gameObject);
        }
    }
}