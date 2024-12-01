using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Obtiene el Rigidbody del pino
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No se encontr� Rigidbody en el pin");
        }

        // Al principio, los pinos no deben caer hasta que sean golpeados
        rb.isKinematic = true;
    }

    // Detecta la colisi�n con la bola
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n fue con la bola
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Activa la f�sica para que el pino caiga
            rb.isKinematic = false;

            // Aplica una peque�a fuerza al pino para que caiga
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
