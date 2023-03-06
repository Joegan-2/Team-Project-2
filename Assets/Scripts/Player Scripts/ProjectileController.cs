using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public GameObject throwObject;
    private ThrowableHolding throwableHoldingScript;

    [Header("Settings")]
    public int totalThrows = 0; //how many pickups the object is throwing (right now it can only hold one, so this value should never go over 1)
    public bool isSafe;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0; //button to throw the projectile
    public float throwForce; //how hard horizontally the object is thrown
    public float throwUpwardForce; // how much vertical force is applied

    private bool readyToThrow;
    private AudioSource SFX;
    public AudioClip LaunchSFX;

    private void Start()
    {
        readyToThrow = false;
        throwableHoldingScript = attackPoint.gameObject.GetComponent<ThrowableHolding>();
        SFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0) //if the player presses the throw key and the conditions are correct, the throw function is called
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false; //sets it so the player cant throw again

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation); //summons the projectile at the spawn point

        throwableHoldingScript.ThrewDisc();

        Vector3 forceDirection = cam.transform.forward; //sets the direction the object will be pushed
        RaycastHit hit; 
        SFX.PlayOneShot(LaunchSFX);

        //the next few lines "calculate a triangle", because the object is summoned to the side of the player, it will be shot off center (which is bade). This sends a raycast directly in front of a player.
        //wherever it hits is where the line ends. It then "creates a triangle" based on the spawn point of the projectile and the beggining and end of the raycast. It then determines what force is requierd to
        // get the projectile to hit the end of the raycast, rather than head straight forward. (this means the projectile should hit almost exactly where the player aims)

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse); //pushes the disc

        totalThrows--; //sets the totalThrows to 0 (unless we decided to let the player carry multiple projectiles)
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ProjectilePickup")) //handles picking up the projectile 
        {
            if(!readyToThrow)
            {
                throwableHoldingScript.HoldingDisc();
                totalThrows = 1;
                readyToThrow = true;
                Destroy(collision.gameObject);
            }
        }
        if(collision.gameObject.CompareTag("SafeZone"))
        {
            isSafe = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
                if(collision.gameObject.CompareTag("SafeZone"))
        {
            isSafe = false;
        }
    }
}
