using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMovement : MonoBehaviour
{

    public float speed = 0.1f;
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Sin(Time.time * speed) * 100;
    }
}
