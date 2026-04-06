using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("Fucking Exitted the game!");
        }

    }
    public void ImOuttaHere()
    {
        Application.Quit();
        Debug.Log("Fucking Exitted the game!");
    }
}
