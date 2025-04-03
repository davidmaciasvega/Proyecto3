using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar la escena

public class ReinicioJuego : MonoBehaviour
{
    // Método para reiniciar el juego
    public void Reiniciar()
    {
        // Reinicia la puntuación, nivel, y otras variables si es necesario
        ControladorDelJugador controlador = FindFirstObjectByType<ControladorDelJugador>();
        if (controlador != null)
        {
            controlador.ReiniciarVariables();
        }

        // Reinicia la escena actual para comenzar de nuevo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
