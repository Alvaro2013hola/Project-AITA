using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject creditosMenu;

    public void OpenOptionsPanel()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        creditosMenu.SetActive(false);



    }
    public void OpenMainMenuPanel()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditosMenu.SetActive(false);


    }

    public void OpenCreditosPanel() 
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditosMenu.SetActive(true);
    
    
    }

    public void QuitGame()
    {
        Debug.Log("Aplicacion cerrada");
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}