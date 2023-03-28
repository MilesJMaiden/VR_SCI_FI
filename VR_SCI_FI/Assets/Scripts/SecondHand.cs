using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SecondHand : MonoBehaviour
{
    private Interactable interactable;
    public Interactable mainInteractable;
    private Quaternion secondRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandAttachedUpdate(Hand hand)
    {
        if(mainInteractable.attachedToHand)
        {
            //rotate the pivot
            if(mainInteractable.skeletonPoser)
            {
                mainInteractable.transform.rotation = GetTargetRotation() * secondRotationOffset
                    * mainInteractable.skeletonPoser.GetBlendedPose(mainInteractable.attachedToHand.skeleton).rotation;
            }
            else
                mainInteractable.attachedToHand.objectAttachmentPoint.rotation = GetTargetRotation() * secondRotationOffset;
        }
    }

    Quaternion GetTargetRotation()
    {
        return Quaternion.LookRotation(interactable.attachedToHand.transform.position - mainInteractable.attachedToHand.transform.position
            ,interactable.attachedToHand.objectAttachmentPoint.up);
    }

    public void ForceDetach()
    { 
        if(interactable.attachedToHand)
        {
            interactable.attachedToHand.HoverUnlock(interactable);
            interactable.attachedToHand.DetachObject(gameObject);
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //Grabs the object and sets a new roational pivot point
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType, 0);
            hand.HoverLock(interactable);
            hand.HideGrabHint();
            secondRotationOffset = Quaternion.Inverse(GetTargetRotation()) * mainInteractable.attachedToHand.currentAttachedObjectInfo.Value.handAttachmentPointTransform.rotation;
        }
        //Rekeases the object
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
