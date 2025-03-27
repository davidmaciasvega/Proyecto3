using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class MenuManager : MonoBehaviour
{
    // Método para cargar la escena del juego
    public void IniciarJuego()
    {
        // Aquí se carga la escena del juego. Asegúrate de que el nombre sea correcto.
        SceneManager.LoadScene("minijuego"); // Asegúrate de que "Juego" sea el nombre de tu escena de juego
    }
}
