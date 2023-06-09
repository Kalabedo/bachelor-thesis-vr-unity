using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallThrow : MonoBehaviour
{
    private string[] sceneNames = {"Scenario1_1", "Scenario1_1Quest", "Scenario1_2", "Scenario1_2Quest", "Scenario2_1", "Scenario2_1Quest", "Scenario2_2", "Scenario2_2Quest", "Scenario3_1", "Scenario3_1Quest", "Scenario3_2", "Scenario3_2Quest"};
    
    public float despawnDelay = 5f; // delay in seconds for respawn/despawn
    private Vector3 player = Vector3.zero;

    private Rigidbody rigidBody;
    private XRGrabInteractable grabInteractable;

    private Transform parentTransform; // transform des parents => spawnpunkt
    private static int ballCounter = 0;
    private bool isRespawning = false;
    private bool isGrabbed = false;
    private float throwDistance = 0f;
    private float throwThreshold = 2f;

    private void Start()
    {
        parentTransform = transform.parent;
        rigidBody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRespawning)
        {
            if (collision.gameObject.CompareTag("HandPoseEnvelope(Clone)"))
            {
                // respawn if the ball was hit by another ball and rolled away
                Respawn();
            }
        }
    }

    private void Update()
    {
        if (isGrabbed)
        {
            // check throw distance while the ball is grabbed
            throwDistance = Vector3.Distance(transform.position, player);
        }

        if (!isRespawning)
        {
            if (throwDistance > throwThreshold)
            {
                // disable grab if ball is thrown over threshold distance
                grabInteractable.enabled = false;
                Respawn();
            }
        }
    }

    public void OnThrowStarted()
    {
        isGrabbed = true;
        player = transform.position;
    }

    public void OnThrowCompleted()
    {
        isGrabbed = false;
        ballCounter++;

        if (ballCounter == 5)
        {
            // load next scene if 5 balls have been thrown
            LoadNextScene();
        }
        else
        {
            // respawn ball if it was thrown below threshold distance
            if (throwDistance < throwThreshold)
            {
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        isRespawning = true;
        grabInteractable.enabled = true;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
        StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(despawnDelay);
        isRespawning = false;
    }

    public void LoadNextScene()     //scene loader
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (currentSceneIndex == sceneNames.Length)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}


