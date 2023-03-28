using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Apply damage to the balloon.
        collision.gameObject.SendMessageUpwards("ApplyDamage",SendMessageOptions.DontRequireReceiver);
    }
}
