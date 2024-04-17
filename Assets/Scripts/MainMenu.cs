using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private string sceneName = "TestLevel";
    //[SerializeField] private GameObject leaderboardUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject MainUI;

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);

            CheckForInGameUI();

            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        SwitchTo(MainUI);
    }
    public void SwitchTo(GameObject _menu)
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }
    }


    //load level
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    //exit game
    public void ExitGame()
    {
        QuestionDialogUI.Instance.ShowQuestion("This action will close the application, are you sure?",
            () =>
            {
                Application.Quit();
            },
            () =>
            {
                SwitchWithKeyTo(MainUI);
            });
        //Application.Quit();
    }
}
