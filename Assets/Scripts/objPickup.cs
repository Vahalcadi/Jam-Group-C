using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    [HideInInspector] public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (pickedup)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
                pickedup = false;
            }
            else
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
            }
        }
    }
    protected virtual void Update()
    {
        if (interactable)
        {
            if (InputManager.Instance.GetGrabObject())
            {
                objTransform.parent = cameraTrans;
                objRigidbody.useGravity = false;
                pickedup = true;
            }
            if (InputManager.Instance.GetGrabObject())
            {
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                pickedup = false;
            }
            if (pickedup)
            {
                if (Input.GetMouseButtonDown(1))
                {                  
                    objTransform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                    pickedup = false;
                }
            }
        }
    }
}
