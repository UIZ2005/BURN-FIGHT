using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private void Start()
    {
        // Destruir la bala automáticamente después de 2 segundos
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Movimiento>().tomarDano();
            Destroy(gameObject);
        }
    }
}

