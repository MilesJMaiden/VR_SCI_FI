using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHands : MonoBehaviour
{
    public bool secondGrab = true;

    public Transform rightHand;
    public Transform leftHand;

    private Vector3 positionalOffset;
    private Quaternion rotationalOffset;
    private Quaternion secondHandRotationalOffset;
    private Vector3 secondHandPosOffset;

    // Start is called before the first frame update
    void Start()
    {
        SetupPivot();
    }

    public void SetupPivot()
    {
        positionalOffset = rightHand.InverseTransformPoint(transform.position);
        rotationalOffset = Quaternion.Inverse(rightHand.rotation) * transform.rotation;
        secondHandRotationalOffset = Quaternion.Inverse(Quaternion.LookRotation(leftHand.position - rightHand.position)) * rightHand.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(secondGrab)
        {
            rightHand.rotation = Quaternion.LookRotation((leftHand.position - rightHand.position).normalized) * secondHandRotationalOffset;
        }

        transform.position = rightHand.TransformPoint(positionalOffset); 
        transform.rotation = rightHand.rotation * rotationalOffset;        
    }
}
