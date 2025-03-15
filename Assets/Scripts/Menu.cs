using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class Menu : MonoBehaviour
{
    // Reference to the panels
    public GameObject MainPanel;
    public GameObject ControlsPanel;

    void Start()
    {
        // Ensure only the main menu is visible at the start
        ShowMainMenu();
    }

    // Show the main menu and hide the controls menu
    public void ShowMainMenu()
    {
        MainPanel.SetActive(true);
        ControlsPanel.SetActive(false);
    }

    // Show the controls menu and hide the main menu
    public void ShowControlsMenu()
    {
        MainPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    // Example: Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        GameState.difficulty = "easy";
        SceneManager.LoadScene("Office"); // Load the scene with the name "Office"
    }
}