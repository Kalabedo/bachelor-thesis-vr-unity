using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class BallDespawnScenario3 : MonoBehaviour
{
    private string[] sceneNames = { "Scenario1", "Scenario1Quest", "Scenario2", "Scenario2Quest", "Scenario3", "Scenario3Quest" };
    public float despawnDelay = 5f; // Zeitverzögerung in Sekunden, bevor der Ball despawnt

    private Rigidbody rigidBody;
    private XRGrabInteractable grabInteractable;

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

        isThrown = true;
        isPickedUp = false;
        ballCounter++;

        if ((ballCounter % 5) == 0)
        {
            Invoke(nameof(LoadNextScene), despawnDelay);
            return;
        }

        Invoke(nameof(MoveToSpawnBall), despawnDelay);
    }

    private void OnPickup(XRBaseInteractor interactor)
    {
        isThrown = false;
        isPickedUp = true;
        CancelInvoke(nameof(MoveToSpawnBall));
    }

    private void MoveToSpawnBall()
    {
        if (!isPickedUp)
        {
            Destroy(gameObject);
            // transform.position = parentTransform.position;
            // rigidBody.velocity = Vector3.zero;
            // rigidBody.angularVelocity = Vector3.zero;
            // isThrown = false;
        }
    }

    private void RespawnBall()
    {
        if (!isPickedUp)
        {
            transform.position = parentTransform.position;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            isThrown = false;
        }
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
        // Calculate the index of the next scene to load
        // int nextSceneIndex = (currentSceneIndex) % sceneNames.Length;
        // Debug.Log(currentSceneIndex);
        // Debug.Log(nextSceneIndex);

        // // Load the next scene
        // SceneManager.LoadScene(sceneNames[nextSceneIndex]);
    }
}
