using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TwoHandsGrab : MonoBehaviour
{
    [EnumFlags]
    public Hand.AttachmentFlags flags = Hand.defaultAttachmentFlags;
    public Interactable firstGrabInteractable;
    private Interactable secondGrabInteractable;

    private Quaternion rotationalOffset;
    private Quaternion secondHandRotationalOffset;
    
    private Quaternion initialAttachmentPointRot;
    public RotationControl rotationControlType;
    public enum RotationControl { LEFT_HAND,RIGHT_HAND,NONE}

    // Start is called before the first frame update
    void Start()
    {
        secondGrabInteractable = GetComponent<Interactable>();
        flags = 0;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        if (firstGrabInteractable.attachedToHand != null)
            hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (firstGrabInteractable.attachedToHand != null)
        {
            GrabTypes grabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(gameObject);

            //Grabs the object
            if (grabType != GrabTypes.None)
            {
                hand.AttachObject(gameObject, grabType, flags);
                hand.HoverLock(secondGrabInteractable);
                hand.HideGrabHint();
            }
            //Release object
            else if (isGrabEnding)
            {
                ForceGrabEnd();
            }
        }
    }

    public void ForceGrabEnd()
    {
        Hand hand = secondGrabInteractable.attachedToHand;
        if (!hand)
            return;

        if(firstGrabInteractable.attachedToHand)
            GetFirstHandTransform().localRotation = initialAttachmentPointRot;
        hand.DetachObject(gameObject);
        hand.HoverUnlock(secondGrabInteractable);
    }

    public Vector3 GetUpAxis()
    {
        if (rotationControlType == RotationControl.LEFT_HAND)
        {
            return secondGrabInteractable.attachedToHand.objectAttachmentPoint.up;
        }
        else if (rotationControlType == RotationControl.RIGHT_HAND)
        {
            return firstGrabInteractable.attachedToHand.objectAttachmentPoint.up;
        }
        else
        {
            return firstGrabInteractable.transform.up;
        }
    }


    public void SetupPivot()
    {
        Vector3 upAxis = GetUpAxis();

        secondHandRotationalOffset = Quaternion.Inverse(Quaternion.LookRotation(GetSecondHandTransform().position - GetFirstHandTransform().position, upAxis)) 
            * GetFirstHandTransform().rotation;
        
        initialAttachmentPointRot = GetFirstHandTransform().localRotation;
    }

    //-------------------------------------------------
    protected virtual void OnAttachedToHand(Hand hand)
    {
        SetupPivot();
    }

    private void HandAttachedUpdate(Hand hand)
    {
        if (firstGrabInteractable.attachedToHand)
        {           
            Quaternion targetRotation;

            targetRotation = Quaternion.LookRotation(GetSecondHandTransform().position - GetFirstHandTransform().position,GetUpAxis()) * secondHandRotationalOffset;

            Debug.Log("ATTACHED ROTATE");
           
            if(firstGrabInteractable.skeletonPoser)
            {
                firstGrabInteractable.transform.rotation = targetRotation * firstGrabInteractable.skeletonPoser.GetBlendedPose(firstGrabInteractable.attachedToHand.skeleton).rotation;
                Debug.Log("WE GOT A SKELETON");
            }
            else
                GetFirstHandTransform().rotation = targetRotation;
        }
    }

    public Transform GetFirstHandTransform()
    {
        return firstGrabInteractable.attachedToHand.currentAttachedObjectInfo.Value.handAttachmentPointTransform;
    }

    public Transform GetSecondHandTransform()
    {
        return secondGrabInteractable.attachedToHand.currentAttachedObjectInfo.Value.handAttachmentPointTransform;
    }
}

