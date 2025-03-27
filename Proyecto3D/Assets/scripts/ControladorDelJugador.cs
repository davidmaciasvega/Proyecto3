using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorDelJugador : MonoBehaviour
{
    // Variables
    int contador;
    int nivel;
    Rigidbody rb;
    public TextMeshProUGUI puntuacion; // Texto para la puntuación
    public TextMeshProUGUI nivelTexto; // Texto para el nivel
    public float velocidad;
    public float fuerzaSalto = 5.0f;
    public GameObject cuboPrefab;
    public int numeroDeCubos = 5; // Número de cubos en el siguiente nivel
    public GameObject obstaculoPrefab;
    public int numeroDeObstaculos = 3; // Número de obstáculos
    private bool avanzandoDeNivel = false; // Control booleano para evitar bucles

    public GameObject cuboxPrefab;
public int numeroDeCubox = 1; 

    // Método Awake (inicializa valores)
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        nivel = 1; // Nivel inicial
        actualizarmarcador(); // Inicializa los textos
    }

    // Método FixedUpdate para movimiento y salto
    public void FixedUpdate()
    {
        // Movimiento horizontal y vertical
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento * velocidad);

        // Lógica para el salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
        }
    }

    // Método Update para verificar cubos
void Update()
{
    if (!avanzandoDeNivel) // Solo avanza si no está ya cambiando de nivel
    {
        int cubosRestantes = GameObject.FindGameObjectsWithTag("cubo").Length;
        Debug.Log("Cubos restantes: " + cubosRestantes);

        if (cubosRestantes == 0) 
        {
            avanzandoDeNivel = true; // Evita que se repita el proceso varias veces
   SubirNivel();
        }
    }
}
    // Método OnTriggerEnter para recolectar objetos
public void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("cubo"))
    {
        Destroy(other.gameObject);
        contador++; // Suma puntos
        actualizarmarcador();
    }
    else if (other.CompareTag("cubox"))
    {
        Destroy(other.gameObject);
        contador = Mathf.Max(contador - 1, 0); // 🔴 Resta 1 puntos (mínimo 0)
        actualizarmarcador();
    }
}

    // Método para actualizar los textos
    private void actualizarmarcador()
    {
        puntuacion.text = "Puntuación: " + contador; // Actualiza solo la puntuación
        nivelTexto.text = "Nivel: " + nivel; // Actualiza solo el nivel
    }

    // Método para subir de nivel
  

  
   void SubirNivel()
{
    Debug.Log("Avanzando al nivel: " + nivel);

    nivel++; 
    velocidad += 2.0f;
    fuerzaSalto += 1.0f;

    numeroDeCubos = Mathf.Min(numeroDeCubos + 2, 20);
    numeroDeObstaculos = Mathf.Min(numeroDeObstaculos + 1, 10);

    nivelTexto.text = "Nivel: " + nivel;

    GenerarCubos();
    GenerarObstaculos();
    GenerarCubox();
    numeroDeCubox = Mathf.Min(numeroDeCubox + 1, 5);

    // 🛑 Asegurar que solo vuelve a detectar cubos después de generarlos
    StartCoroutine(EsperarAntesDeReactivarDetección());
}


    // Método para generar cubos
   void GenerarCubos()
{
    GameObject piso = GameObject.FindGameObjectWithTag("Piso");  
    if (piso == null) 
    {
        Debug.LogError("¡No se encontró un objeto con el tag 'Piso'! Verifica que el objeto 'Suelo' tenga el tag correcto.");
        return;
    }

    Collider pisoCollider = piso.GetComponent<Collider>();
    if (pisoCollider == null) 
    {
        Debug.LogError("El objeto con tag 'Piso' no tiene un Collider. Agrega un BoxCollider.");
        return;
    }

    Bounds limites = pisoCollider.bounds;

    for (int i = 0; i < numeroDeCubos; i++)
    {
        UnityEngine.Vector3 posicionAleatoria = new UnityEngine.Vector3(
            Random.Range(limites.min.x + 1, limites.max.x - 1),
            Random.Range(1.0f, 5.0f),
            Random.Range(limites.min.z + 1, limites.max.z - 1)
        );
        GameObject nuevoCubo = Instantiate(cuboPrefab, posicionAleatoria, UnityEngine.Quaternion.identity);
        nuevoCubo.tag = "cubo";
    }
}


 void GenerarCubox()
{
    GameObject piso = GameObject.FindGameObjectWithTag("Piso");
    if (piso == null)
    {
        Debug.LogError("¡No se encontró un objeto con el tag 'Piso'! Verifica que el objeto 'Suelo' tenga el tag correcto.");
        return;
    }

    Collider pisoCollider = piso.GetComponent<Collider>();
    if (pisoCollider == null)
    {
        Debug.LogError("El objeto con tag 'Piso' no tiene un Collider. Agrega un BoxCollider.");
        return;
    }

    Bounds limites = pisoCollider.bounds;
    int numeroDeCubox = Mathf.Max(1, numeroDeCubos / 5); // Se generan menos Cubox que Cubos

    for (int i = 0; i < numeroDeCubox; i++)
    {
        UnityEngine.Vector3 posicionAleatoria = new UnityEngine.Vector3(
            Random.Range(limites.min.x + 1, limites.max.x - 1),
            Random.Range(1.0f, 5.0f),
            Random.Range(limites.min.z + 1, limites.max.z - 1)
        );

        GameObject nuevoCubox = Instantiate(cuboxPrefab, posicionAleatoria, UnityEngine.Quaternion.identity);
        nuevoCubox.tag = "cubox"; // Asegura que tenga el tag correcto
    }
}


    // Coroutine para esperar antes de reactivar la detección
    IEnumerator EsperarAntesDeReactivarDetección()
    {
        yield return new WaitForSeconds(2.0f); // Espera 2 segundos
        avanzandoDeNivel = false; // Reactiva la detección de cubos
    }

    // Método para generar obstáculos
   void GenerarObstaculos()
{
    GameObject piso = GameObject.FindGameObjectWithTag("Piso");
    if (piso == null)
    {
        Debug.LogError("¡No se encontró un objeto con el tag 'Piso'! Verifica que el objeto 'Suelo' tenga el tag correcto.");
        return;
    }

    Collider pisoCollider = piso.GetComponent<Collider>();
    if (pisoCollider == null)
    {
        Debug.LogError("El objeto con tag 'Piso' no tiene un Collider. Agrega un BoxCollider.");
        return;
    }

    Bounds limites = pisoCollider.bounds;

    for (int i = 0; i < numeroDeObstaculos; i++)
    {
        UnityEngine.Vector3 posicionAleatoria = new UnityEngine.Vector3(
            Random.Range(limites.min.x + 1, limites.max.x - 1),
            0.5f, // Mantén los obstáculos a nivel del suelo
            Random.Range(limites.min.z + 1, limites.max.z - 1)
        );

        Instantiate(obstaculoPrefab, posicionAleatoria, UnityEngine.Quaternion.identity);
    }
}
}
