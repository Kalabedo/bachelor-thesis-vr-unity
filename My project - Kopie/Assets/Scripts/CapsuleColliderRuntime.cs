using System.Collections;
using UnityEngine;
 
public class CapsuleColliderRuntime : MonoBehaviour {
 
    private void Start() 
    {
        Cloth cloth = GetComponent<Cloth>();
        var tmp = new CapsuleCollider[1];
        tmp[0] = GameObject.Find("HandPoseEnvelope(Clone)").GetComponent<CapsuleCollider>();
        cloth.capsuleColliders = tmp;
    }
}


