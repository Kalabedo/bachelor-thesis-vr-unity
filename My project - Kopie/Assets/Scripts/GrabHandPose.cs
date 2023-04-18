using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;          //custom right Handpose
    public HandData leftHandPose;           //custom left Handpose
    private Vector3 startingHandPos;        //position start 
    private Vector3 finalHandPos;           //position end
    private Quaternion startingHandRot;     //rotation start 
    private Quaternion finalHandRot;        //rotation end

    private Quaternion[] startingFingerRot; //finger rotation start 
    private Quaternion[] finalFingerRot;    //finger rotation end 

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);      //call when grab
        grabInteractable.selectExited.AddListener(UnsetPose);       //call when release

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if(arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();  //get current hand data
            handData.animator.enabled = false;

            if (handData.handType == HandData.HandModelType.Right)
            {
                SetHandDataValues(handData, rightHandPose);         //set new for right hand
            }
            else
            {
                SetHandDataValues(handData, leftHandPose);          //set new for left hand
            }

            SetHandData(handData, finalHandPos, finalHandRot, finalFingerRot);
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPos = new Vector3(h1.root.localPosition.x / h1.root.localScale.x, h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
        finalHandPos = new Vector3(h2.root.localPosition.x / h2.root.localScale.x, h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z);

        startingHandRot = h1.root.localRotation;
        finalHandRot = h2.root.localRotation;

        startingFingerRot = new Quaternion[h1.fingerBones.Length];
        finalFingerRot = new Quaternion[h1.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRot[i] = h1.fingerBones[i].localRotation;
            finalFingerRot[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPos, Quaternion newRot, Quaternion[] newBonesRot)
    {
        h.root.localPosition = newPos;
        h.root.localRotation = newRot;

        for (int i = 0; i < newBonesRot.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRot[i];
        }
    }

    public void UnsetPose(BaseInteractionEventArgs arg) 
    {
        if(arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            SetHandData(handData, startingHandPos, startingHandRot, startingFingerRot);
        }
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Mirror Selected Right Grab Pose")]         //create function to mirror handpose, without editing in unity
    public static void MirrorRightPose()
    {
        Debug.Log("Mirror Pose");
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
    }
#endif

    public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror){
        Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
        mirroredPosition.x *= -1;

        Quaternion mirroredQuaternion = poseToMirror.root.localRotation;
        mirroredQuaternion.y *= -1;
        //mirroredQuaternion.z *= -1;
        //mirroredQuaternion.x *= -1;

        poseToMirror.root.localPosition = mirroredPosition;
        poseToMirror.root.rotation = mirroredQuaternion;

        for (int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
        }
    }
    
}
