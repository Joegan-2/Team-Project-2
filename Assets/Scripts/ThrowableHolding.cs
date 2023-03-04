using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHolding : MonoBehaviour //This script is for showing the throwable is being held
{
    void Start()
    {
        gameObject.SetActive(false); //makes the object invisible
    }

    public void HoldingDisc()
    {
        gameObject.SetActive(true); //referenced in the ProjectileController when the object is picked up
    }

    public void ThrewDisc()
    {
        gameObject.SetActive(false); //turns the object off when the player throws the projectile   
    }

}
