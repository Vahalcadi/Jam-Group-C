using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Dwarf : objPickup
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractableCollider"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableCollider"))
        {
            crosshair1.SetActive(true);
            crosshair2.SetActive(false);
            interactable = false;

        }
    }

    protected override void Update()
    {
        if (interactable)
        {
            if (InputManager.Instance.GetInteract())
            {
                GameManager.Instance.PickUpDwarf(gameObject);
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
            }
        }
    }
}
