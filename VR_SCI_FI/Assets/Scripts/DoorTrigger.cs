using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator animator;
    bool doorOpen;

    public AudioClip openClose;
    AudioSource audioSource;
    public float vol = 0.5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        doorOpen = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // If the character collides with door collider the bool parameter is made true enabling its opening animation and plays a sound once.
            doorOpen = true;
            animator.SetBool("character_nearby", true);
            audioSource.PlayOneShot(openClose, vol);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(doorOpen)
        {
            //Once the character is no longer colliding with the door collider, the aninmation parameter is set to false, closing the door and playing a closing sound.
            doorOpen = false;
            animator.SetBool("character_nearby", false);
            audioSource.PlayOneShot(openClose, vol);
        }
    }
}
