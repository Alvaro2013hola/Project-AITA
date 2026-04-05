using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nombreEscena; // Nombre exacto de la escena

    /// <summary>
    /// Carga la escena especificada en nombreEscena.
    /// Puede ser llamada por un Button de UI (On Click) o por otros scripts.
    /// </summary>
    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogError("ChangeScene: No se ha especificado el nombre de la escena.");
        }
    }

    /// <summary>
    /// Opcional: Carga la escena cuando el jugador entra en este Trigger.
    /// Solo funciona si el GameObject tiene un Collider con 'Is Trigger' activado y el objeto que entra tiene el tag 'Player'.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CargarEscena();
        }
    }
}
