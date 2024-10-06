using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject SideMenu;

    bool isActiveSideMenu = false;

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
        isActiveSideMenu = !isActiveSideMenu;
        SideMenu.SetActive(isActiveSideMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
