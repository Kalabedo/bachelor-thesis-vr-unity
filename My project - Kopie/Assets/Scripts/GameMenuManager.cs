using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2;

    private bool canvas1Active = true;

    // Start is called before the first frame update
    void Start()
    {
        // deactivate both canvases on start
        canvas1.SetActive(false);
        canvas2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            // toggle active canvas visibility
            if(canvas1Active)
            {
                canvas1.SetActive(!canvas1.activeSelf);
                if(canvas1.activeSelf)
                {
                    // set active canvas position, orientation
                    canvas1.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
                    canvas1.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                    canvas1.transform.forward *= -1;
                }
            }
            else
            {
                canvas2.SetActive(!canvas2.activeSelf);
                if(canvas2.activeSelf)
                {
                    // set the active canvas position, orientation
                    canvas2.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
                    canvas2.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
                    canvas2.transform.forward *= -1;
                }
            }
        }
    }

    public void SwitchCanvas()
    {
        // Sswitch active canvas, update position and orientation
        if(canvas1Active)
        {
            canvas1Active = false;
            canvas1.SetActive(false);
            canvas2.SetActive(true);
            canvas2.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            canvas2.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            canvas2.transform.forward *= -1;
        }
        else
        {
            canvas1Active = true;
            canvas1.SetActive(true);
            canvas2.SetActive(false);
            canvas1.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            canvas1.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
            canvas1.transform.forward *= -1;
        }
    }
}




