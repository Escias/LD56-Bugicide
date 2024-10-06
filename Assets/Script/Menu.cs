using System.Collections;
using System.Collections.Generic;
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
        menuSceneActive = SceneManager.GetActiveScene().name == "MenuScene";
        try
        {
            timerUI = m_GameManager.GetComponent<TimerUI>();
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !timerUI.IsTimerRunning() && !menuSceneActive)
        {
            GoMenu();
        }
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
}
