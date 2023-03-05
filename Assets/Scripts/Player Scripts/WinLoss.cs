using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinLoss : MonoBehaviour
{
    
    //preliminary code to introduce bools for winning and losing the game. both will call the menu, one to have a loss screen and the other a win screen

    public bool win = false;
    public bool lose = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LOSE")
        {
            lose = true;
        }

        if (collision.gameObject.tag == "EnemyProjectile")
        {
            lose = true;
        }

        if (collision.gameObject.tag == "WIN")
        {
            win = true;
        }
    }
}
