using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaJugador : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int dano=10;
    public int combo=1;
    private GameObject player;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time>1.5f)
        {
            player.GetComponent<DisparoJugador>().comboActual = 1;
        }
        combo =player.GetComponent<DisparoJugador>().comboActual;

        transform.Translate(Vector2.up * velocidad*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sol")
        {

            collision.GetComponent<PuntosDano>().puntaje(dano*combo);
            player.GetComponent<DisparoJugador>().comboActual+=1;
            Destroy(gameObject);
        }
    }
}
