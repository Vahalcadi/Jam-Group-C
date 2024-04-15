using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> rooms;
    private List<int> extractedRooms = new();
    private bool pickupFound;

    [SerializeField] private Animator door;

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

    public IEnumerator GenerateNewRoom()
    {
        pickupFound = false;
        Door.anim.SetBool("isOpen", false);
        yield return new WaitForSeconds(1);
        GameManager.Instance.ShowNewRoom(rooms, ref extractedRooms);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && pickupFound)
        {
            StartCoroutine(GenerateNewRoom());
        }
    }
}
