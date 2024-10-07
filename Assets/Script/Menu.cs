using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject SideMenu;
    [SerializeField]
    private GameObject m_GameManager;
    TimerUI timerUI;

    bool isActiveSideMenu = false;
    bool menuSceneActive;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            timerUI = m_GameManager.GetComponent<TimerUI>();
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OpenSideMenu()
    {
        if (timerUI.IsTimerRunning())
        {
            isActiveSideMenu = !isActiveSideMenu;
            SideMenu.SetActive(isActiveSideMenu);
        }
    }

    public void OpenSideMenuControls()
    {
        isActiveSideMenu = !isActiveSideMenu;
        SideMenu.SetActive(isActiveSideMenu);
    }
}
