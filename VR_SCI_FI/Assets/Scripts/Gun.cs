using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(AudioSource))]

public class Gun : MonoBehaviour
{
    public bool automatic = true;
    public float shootdelay = 0.1f;
    public SteamVR_Action_Boolean fireAction;
    public GameObject bullet;
    public Transform barrelPivot;
    public float shootingSpeed = 1;
    public GameObject muzzleFlash;

    public float shootVol = 0.5f;
    public AudioClip shootSound;
    AudioSource audioSource;

    private Animator animator;
    private Interactable interactable;
    private float lastShoot = 0;
    private float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        muzzleFlash.SetActive(false);
        interactable = GetComponent<Interactable>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //check if grabbed
        if(interactable.attachedToHand != null)
        {
            //get the hand source
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            //check button is down
            if(!automatic && fireAction[source].stateDown)
            {
                Fire();
                lastShoot = 0;
            }
            else if(automatic && lastShoot>shootdelay && fireAction[source].state)
            {
                Fire();
                lastShoot = 0;
            }
        }

        lastShoot += Time.deltaTime;
    }

    void Fire()
    {
        //this instantiates, plays a sound and sets the velocity of/for the bullet prefab
        Debug.Log("Fire");
        Rigidbody bulletrb = Instantiate(bullet, barrelPivot.position,barrelPivot.rotation).GetComponent<Rigidbody>();
        bulletrb.velocity = barrelPivot.forward * shootingSpeed;
        muzzleFlash.SetActive(true);
        audioSource.PlayOneShot(shootSound, shootVol);
    }
}
