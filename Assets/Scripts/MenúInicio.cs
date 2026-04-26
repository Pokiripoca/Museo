using UnityEngine;
using UnityEngine.SceneManagement;
public class ControladorMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CargarMuseo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Cerrando museo...");
        Application.Quit();
    }

 
}