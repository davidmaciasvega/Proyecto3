using UnityEngine;

public class CambioDeEstilo : MonoBehaviour
{
    public int nivelActual = 1; // Nivel inicial
    public GameObject suelo; // Objeto del suelo
    public Renderer jugadorRenderer; // Renderer del jugador

    public Color[] coloresSuelo; // Lista de colores para el suelo
    public Color[] coloresJugador; // Lista de colores para el jugador

    void Start()
    {
        Debug.Log("Juego iniciado. Nivel actual: " + nivelActual);
      //  AplicarCambioDeEstilo(); // Aplica el estilo inicial
    }

    public void SiguienteNivel()
    {
        nivelActual++; // Incrementar el nivel
        Debug.Log("🔼 Subiendo al nivel: " + nivelActual);
        
        AplicarCambioDeEstilo(); // Aplicar cambio de color en cada nivel
    }

    void AplicarCambioDeEstilo()
{
    Debug.Log("🔄 Cambiando colores...");

    // Cambiar el color del suelo
    if (coloresSuelo.Length > 0 && suelo != null)
    {
        Renderer sueloRenderer = suelo.GetComponent<Renderer>();
        if (sueloRenderer != null)
        {
            int indiceSuelo = Random.Range(0, coloresSuelo.Length);
            sueloRenderer.material.color = coloresSuelo[indiceSuelo];
            Debug.Log("🌍 Nuevo color del suelo: " + coloresSuelo[indiceSuelo]);
        }
        else
        {
            Debug.LogWarning("⚠️ El suelo no tiene un Renderer.");
        }
    }
    else
    {
        Debug.LogWarning("⚠️ No hay colores asignados para el suelo.");
    }

    // Cambiar el color del jugador
    if (coloresJugador.Length > 0 && jugadorRenderer != null)
    {
        int indiceJugador = Random.Range(0, coloresJugador.Length);
        jugadorRenderer.material.color = coloresJugador[indiceJugador];
        Debug.Log("👤 Nuevo color del jugador: " + coloresJugador[indiceJugador]);
    }
    else
    {
        Debug.LogWarning("⚠️ No hay colores asignados para el jugador.");
    }
}

}
