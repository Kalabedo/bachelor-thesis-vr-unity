using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public AudioClip floorSound;
    public AudioClip rimShot;
    public AudioClip backboardShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "court" || collision.gameObject.name == "Walls" || collision.gameObject.name == "courtFloor" || collision.gameObject.name == "Plane.003" || collision.gameObject.name == "Cube.002")
        {
            AudioSource.PlayClipAtPoint(floorSound, transform.position);
        }

        else if (collision.gameObject.name == "Circle")
        {
            AudioSource.PlayClipAtPoint(rimShot, transform.position);
        }

        else if(collision.gameObject.name == "Cube")
        {
            AudioSource.PlayClipAtPoint(backboardShot, transform.position);
        }
    }
}
