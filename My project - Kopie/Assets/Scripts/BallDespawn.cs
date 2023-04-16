using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class BallDespawn : MonoBehaviour
{
    private string[] sceneNames = {"Scenario1_1", "Scenario1_1Quest", "Scenario1_2", "Scenario1_2Quest", "Scenario2_1", "Scenario2_1Quest", "Scenario2_2", "Scenario2_2Quest", "Scenario3_1", "Scenario3_1Quest", "Scenario3_2", "Scenario3_2Quest"};
    
    public float despawnDelay = 5f; // Zeitverzögerung in Sekunden, bevor der Ball despawnt

    private bool hasCollided = false;

    private Rigidbody rigidBody;
    private XRGrabInteractable grabInteractable;
    ThrowDetector throwDetector;

    private bool isThrown = false; //  um zu überprüfen, ob der Ball geworfen wurde
    private bool isPickedUp = false; //  um zu überprüfen, ob der Ball aufgenommen wurde

    private Transform parentTransform; // Transform des Parents, an dem der Ball gespawnt werden soll
    private static int ballCounter = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        parentTransform = transform.parent;
    }

    private void OnEnable()
    {
        grabInteractable.onSelectExited.AddListener(OnThrow);
        grabInteractable.onSelectEntered.AddListener(OnPickup);
    }

    private void OnDisable()
    {
        grabInteractable.onSelectExited.RemoveListener(OnThrow);
        grabInteractable.onSelectEntered.RemoveListener(OnPickup);
    }

    private void OnThrow(XRBaseInteractor interactor)
    {
        isPickedUp = false;
        hasCollided = false;
        isThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;

        if (collision.gameObject.CompareTag("colliderThrow"))
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            float distanceFromZero = Vector3.Distance(collisionPoint, Vector3.zero);

            if (!isThrown)  //wenn der Ball nicht geworfen wurde
            {
                Invoke(nameof(RespawnBall), despawnDelay);
                hasCollided = true;
            }
            else
            {
                if (distanceFromZero > 2)  //ball weiter als 2 Units
                {   
                    grabInteractable.enabled = false;
                    Invoke(nameof(DespawnBall), despawnDelay);
                }
                else  //ball weniger als 2 Units
                {
                    Invoke(nameof(RespawnBall), despawnDelay);
                }

                hasCollided = true;
            }
        }
    }


    private void OnPickup(XRBaseInteractor interactor)
    {
        isThrown = false;
        isPickedUp = true;
        CancelInvoke(nameof(DespawnBall));
        CancelInvoke(nameof(RespawnBall));
    }

    private void DespawnBall()
    {
        if (!isPickedUp)
        {
            ballCounter++;
            Debug.Log(ballCounter);
            if ((ballCounter % 5) == 0)
            {
                LoadNextScene();
                return;
            }
            Destroy(gameObject);
        }
    }

    private void RespawnBall()
    {
        transform.position = parentTransform.position;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isThrown = false;
        isPickedUp = false;
        hasCollided = false;
    }

    private void Update()
    {
        
    }

    public void LoadNextScene()
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
