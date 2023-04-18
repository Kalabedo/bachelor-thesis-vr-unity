using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootHoop : MonoBehaviour
{
    public AudioClip sound;

    public static event Action<int> ScoredPoints = delegate{ };

    public float coolDownTime = 1f; 
    public float resetHoop = 1f;
    private bool canActivate = true;
    private bool fromBottom = false;


    public void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null && rb.velocity.y > 0)
        {
            //ball enters from below
            fromBottom = true;
            StartCoroutine(ResetFromBottom());
        }

        if (rb != null && rb.velocity.y < 0 && canActivate && !fromBottom)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
            ScoredPoints(2);
            // deactivate trigger
            canActivate = false;

            // Coroutine start, to count cooldown time
            StartCoroutine(ResetActivation());
        }
    }

    IEnumerator ResetActivation()
    {
        yield return new WaitForSeconds(coolDownTime);
        canActivate = true;
    }

    IEnumerator ResetFromBottom()
    {
        yield return new WaitForSeconds(resetHoop);
        fromBottom = false;
    }
}
