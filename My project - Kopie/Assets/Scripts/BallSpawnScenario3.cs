using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallSpawnScenario3 : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToBeSpawned;
    [SerializeField] Transform[] spawnPoints;

    private List<int> usedIndexes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (objectsToBeSpawned.Length == 0)
        {
            Debug.LogError("No objects to be spawned!");
            return;
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int index;

            if (usedIndexes.Count == objectsToBeSpawned.Length)
            {
                usedIndexes.Clear(); // all balls have been spawned
            }

            do
            {
                // pick random index, that wasnt used already
                index = UnityEngine.Random.Range(0, objectsToBeSpawned.Length);
            } while (usedIndexes.Contains(index));

            usedIndexes.Add(index);

            Instantiate(objectsToBeSpawned[index], spawnPoints[i].position, RandomQuaternion(), spawnPoints[i]);

        }
        for (int i = 0; i < objectsToBeSpawned.Length; i++)
        {
            objectsToBeSpawned[i].SetActive(false);
        }
        
    }

    Quaternion RandomQuaternion()  //random rotation
    {
        double x, y, z, u, v, w, s;
        do { x = UnityEngine.Random.Range(-1f, 1f); y = UnityEngine.Random.Range(-1f, 1f); z = x * x + y * y; } while (z > 1);
        do { u = UnityEngine.Random.Range(-1f, 1f); v = UnityEngine.Random.Range(-1f, 1f); w = u * u + v * v; } while (w > 1);
        s = Mathf.Sqrt((float)((1 - z) / w));
        return new Quaternion((float)x, (float)y, (float)(s * u), (float)(s * v));
    }
}
