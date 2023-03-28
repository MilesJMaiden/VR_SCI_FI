using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class ColliderFollowHeadset : MonoBehaviour
{
    private CharacterController charController;
    public Transform centerEye;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void LateUpdate() //LateUpdate is called after all other functions.
    {
        //Here I am creating a new center vector 3 by taking the headset position and moving the character controller accordingly so character collides properly.
        Vector3 newCenter = transform.InverseTransformVector(centerEye.position - transform.position);
        charController.center = new Vector3(newCenter.x, charController.center.y, newCenter.z);
    }
}
