using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject inGameCanvas; // Reference to the in-game canvas

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that only the Main Menu Panel is initially active
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        inGameCanvas.SetActive(false); // Ensure that the in-game canvas is initially inactive
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        inGameCanvas.SetActive(true); 
    }

    public void OnSettingsButtonClicked()
    {
        // Show the settings panel and hide the main menu panel
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        // Show the main menu panel and hide the settings panel
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }


}
