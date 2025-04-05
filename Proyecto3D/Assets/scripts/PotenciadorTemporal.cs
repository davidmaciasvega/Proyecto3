using UnityEngine;
using System.Collections;

public class PotenciadorTemporal : MonoBehaviour
{
    public float duracion = 5f; // duración del efecto
    public float velocidadExtra = 10f; // velocidad que se sumará
    private ControladorDelJugador jugador;
    private Color colorOriginal;
    private TrailRenderer trail;

    void Start()
    {
        jugador = GetComponent<ControladorDelJugador>();
        colorOriginal = GetComponent<Renderer>().material.color;

        // Si no tiene TrailRenderer, lo añadimos (puedes preconfigurarlo también en el editor)
        trail = GetComponent<TrailRenderer>();
        if (trail == null)
        {
            trail = gameObject.AddComponent<TrailRenderer>();
            trail.time = 0.5f;
            trail.startWidth = 0.5f;
            trail.endWidth = 0f;
            trail.material = new Material(Shader.Find("Sprites/Default")); // material sencillo
            trail.startColor = Color.yellow;
            trail.endColor = new Color(1, 1, 0, 0); // amarillo a transparente
        }
        trail.enabled = false;
    }

    public void ActivarPotenciador()
    {
        StartCoroutine(PotenciadorCoroutine());
    }

    IEnumerator PotenciadorCoroutine()
    {
        Debug.Log("🚀 Potenciador activado durante " + duracion + " segundos");
        jugador.velocidad += velocidadExtra;

        // Efecto visual
        GetComponent<Renderer>().material.color = Color.yellow;
        trail.enabled = true;

        yield return new WaitForSeconds(duracion);

        jugador.velocidad -= velocidadExtra;

        // Restaurar visual
        GetComponent<Renderer>().material.color = colorOriginal;
        trail.enabled = false;
    }
}
