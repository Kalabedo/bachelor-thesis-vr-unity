using UnityEngine;

public class ThrowDetector : MonoBehaviour
{
    private int ballCount;

    private void Start()
    {
        ballCount = 0;
    }

    public void IncrementBallCount()
    {
        ballCount++;

        if (ballCount >= 5)
        {
            Debug.Log("Alle Bälle geworfen");
            ballCount = 0;
        }
    }

    public void OnTriggerEnter(Collision collision)
    {
        if(collision.gameObject.name == "HandPoseEnvelope")
        {
            Debug.Log("Hit");
        }
    }
}
