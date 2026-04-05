using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;


    public void PauseGame() 
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        
    }
    public void ResumeGame() 
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;

    }

    public void MenuGame() 
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;

    }
}
