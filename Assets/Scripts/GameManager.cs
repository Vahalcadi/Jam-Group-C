using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int random;
    private int numberOfPickedUpDwarfs = 0;
    private bool pickupFound;

    public bool PickupFound { get { return pickupFound; } }

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }


    public void ShowNewRoom(List<GameObject> rooms, ref List<int> extractedRooms)
    {
        pickupFound = false;

        if (rooms.Count == extractedRooms.Count)
        {
            RestartGame("ALL DWARFS FOUND!, PLAY AGAIN?");
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

    public void RestartGame(string dialog)
    {
        PauseGame(true);

        QuestionDialogUI.Instance.ShowQuestion(dialog,
            () =>
            {
                PauseGame(false);
                SceneManager.LoadScene(GameMenu.Instance.currentSceneName);
            },
            () =>
            {
                QuestionDialogUI.Instance.ShowQuestion("This will close the game, are you sure?",
                    () =>
                    {
                        Application.Quit();
                    },
                    () =>
                    {
                        RestartGame(dialog);
                    });
            });
    }

    public virtual void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
            InputManager.Instance.OnDisable();
            Cursor.visible = true;
        }
        else
        {
            InputManager.Instance.OnEnable();
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }

    public void PickUpDwarf(GameObject dwarf)
    {
        dwarf.SetActive(false);
        numberOfPickedUpDwarfs++;
        GameMenu.Instance.dwarfCounter.text = $"{numberOfPickedUpDwarfs}";
        pickupFound = true;
    }
}
