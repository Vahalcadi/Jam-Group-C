using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool pickupFound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            pickupFound = true;
            Debug.Log("Found pickup");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && pickupFound)
        {
            GameManager.Instance.ShowNewRoom();
            Debug.Log("Room changed");
        }
    }
}
