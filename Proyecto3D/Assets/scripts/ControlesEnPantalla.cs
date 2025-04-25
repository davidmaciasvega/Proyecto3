using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    // Banderas de movimiento
    private bool moviendoIzquierda;
    private bool moviendoDerecha;
    private bool moviendoAdelante;
    private bool moviendoAtras;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Eventos de presionar y soltar los botones
        AgregarEventos(botonMovimientoIzquierda, () => moviendoIzquierda = true, () => moviendoIzquierda = false);
        AgregarEventos(botonMovimientoDerecha, () => moviendoDerecha = true, () => moviendoDerecha = false);
        AgregarEventos(botonMovimientoAdelante, () => moviendoAdelante = true, () => moviendoAdelante = false);
        AgregarEventos(botonMovimientoAtras, () => moviendoAtras = true, () => moviendoAtras = false);
        botonSalto.onClick.AddListener(Saltar);
    }

    void Update()
    {
        Vector3 direccion = Vector3.zero;

        if (moviendoIzquierda) direccion += Vector3.left;
        if (moviendoDerecha) direccion += Vector3.right;
        if (moviendoAdelante) direccion += Vector3.forward;
        if (moviendoAtras) direccion += Vector3.back;

        // Movimiento suave continuo
        rb.AddForce(direccion.normalized * velocidad);
    }

    void Saltar()
    {
        // Solo saltar si está en el suelo (opcional)
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    void AgregarEventos(Button boton, System.Action onPress, System.Action onRelease)
    {
        EventTrigger trigger = boton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = boton.gameObject.AddComponent<EventTrigger>();

        // Presionar
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { onPress(); });
        trigger.triggers.Add(entryDown);

        // Soltar
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { onRelease(); });
        trigger.triggers.Add(entryUp);
    }
}
