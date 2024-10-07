using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject SideMenu;
    [SerializeField]
    private GameObject m_GameManager;
    TimerUI timerUI;
    AudioSource[] audioSources;

    bool isActiveSideMenu = false;
    bool menuSceneActive;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
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
        StartCoroutine(PlayAudioAndChangeScene(1, "main"));
    }

    public void ExitGame()
    {
        StartCoroutine(PlayAudioAndChangeScene(0, "quit"));
    }

    public void GoMenu()
    {
        StartCoroutine(PlayAudioAndChangeScene(1, "menu"));
    }

    public void OpenSideMenu()
    {
        if (timerUI.IsTimerRunning())
        {
            StartCoroutine(PlayAudioAndChangeScene(1, "control"));
        }
    }

    public void OpenSideMenuControls()
    {
        StartCoroutine(PlayAudioAndChangeScene(1, "control"));
    }

    IEnumerator PlayAudioAndChangeScene(int audioIndex, string action)
    {
        if (audioSources != null && audioIndex >= 0 && audioIndex < audioSources.Length)
        {
            audioSources[audioIndex].Play();
            yield return new WaitWhile(() => audioSources[audioIndex].isPlaying);
        }
        if (action == "main")
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (action == "quit")
        {
            Application.Quit();
        }
        else if (action == "menu")
        {
            SceneManager.LoadScene("MenuScene");
        }
        else if (action == "control")
        {
            isActiveSideMenu = !isActiveSideMenu;
            SideMenu.SetActive(isActiveSideMenu);
        }
    }
}
