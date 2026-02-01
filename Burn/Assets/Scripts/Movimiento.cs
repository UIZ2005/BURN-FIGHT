using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody2D rig;
    public int vidas = 3;
    public GameObject[] corazones;
    public float tiempoInvulnerable=2;
    private float time=2;
    public Animator pina;
    [SerializeField]
    private float velocidad;


    //poder de velocidad
    [SerializeField] private float velocidadBase;
    [SerializeField] private float velocidadExtra;
    [SerializeField] private float tiempoSprint;
    private float tiempoActualSprint;
    private float tiemposiguietneSprint;
    [SerializeField] private float tiempoEntreSprint;
    private bool puedeCorrer;
    private bool estaCorriendo;


    void Start()
    {
         rig = GetComponent<Rigidbody2D>();
         tiempoActualSprint = tiempoSprint;



    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Activar sprint solo si se presiona la tecla Z y está disponible
        if (Input.GetKeyDown(KeyCode.Space) && puedeCorrer)
        {
            velocidad = velocidadExtra;
            tiempoActualSprint = tiempoSprint;
            estaCorriendo = true;
        }

        // Reducir el tiempo de sprint mientras esté activo
        if (estaCorriendo)
        {
            tiempoActualSprint -= Time.deltaTime;

            // Si el sprint se agota, volver a la velocidad normal
            if (tiempoActualSprint <= 0)
            {
                velocidad = velocidadBase;
                estaCorriendo = false;
                puedeCorrer = false; // Bloquea el sprint hasta que se recargue
                tiemposiguietneSprint = Time.time + tiempoEntreSprint;
            }
        }

        // Recargar sprint después del tiempo de espera
        if (!puedeCorrer && Time.time >= tiemposiguietneSprint)
        {
            puedeCorrer = true;
        }

        // Aplicar movimiento
        rig.velocity = new Vector3(horizontal, vertical) * velocidad;

        rig.velocity = new Vector3(horizontal, vertical) * velocidad;

        if (time > tiempoInvulnerable)
        {
            pina.SetBool("dano", false);
        }
        if (vidas == 0)
        {
            pina.SetBool("dano", false);
            
        }
    }

    public void tomarDano()
    {
        if (time > tiempoInvulnerable)
        {
            corazones[vidas].SetActive(false);
            vidas -= 1;
            time = 0;
            pina.SetBool("dano", true);
        }
        
    } 
    public void resetVida()
    {
        vidas = 3;
        corazones[1].SetActive(true);
        corazones[2].SetActive(true);
        corazones[3].SetActive(true);
    }
}
