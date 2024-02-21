using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Required for scene management


public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pausePanel;
    public GameObject inGameCanvas;
    public GameObject anomalyReportPanel;
    public GameObject gameOverCanvas; 
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject winPanel;

    public TMP_Dropdown roomDropdown;

    public MusicController musicManager;

    public Slider musicVolumeSlider;
    public Slider musicVolumeSlider2;


    public Button sendButton;
    public Button fileAnomalyReportButton;
    public Button cancelButton;

    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI unreportedAnomaliesText; 
    public TextMeshProUGUI totalAnomaliesFound; 
    public TextMeshProUGUI timeSurvived;
    public TextMeshProUGUI livesText; 

    private int counterLivingRoom = 0;
    private int counterBedroom = 0;
    private int counterKitchen = 0;
    public int totalLives = 3;
    public int totalAnomaliesFoundCounter = 0;
    private int totalUnreportedAnomalies = 0;



    private float elapsedTime = 0f;
    
  
    void Update()
    {
        if(!gameManager.gameIsOver)
        {
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               
                TogglePausePanel();
            }
            
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = formattedTime;
            UpdateTotalLivesCount();
            totalAnomaliesFound.text = "Total Anomalies Found: " + (totalAnomaliesFoundCounter);
            timeSurvived.text = "Time Survived: " + formattedTime;

        }
        else
        {
            inGameCanvas.SetActive(false); // Hide the in-game canvas
            gameOverCanvas.SetActive(true); // Show the game over canvas
        }

    }

    // Start is called before the first frame update
    void Start()
    {


        pausePanel.SetActive(false);
        inGameCanvas.SetActive(false);
        menuPanel.SetActive(true);
        
        Time.timeScale = 0f;
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        musicVolumeSlider2.onValueChanged.AddListener(OnMusicVolumeChanged);


        gameOverCanvas.SetActive(false); 

        SetAnomalyReportPanelVisibility(false);

        fileAnomalyReportButton.onClick.AddListener(OnFileAnomalyReportButtonClick);

        // Find the "Cancel" button in the anomaly report panel and register a listener for its click event
        Button cancelButton = anomalyReportPanel.GetComponentInChildren<Button>();
        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(OnCancelButtonClick);
        }
    }

    public void OnMusicVolumeChanged(float volume)
    {
        musicManager.SetMusicVolume(volume);
    }

    public void OnSettingsButtonClick()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);



    }

    public void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnBackButtonClick()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnStartButtonClick()
    {
        menuPanel.SetActive(false); // Hide the menu panel
        inGameCanvas.SetActive(true); // Show the in-game canvas
        Time.timeScale = 1f; // Set time scale to 1 to resume game
    }

    void TogglePausePanel()
    {
        // Invert the current state of the pause panel
        bool isPaused = !pausePanel.activeSelf;
        pausePanel.SetActive(isPaused);

        // Pause/unpause the game based on the visibility of the pause panel
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Method to handle the "Try Again" button click event
    public void OnTryAgainButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to handle the "Go to Main Menu" button click event
    public void OnGoToMainMenuButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }
    public void showWinPanel()
    {
        winPanel.SetActive(true); // Show the win panel
    }

    // Method to handle the "File an anomaly report" button click event
    void OnFileAnomalyReportButtonClick()
    {
        // Show the anomaly report panel and hide the "File an anomaly report" button
        SetAnomalyReportPanelVisibility(true);
        SetFileAnomalyReportButtonVisibility(false);
    }

    // Method to handle the "Cancel" button click event
    void OnCancelButtonClick()
    {
        // Hide the anomaly report panel and show the "File an anomaly report" button
        SetAnomalyReportPanelVisibility(false);
        SetFileAnomalyReportButtonVisibility(true);
    }

    // Method to control the visibility of the anomaly report panel
    void SetAnomalyReportPanelVisibility(bool isVisible)
    {
        anomalyReportPanel.SetActive(isVisible);
    }

   

    // Method to control the visibility of the "File an anomaly report" button
    void SetFileAnomalyReportButtonVisibility(bool isVisible)
    {
        fileAnomalyReportButton.gameObject.SetActive(isVisible);
    }

    // Method to display feedback text
    public void DisplayFeedback(string message)
    {
        feedbackText.text = message;
    }

    // Method to update the total unreported anomalies count
    void UpdateTotalUnreportedAnomaliesCount()
    {
        totalUnreportedAnomalies = counterLivingRoom + counterBedroom + counterKitchen;
        unreportedAnomaliesText.text = "Total Unreported Anomalies: " + totalUnreportedAnomalies;

        // Check if the game should end
        if (totalUnreportedAnomalies >= 4)
        {
            gameManager.GameOver(); 
        }
    }
    void UpdateTotalLivesCount()
    {
        string lifeSymbols = "";
        for (int i = 0; i < totalLives; i++)
        {
            lifeSymbols += "# "; 
        }
        livesText.text =  lifeSymbols;
    }

    public void OnSendButtonClick()
    {
        
        string selectedRoom = roomDropdown.options[roomDropdown.value].text;
        UpdateTotalLivesCount();
        switch (selectedRoom)
        {
            case "Living Room":
                if (counterLivingRoom > 0)
                {
                    counterLivingRoom--;
                    DisplayFeedback("Correct report for Living Room.");
                    totalAnomaliesFoundCounter++;
                }
                else
                {
                    DisplayFeedback("False report. No unreported events in Living Room.");
                    totalLives--;
                    if (totalLives == 0)
                    {
                        gameManager.GameOver(); // Call the game over method in GameManager if total lives reach 0
                    }
                }
                break;
            case "Bedroom":
                if (counterBedroom > 0)
                {
                    counterBedroom--;
                    DisplayFeedback("Correct report for Bedroom.");
                    totalAnomaliesFoundCounter++;
                }
                else
                {
                    DisplayFeedback("False report. No unreported events in Bedroom.");
                    totalLives--;
                    if (totalLives == 0)
                    {
                        gameManager.GameOver(); // Call the game over method in GameManager if total lives reach 0
                    }
                }
                break;
            case "Kitchen":
                if (counterKitchen > 0)
                {
                    counterKitchen--;
                    DisplayFeedback("Correct report for Kitchen.");
                    totalAnomaliesFoundCounter++;
                }
                else
                {
                    DisplayFeedback("False report. No unreported events in Kitchen.");
                    totalLives--;
                    if (totalLives == 0)
                    {
                        gameManager.GameOver(); // Call the game over method in GameManager if total lives reach 0
                    }
                }
                break;
        }
        SetAnomalyReportPanelVisibility(false);
        fileAnomalyReportButton.gameObject.SetActive(true);

        UpdateTotalUnreportedAnomaliesCount(); // Update the total unreported anomalies count after sending a report
    }

    public void IncrementCounter(string room)
    {
        switch (room)
        {
            case "Living Room":
                counterLivingRoom++;
                break;
            case "Bedroom":
                counterBedroom++;
                break;
            case "Kitchen":
                counterKitchen++;
                break;
        }

        UpdateTotalUnreportedAnomaliesCount(); // Update the total unreported anomalies count after incrementing counter
    }

}
