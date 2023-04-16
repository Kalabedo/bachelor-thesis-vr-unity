using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallSpawnScenario1u2 : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(objectToBeSpawned, spawnPoints[i].position, RandomQuaternion(), spawnPoints[i]);
            // SphereCollider collider = objectsToBeSpawned[randomNumber].AddComponent<SphereCollider>();
            // collider.radius = 0.5f;
        }
        objectToBeSpawned.SetActive(false);
        
    }

    Quaternion RandomQuaternion()
    {
    double x, y, z, u, v, w, s;
    do { x = UnityEngine.Random.Range(-1f, 1f); y = UnityEngine.Random.Range(-1f, 1f); z = x * x + y * y; } while (z > 1);
    do { u = UnityEngine.Random.Range(-1f, 1f); v = UnityEngine.Random.Range(-1f, 1f); w = u * u + v * v; } while (w > 1);
    s = Mathf.Sqrt((float)((1 - z) / w));
    return new Quaternion((float)x, (float)y, (float)(s * u), (float)(s * v));
    }
}
