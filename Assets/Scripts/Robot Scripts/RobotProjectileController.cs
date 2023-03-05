using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectileController : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Transform player;
    public GameObject playerGO;
    public GameObject Robot;
    RobotController robotController;
    ProjectileController playerController;

    public AudioSource SFX;
    public AudioClip LaunchSFX;
    public AudioClip StunnedSFX;
    [Header("Throwing")]
    public float throwForce; //how hard horizontally the object is thrown
    public float throwUpwardForce; // how much vertical force is applied
    public float timer = 4f;
    public float cooldown = 3f;

    [Header("Rotation")]
    float rotationSpeed = 10f;




    void Awake()
    {
        robotController = Robot.GetComponent<RobotController>();
        playerController = playerGO.GetComponent<ProjectileController>();
        SFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        LookAtTarget();
    }

    private void Throw()
    {
        if (robotController.IsStunned == false || playerController.isSafe == false)
        {
            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, transform.rotation); //summons the projectile at the spawn point

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = transform.forward * throwForce + transform.up * throwUpwardForce;

            projectileRb.AddForce(forceToAdd, ForceMode.Impulse); //pushes the disc 

            SFX.PlayOneShot(LaunchSFX);

            StartCoroutine(Timer());
        }
    }
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);

        Throw();
    }

    IEnumerator StunCooldown()
    {
        Debug.Log(robotController.IsStunned);
        yield return new WaitForSeconds(cooldown);
        robotController.IsStunned = false;
        Debug.Log(robotController.IsStunned);
        StartCoroutine(Timer());
    }

    private void LookAtTarget()
    {
        if (robotController.IsStunned == false || playerController.isSafe == false)
        {
            // Calculate the direction towards the player
            Vector3 direction = player.position - transform.position;

            // Calculate the rotation needed to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Smoothly rotate the object towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            robotController.IsStunned = true;
            SFX.PlayOneShot(StunnedSFX);
            StartCoroutine(StunCooldown());
            StopCoroutine(Timer());
            Destroy(other.gameObject);
        }
    }

    public void Start()
    {
        StartCoroutine(Timer());
        robotController.IsStunned = false;
    }
}
