using System.Collections;
using UnityEngine;
 
public class ClothTest : MonoBehaviour {

    public Cloth cloth;
    public GameObject[] spawnPunkte;
 
    private void Start() {

        var tmp = new CapsuleCollider[spawnPunkte.Length];

        for (int i = 0; i < spawnPunkte.Length; i++)
        {         
            tmp[i] = spawnPunkte[i].GetComponentInChildren<CapsuleCollider>();      //add capusle colliders to the instantiated balls for cloth interaction
        }
        
        cloth.capsuleColliders = tmp;

    }
}
