using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    public float timeBeforeDisable = 0.2f;
    private float timer = 0;

    private void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeBeforeDisable)
            gameObject.SetActive(false);
        timer += Time.deltaTime;
    }
}
