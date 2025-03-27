using UnityEngine;
using System.Collections;

public class giro : MonoBehaviour
{
   Vector3 rotacion = new Vector3(15, 30, 45);
void Start()
{
}

void Update()
{
    transform.Rotate(rotacion * Time.deltaTime);
}
}