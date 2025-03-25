using UnityEngine;
using System.Collections;

public class ControladorDeLaCamara : MonoBehaviour
{
    public GameObject jugador;
    private Vector3 posicionRelativa;

    public void Start()
    {
        posicionRelativa = transform.position - jugador.transform.position;
    }

    void LateUpdate()
    {
        transform.position = jugador.transform.position + posicionRelativa;
    }
}