using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawn : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
