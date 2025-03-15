using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class Menu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Office");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void onControlsButtonClicked()
    {
        SceneManager.LoadScene("Controls");
    }
}