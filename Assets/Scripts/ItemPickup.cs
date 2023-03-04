using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Floating")]
    public float amplitude = 0.1f; // Amplitude of the wave
    public float frequency = 1f; // Frequency of the wave
    public float speed = 1f; // Speed of the floating motion
    private Vector3 startPos; // Starting position of the object

    [Header("Rotating")]
    public float x = 0; //rotation of the object for each axis
    public float y = 45;
    public float z = 10;

    void Start()
    {
        startPos = transform.position; //saves the original position of the object
    }
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime); //makes the object rotate

        float newY = Mathf.Sin(Time.time * speed * Mathf.PI * frequency) * amplitude + startPos.y; //calculates the floating motion

        transform.position = new Vector3(transform.position.x, newY, transform.position.z); //changes the position of the object based on the sin wave calculation
    }
}