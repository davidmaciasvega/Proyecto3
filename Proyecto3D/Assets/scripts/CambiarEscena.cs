using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void IrAEscenaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}