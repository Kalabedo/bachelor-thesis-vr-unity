using UnityEngine;

public class PlayerOrientationStart : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    [ContextMenu ("Reset Position")]
    public void ResetPosition()
    {
        var rotationAngleY = playerHead.transform.rotation.eulerAngles.y - 
        resetTransform.transform.rotation.eulerAngles.y;

        player.transform.Rotate(0, -rotationAngleY, 0);

        var distanceDiff = resetTransform.position - 
        playerHead.transform.position;

        player.transform.position += distanceDiff;
    }
}
