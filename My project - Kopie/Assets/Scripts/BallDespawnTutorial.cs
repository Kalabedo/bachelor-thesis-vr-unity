using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class BallDespawnTutorial : MonoBehaviour
{
    public float despawnDelay = 5f; // Zeitverzögerung in Sekunden, bevor der Ball despawnt

    private Rigidbody rigidBody;
    private XRGrabInteractable grabInteractable;

    private bool isThrown = false; //  um zu überprüfen, ob der Ball geworfen wurde
    private bool isPickedUp = false; //  um zu überprüfen, ob der Ball aufgenommen wurde

    private Transform parentTransform; // Transform des Parents, an dem der Ball gespawnt werden soll

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
            // Destroy(gameObject);
            transform.position = parentTransform.position;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            isThrown = false;
        }
    }

    private void Update()
    {
        if (isThrown && rigidBody.velocity.magnitude < 0.1f) // Wenn der Ball geworfen wurde und nicht mehr in Bewegung ist
        {
            Invoke(nameof(MoveToSpawnBall), despawnDelay); // Despawn den Ball nach der Verzögerung
        }
    }
}


