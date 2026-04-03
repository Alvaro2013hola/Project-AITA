using UnityEngine;
using UnityEngine.UI;

public class LogicaFullCreen : MonoBehaviour
{
    public Toggle toggle;
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;

        }
        else 
        { 
            toggle.isOn =false;


        }
        
    }

    void Update()
    {


        
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta) 
    {
        Screen.fullScreen = pantallaCompleta;
        Debug.Log("Pantalla completa");
    }



}
