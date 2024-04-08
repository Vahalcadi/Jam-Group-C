using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> rooms;
    private List<int> extractedRooms = new();
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
        ShowNewRoom();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.K))
        //{
        //    ShowNewRoom();
        //}
    }

    public void ShowNewRoom()
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
        CheckExtractedRooms();
        rooms[random].SetActive(true);
    }

    private void CheckExtractedRooms()
    {        
        random = UnityEngine.Random.Range(0, rooms.Count);
        if (extractedRooms.Contains(random))
        {
            CheckExtractedRooms();
        }
        else
            extractedRooms.Add(random);
    }
}
