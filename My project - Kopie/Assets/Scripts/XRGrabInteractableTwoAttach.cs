using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    public AudioClip sound;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("LeftHand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if(args.interactorObject.transform.CompareTag("RightHand"))
        {
            attachTransform = rightAttachTransform;                         //use correct attach point
        }

        AudioSource.PlayClipAtPoint(sound, transform.position);             //sound when pickup
        base.OnSelectEntered(args);
    }
}
