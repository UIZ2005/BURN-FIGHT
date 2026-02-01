using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBomb : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Circulo")
        {
            Destroy(collision.gameObject);
            audioManager.seleccionAudio(2);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Animator>().SetBool("explode", true);
        }
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movimiento>().tomarDano();
        }
    }
}
