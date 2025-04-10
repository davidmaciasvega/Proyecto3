using UnityEngine;
using UnityEngine.UI;

public class ControlesEnPantalla : MonoBehaviour
{
    public Button botonMovimientoIzquierda;
    public Button botonMovimientoDerecha;
    public Button botonMovimientoAdelante;
    public Button botonMovimientoAtras;
    public Button botonSalto;

    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Asignar eventos a los botones
        botonMovimientoIzquierda.onClick.AddListener(MoverIzquierda);
        botonMovimientoDerecha.onClick.AddListener(MoverDerecha);
        botonMovimientoAdelante.onClick.AddListener(MoverAdelante);
        botonMovimientoAtras.onClick.AddListener(MoverAtras);
        botonSalto.onClick.AddListener(Saltar);
    }

    void MoverIzquierda()
    {
        rb.AddForce(Vector3.left * velocidad);
    }

void MoverDerecha()
{
    rb.AddForce(Vector3.right * velocidad);  // Movimiento hacia la derecha (eje X positivo)
}

    void MoverAdelante()
    {
        rb.AddForce(Vector3.forward * velocidad);
    }

    void MoverAtras()
    {
        rb.AddForce(Vector3.back * velocidad);
    }

    void Saltar()
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }
}
