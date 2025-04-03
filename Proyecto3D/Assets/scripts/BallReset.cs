using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Para manejar el texto en pantalla
using System.Collections; // Necesario para usar corutinas

public class BallReset : MonoBehaviour
{
    public int vidas = 3; // Inicializa con 3 vidas
    public TextMeshProUGUI textoVidas; // Referencia al TextMeshPro en la UI
    private ReinicioJuego reinicioJuego; // Referencia al script de reinicio

    public TextMeshProUGUI mensajeGameOver; // Mensaje de "Perdiste, reiniciando..."
    public float delayReinicio = 3f; // Tiempo antes de reiniciar

    void Start()
    {
        // Buscar y asignar el objeto ReinicioJuego automáticamente
        reinicioJuego = Object.FindFirstObjectByType<ReinicioJuego>();

        // Mostrar las vidas al inicio
        ActualizarTextoVidas();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name); // Verifica si detecta la colisión

        if (other.CompareTag("ZonaP")) 
        {
            Debug.Log("Tocó la ZonaP, restando vida...");
            vidas--; 
            ActualizarTextoVidas();

            if (vidas <= 0)
            {
                StartCoroutine(PerderYReiniciar()); // Iniciar corutina para mostrar mensaje y reiniciar
            }
            else
            {
                transform.position = new Vector3(0, 0.5f, 0);
            }
        }
    }

    void ActualizarTextoVidas()
    {
        textoVidas.text = "Vidas: " + vidas;
    }

    IEnumerator PerderYReiniciar()
    {
        if (mensajeGameOver != null)
        {
            mensajeGameOver.text = "Perdiste, reiniciando...";
            mensajeGameOver.gameObject.SetActive(true); // Muestra el mensaje
        }

        yield return new WaitForSeconds(delayReinicio); // Espera 3 segundos antes de reiniciar

        reinicioJuego.Reiniciar();
    }
}
