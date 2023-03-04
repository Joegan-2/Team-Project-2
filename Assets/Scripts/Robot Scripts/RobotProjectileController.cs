using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectileController : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Transform player;

    [Header("Throwing")]
    public float throwForce; //how hard horizontally the object is thrown
    public float throwUpwardForce; // how much vertical force is applied
    public float timer = 4f;

    [Header("Rotation")]
    float rotationSpeed = 10f;

    void Start()
    {
        StartCoroutine(Timer());
    }

    private void Update()
    {
        LookAtTarget();
    }

    private void Throw()
    {
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, transform.rotation); //summons the projectile at the spawn point


        Vector3 forceDirection = transform.forward; //sets the direction the object will be pushed
        RaycastHit hit; 


        //the next few lines "calculate a triangle", because the object is summoned to the side of the player, it will be shot off center (which is bade). This sends a raycast directly in front of a player.
        //wherever it hits is where the line ends. It then "creates a triangle" based on the spawn point of the projectile and the beggining and end of the raycast. It then determines what force is requierd to
        // get the projectile to hit the end of the raycast, rather than head straight forward. (this means the projectile should hit almost exactly where the player aims)

        if(Physics.Raycast(transform.position, transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse); //pushes the disc 

        StartCoroutine(Timer());
    }
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);

        Throw();
    }

    private void LookAtTarget()
    {
        // Calculate the direction towards the player
        Vector3 direction = player.position - transform.position;

        // Calculate the rotation needed to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate the object towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
