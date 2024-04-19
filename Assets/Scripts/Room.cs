using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> rooms;
    private List<int> extractedRooms = new();

    [SerializeField] private Animator door;

    private void Start()
    {
        GameManager.Instance.ShowNewRoom(rooms, ref extractedRooms);
    }

    public IEnumerator GenerateNewRoom()
    {
        Door.anim.SetBool("isOpen", false);
        yield return new WaitForSeconds(1);
        GameManager.Instance.ShowNewRoom(rooms, ref extractedRooms);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.PickupFound)
        {
            StartCoroutine(GenerateNewRoom());
        }
    }
}
