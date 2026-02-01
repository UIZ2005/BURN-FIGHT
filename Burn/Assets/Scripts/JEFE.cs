using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class JEFE : MonoBehaviour
{
    private float TiempoCombate;
    public bool inicioCombate=false;
    public int tiempoEntreacciones=5;
    public float time;
    private int accion;
    public GameObject BolaFuego;
    public GameObject CirculoMagico;
    public Transform firePoint;
    public GameObject player;
    private Vector2 playerPosition;
    public float velocidadSolarBomb=20;
    public float velocidadsol=20;
    public Transform[] posicionesSol;
    private GameObject proyectil;
    private Vector2 circuloPosition;
    private int posicion;
    public GameObject puntaje;
    public Informacion informacion;
    private Vector2 posicionInicialSol;
    public GameObject sol1;
    public GameObject sol2;
    private AudioManager audioManager;
    public GameObject[] ambientes;
    public GameObject efecthurt;
    public GameObject finalefect;

    //cronometro
    public UnityEngine.UI.Slider cronometroSLider;
    public int tiempoMax = 45;



    // Start is called before the first frame update
    void Start()
    {
        posicionInicialSol = transform.position;

        if (cronometroSLider != null)
        {
            cronometroSLider.maxValue = tiempoMax;
            cronometroSLider.value = tiempoMax;//inicia el circulo lleno
        }
        audioManager = FindAnyObjectByType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
        time+= Time.deltaTime;

        if (inicioCombate)
        {
            TiempoCombate += Time.deltaTime;

            // Actualizar el Slider
            if (cronometroSLider != null)
            {
                cronometroSLider.value = tiempoMax- TiempoCombate;
            }

        }

        if (time > tiempoEntreacciones && inicioCombate)
        {
            accion = Random.Range(0, 4);
            switch (accion)
            {
                case 1:
                    //Accion 1
                    StartCoroutine("DisparoDeFuego");
                    break;

                case 2:
                    //Accion 2
                    playerPosition = player.transform.position;
                    StartCoroutine("circuloFuego");
                    break;

                case 3:
                    //Accion 3
                    int anterior = posicion;
                     posicion = Random.Range(0, 4);

                    if (posicion != anterior)
                     {
                        Vector2 lugar = posicionesSol[posicion].position;
                        Vector2 direccion = (lugar - (Vector2)transform.position).normalized;
                        gameObject.GetComponent<Rigidbody2D>().velocity = velocidadsol * direccion;
                     }
                    if (posicion == anterior)
                    {
                        int random = Random.Range(0, 2);
                        if (random == 0)
                        {
                            StartCoroutine("DisparoDeFuego");
                            
                        }
                        else if (random == 1)
                        {
                            playerPosition = player.transform.position;
                            StartCoroutine("circuloFuego");
                        }
                    }
                    
                    break;

                default:
                    break;
            }
            time = 0;
            accion = 0;
        }





       
     
        if(TiempoCombate>30)
        {
            finalefect.SetActive(true);
            sol1.SetActive(false);
            sol2.SetActive(true);
            tiempoEntreacciones = 1;
            //cambiar los estados del sol a tiempo critico
        }
        if (TiempoCombate > 45 || player.GetComponent<Movimiento>().vidas==0)
        {
            finalefect.SetActive(false);
            ambientes[0].SetActive(true);
            ambientes[1].SetActive(false);
            tiempoEntreacciones = 2;
            time = 0;
            inicioCombate = false;
            TiempoCombate = 0;
            sol1.SetActive(true);
            sol2.SetActive(false);
            // Reiniciar el Slider
            if (cronometroSLider != null)
            {
                cronometroSLider.value =tiempoMax;
            }


            player.GetComponent<Movimiento>().resetVida();
            puntaje.SetActive(true);
            player.GetComponent<Movimiento>().enabled = false;
            player.GetComponent<DisparoJugador>().enabled = false;
            informacion.registrarPuntaje(gameObject.GetComponent<PuntosDano>().puntos);
            informacion.puntajeActual();
        }
        if (TiempoCombate == 0)
        {
            gameObject.transform.position = posicionInicialSol;
        }
    }
    private IEnumerator circuloFuego()
    {
        GameObject circulo=Instantiate(CirculoMagico, playerPosition, transform.rotation);
        circuloPosition = circulo.transform.position;
        yield return new WaitForSeconds(1);
        proyectil= Instantiate(BolaFuego, firePoint.position, transform.rotation);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        Vector2 direccion = (circuloPosition - (Vector2)firePoint.position).normalized;
        rb.velocity = direccion * velocidadSolarBomb;
        Destroy(circulo, 4);
        yield return null;
    }
    private IEnumerator DisparoDeFuego()
    {
        gameObject.GetComponent<Disparosol>().enabled = true;
        audioManager.seleccionAudio(1);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Disparosol>().enabled = false;
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "posicion")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (collision.tag == "Player")
        {
            collision.GetComponent<Movimiento>().tomarDano();
        }
        if (collision.tag == "hurt")
        {
            StartCoroutine("hurt");
        }
    }
    IEnumerator hurt()
    {
        efecthurt.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        efecthurt.GetComponent<SpriteRenderer>().enabled = false;

        yield return null;
    }
}
