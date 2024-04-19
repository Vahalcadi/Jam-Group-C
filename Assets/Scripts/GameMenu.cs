using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseMenu;
    
    [SerializeField] private GameObject playerControlsUI;
    [SerializeField] private GameObject customiseGameSettingsUI;
    [HideInInspector] public string currentSceneName;
    public string mainMenuSceneName;
    public static GameMenu Instance;

    public TextMeshProUGUI dwarfCounter;
    [SerializeField] private Image xAxisCheckmark;
    [SerializeField] private Image yAxisCheckmark;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        dwarfCounter.text = "0";
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchWithKeyTo(pauseMenu);
            
        }

    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            QuestionDialogUI.Instance.Hide();
            _menu.SetActive(false);

            CheckForInGameUI();

            return;
        }
        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        
        SwitchTo(inGameUI);
    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }


        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }

        if (GameManager.Instance != null)
        {
            if (_menu == inGameUI)
                GameManager.Instance.PauseGame(false);
            else
                GameManager.Instance.PauseGame(true);

        }
    }

    public void GoToMainMenu()
    {
        QuestionDialogUI.Instance.ShowQuestion("YOU WILL LOSE ALL YOUR PROGRESS, ARE YOU SURE?",
            () =>
            {
                Time.timeScale = 1.0f;
                //restartGameUI.SetActive(false);
                SceneManager.LoadScene(mainMenuSceneName);
            },
            () =>
            {
                SwitchWithKeyTo(inGameUI);
            });

        // Debug.Log("To Main Menu (implementing)");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void InvertAxisX()
    {
        CinemachinePOVExtention.Instance.InvertX();
        xAxisCheckmark.gameObject.SetActive(!xAxisCheckmark.gameObject.activeSelf);
    }
    public void InvertAxisY() 
    { 
        CinemachinePOVExtention.Instance.InvertY();
        yAxisCheckmark.gameObject.SetActive(!yAxisCheckmark.gameObject.activeSelf);
    }
    public void SetSensitivity(float _value) => CinemachinePOVExtention.Instance.SliderValue(_value);
}
