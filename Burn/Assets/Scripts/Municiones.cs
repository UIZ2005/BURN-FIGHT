using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Municiones : MonoBehaviour
{

    public TextMeshProUGUI TextoBloqueador; 
    public float tiempoReaparicion = 5f; // Tiempo en segundos para reaparecer
    public Vector2 areaSpawnMin; // Punto mínimo del área donde puede aparecer
    public Vector2 areaSpawnMax; // Punto máximo del área donde puede aparecer

    public int cantBloqueador = 4; // Cantidad de bloqueadores recogidos

    void Start()
    {
        ActualizarTexto();
    }

    void Update()
    {
        ActualizarTexto();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cantBloqueador < 5)
        {
            if (collision.gameObject.CompareTag("Bloqueador"))
            {
                cantBloqueador = 4;
                ActualizarTexto();
                StartCoroutine(Reaparecer(collision.gameObject));
            }
        }
    }

    IEnumerator Reaparecer(GameObject bloqueador)
    {
        bloqueador.SetActive(false); // Ocultar el objeto
        yield return new WaitForSeconds(tiempoReaparicion); // Esperar el tiempo de reaparición

        // Elegir una nueva posición aleatoria dentro del área definida
        float randomX = Random.Range(areaSpawnMin.x, areaSpawnMax.x);
        float randomY = Random.Range(areaSpawnMin.y, areaSpawnMax.y);
        bloqueador.transform.position = new Vector3(randomX, randomY, bloqueador.transform.position.z);

        bloqueador.SetActive(true); // Mostrar el objeto de nuevo
    }

    void ActualizarTexto()
    {
        if (TextoBloqueador != null)
        {
            TextoBloqueador.text = cantBloqueador.ToString();
        }
    }
}
