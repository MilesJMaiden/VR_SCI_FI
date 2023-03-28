using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Transform Hand;
    public Transform secondHand;

    private Vector3 positionOffset;
    private Quaternion rotationOffset;
    private Quaternion secondHandRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        positionOffset = Hand.InverseTransformPoint(transform.position);
        rotationOffset = Quaternion.Inverse(Hand.rotation) * transform.rotation;
        secondHandRotationOffset = Quaternion.Inverse(Quaternion.LookRotation(secondHand.position - Hand.position))
            * Hand.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Hand.TransformPoint(positionOffset);
        transform.rotation = Hand.rotation * rotationOffset;

        Hand.rotation = Quaternion.LookRotation(secondHand.position - Hand.position) * secondHandRotationOffset;


    }
}
