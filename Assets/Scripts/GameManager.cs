using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int random;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void ShowNewRoom(List<GameObject> rooms, ref List<int> extractedRooms)
    {

        if (rooms.Count == extractedRooms.Count)
        {
            Debug.Log("All rooms extracted");
            return;
        }
            

        for (int i = 0; i < rooms.Count; i++)
        {    
            rooms[i].SetActive(false);
        }
        CheckExtractedRooms(rooms, ref extractedRooms);
        rooms[random].SetActive(true);
        Debug.Log("Room changed");
    }

    private void CheckExtractedRooms(List<GameObject> rooms, ref List<int> extractedRooms)
    {        
        random = UnityEngine.Random.Range(0, rooms.Count);
        if (extractedRooms.Contains(random))
        {
            CheckExtractedRooms(rooms, ref extractedRooms);
        }
        else
            extractedRooms.Add(random);
    }
}
