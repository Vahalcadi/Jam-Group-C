using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : objPickup
{
    public static Animator anim;
    private bool isOpen;

    private bool hasAnimationStarted;


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        crosshair1.SetActive(true);
        crosshair2.SetActive(false);
        interactable = false;
    }

    protected override void Update()
    {
        if(interactable && !hasAnimationStarted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogWarning(interactable);
                if (isOpen)
                    anim.SetBool("isOpen", false);
                else
                    anim.SetBool("isOpen", true);
            }
        }
    }
    private void OnDisable()
    {
        anim = null;
    }

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    
    public void SetHasAnimationStartedToggle() => hasAnimationStarted = !hasAnimationStarted;
    public void SetIsOpenToggle() => isOpen = !isOpen;
}
