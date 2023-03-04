using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherController : MonoBehaviour

{
    [Header("Timer")]
    private float timer;
    public float timerMin;
    public float timerMax;

    [Header("Push")]
    public float pushDistance;
    public float pushSpeed;
    public float retractSpeed;
    public float timeExtended;

    private Vector3 initialPos;
    private Vector3 pushPos;
    private bool isPushing;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(PushTime());
    }

    private void PickTime()
    {
        timer = Random.Range(timerMin, timerMax);
    }

    private void Push()
    {
        pushPos = initialPos + transform.up * pushDistance;
        isPushing = true;
    }

    private IEnumerator Retract()
    {
        yield return new WaitForSeconds(timeExtended);
        transform.position = initialPos;
        isPushing = false;

        float timeToPush = pushDistance / retractSpeed;
        float t = 0f;

        while(t <= timeToPush)
        {
            transform.position = Vector3.Lerp(pushPos, initialPos, t / timeToPush);
            t += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(PushTime());
    }

    private IEnumerator PushTime()
    {
        PickTime();
        yield return new WaitForSeconds(timer);

        Push();
        float timeToPush = pushDistance / pushSpeed;
        float t = 0f;

        while (isPushing && t <= timeToPush)
        {
            transform.position = Vector3.Lerp(initialPos, pushPos, t / timeToPush);
            t += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Retract());
    }
}

