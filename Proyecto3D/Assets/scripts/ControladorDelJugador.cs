using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControladorDelJugador : MonoBehaviour
{
    // Variables ya definidas
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
    public TextMeshProUGUI highScoreTexto; // Texto para mostrar el puntaje más alto
    private int highScore; // Variable para almacenar el puntaje más alto
    
    public CambioDeEstilo cambioDeEstilo; // Referencia a CambioDeEstilo
    
public PotenciadorTemporal potenciador; // Arrástralo desde el inspector

    // Método Awake (inicializa valores)

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        nivel = 1; // Nivel inicial
        actualizarmarcador(); // Inicializa los textos
         highScore = PlayerPrefs.GetInt("Puntaje mas alto", 0); 
    
    actualizarmarcador();
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

void Start()
{
    highScore = PlayerPrefs.GetInt("HighScore", 0); // Ahora sí actualiza la variable global
    highScoreTexto.text = "High Score: " + highScore;
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
        contador++;

        // 🔹 Solo actualizar el high score si el contador es mayor
        if (contador > highScore)
        {
            highScore = contador;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        actualizarmarcador();
    }
    else if (other.CompareTag("cubox"))
    {
        Destroy(other.gameObject);
        contador = Mathf.Max(contador - 1, 0);
        actualizarmarcador();
    }
}
public void GuardarHighScore()
{
    int highScore = PlayerPrefs.GetInt("HighScore", 0);
    if (contador > highScore)
    {
        PlayerPrefs.SetInt("HighScore", contador);
        PlayerPrefs.Save();
        highScoreTexto.text = "High Score: " + contador;
    }
}
public void ReiniciarHighScore()
{
    if (highScoreTexto == null)
    {
        Debug.LogError("highScoreTexto es NULL. Asigna el objeto de texto en el Inspector.");
    }
    else
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        highScore = 0;  // Reinicia el high score a 0
        highScoreTexto.text = "High Score: 0"; // Actualiza el texto de high score
    }


    highScoreTexto.text = "High Score: 0";
}

 
 public void ReiniciarVariables()
    {
        // Reinicia las variables esenciales
        contador = 0;
        nivel = 1;
        velocidad = 5.0f; // Restablece la velocidad, por ejemplo
        fuerzaSalto = 5.0f; // Restablece la fuerza del salto
        numeroDeCubos = 5; // Restablece el número de cubos en el siguiente nivel
        numeroDeObstaculos = 3; // Restablece el número de obstáculos
        numeroDeCubox = 1; // Restablece el número de cuboxes
        avanzandoDeNivel = false; // Resetea el control para avanzar de nivel

        // Restablece el puntaje más alto (si lo has guardado en PlayerPrefs, puedes restablecerlo de esta manera)
        highScore = PlayerPrefs.GetInt("Puntaje mas alto", 0); 

        // Actualiza la UI con los valores reiniciados
        actualizarmarcador();
    }


    // Método para actualizar los textos
private void actualizarmarcador()
{
    if (puntuacion != null)
        puntuacion.text = "Puntuación: " + contador;
    else
        Debug.LogError("Puntuación no asignada en el Inspector.");

    if (nivelTexto != null)
        nivelTexto.text = "Nivel: " + nivel;
    else
        Debug.LogError("NivelTexto no asignado en el Inspector.");

    if (highScoreTexto != null)
        highScoreTexto.text = "Récord: " + highScore;
    else
        Debug.LogError("HighScoreTexto no asignado en el Inspector.");
}

    // Método para subir de nivel
  

  
private void SubirNivel()
{
    nivel++;  // Incrementa el nivel primero
    Debug.Log("Avanzando al nivel: " + nivel);  // Luego loguea el nuevo nivel

    velocidad += 2.0f;
    fuerzaSalto += 1.0f;

    numeroDeCubos = Mathf.Min(numeroDeCubos + 2, 20);
    numeroDeObstaculos = Mathf.Min(numeroDeObstaculos + 1, 10);

    nivelTexto.text = "Nivel: " + nivel;

    GenerarCubos();
    GenerarObstaculos();
    GenerarCubox();
    numeroDeCubox = Mathf.Min(numeroDeCubox + 1, 5);

    // Solo cambia los colores si el nivel es 2 o más
    if (nivel > 1)
    {
        cambioDeEstilo.SiguienteNivel();
    }
    if (nivel % 2 == 0 && potenciador != null)
{
    Debug.Log("🚀 Potenciador activado por nivel " + nivel);
    potenciador.ActivarPotenciador();
}

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