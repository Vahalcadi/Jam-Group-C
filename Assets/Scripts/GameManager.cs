using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> rooms;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            rooms[0].SetActive(!rooms[0].activeSelf);
            rooms[1].SetActive(!rooms[1].activeSelf);
        }
    }
}
