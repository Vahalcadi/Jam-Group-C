using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> rooms;
    private List<int> extractedRooms = new();
    private bool pickupFound;

    private void Start()
    {
        GameManager.Instance.ShowNewRoom(rooms, ref extractedRooms);
    }

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
            GameManager.Instance.ShowNewRoom(rooms, ref extractedRooms); 
        }
    }
}
