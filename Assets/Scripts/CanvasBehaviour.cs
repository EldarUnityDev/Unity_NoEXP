using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public SceneAsset firstScene;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject currentMenu;

    public GameObject usePrompt;
    public bool usePromptSignal;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI recentScoreText;

    public WeaponPanel mainWeaponPanel;
    public WeaponPanel secondaryWeaponPanel;

    // Awake() happens before Start()
    void Awake() //we need Awake to establish the reference before IT IS USED
    {
        References.canvas = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (currentMenu == mainMenu)
            {
                HideMenu();
                
            }
            else
            {
                ShowMenu(mainMenu);
            }

        }
        usePrompt.SetActive(usePromptSignal);
        usePromptSignal = false;
    }

    public void ShowMainMenu() //for death screen
    {
        ShowMenu(mainMenu);
    }

    public void HideMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);

        }
        currentMenu = null;
        Time.timeScale = 1;
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        currentMenu = menuToShow;
        if(menuToShow != null)
        {
            menuToShow.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(firstScene.name);
        Time.timeScale = 1;


    }
    public void Quit()
    {
        Application.Quit();
    }
}
